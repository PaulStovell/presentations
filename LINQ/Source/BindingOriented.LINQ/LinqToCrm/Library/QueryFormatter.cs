using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using stunnware.CRM.Fetch;

namespace LinqtoCRM
{
    internal class QueryFormatter : ExpressionVisitor
    {
        FetchStatement stmt;
        Dictionary<string, FetchLinkEntity> attributeMap = new Dictionary<string, FetchLinkEntity>();

        internal QueryFormatter()
        {
        }

        internal string Format(Expression expression)
        {
            this.stmt = new FetchStatement();
            this.Visit(expression);
            return this.stmt.ToString();
        }


        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) || m.Method.DeclaringType == typeof(Enumerable))
            {
                switch (m.Method.Name)
                {
                    case "Where":
                        {
                            this.VisitWhere(m);
                            break;
                        }
                    case "Select":
                        {
                            this.VisitSelect(m);
                            break;
                        }
                    case "Join":
                        {
                            this.VisitJoin(m);
                            break;
                        }
                    case "OrderBy":
                        {
                            this.VisitOrderBy(m);
                            break;
                        }
                    default: throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
            }
            return m;
        }



        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    //sb.Append(" NOT ");
                    this.Visit(u.Operand);
                    break;
                case ExpressionType.Quote:
                    this.Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            // this means we'll tend to have a redundant filter on top, but it eases the code
            FetchFilter filter = new FetchFilter(FetchFilterType.And);
            this.stmt.Entity.Filter = filter;
            this.VisitBinary(b, filter);
            return b;
        }

        protected void VisitBinary(Expression e, FetchFilter filter)
        {

            if (e is BinaryExpression)
            {
                BinaryExpression b = e as BinaryExpression;
                FetchFilter newFilter;
                FetchCondition cond = new FetchCondition();
                switch (b.NodeType)
                {
                    case ExpressionType.And:
                    case ExpressionType.AndAlso:
                        newFilter = new FetchFilter(FetchFilterType.And);
                        this.VisitBinary(b.Left, newFilter);
                        this.VisitBinary(b.Right, newFilter);
                        filter.Filters.Add(newFilter);
                        break;
                    case ExpressionType.Or:
                    case ExpressionType.OrElse:
                        newFilter = new FetchFilter(FetchFilterType.Or);
                        this.VisitBinary(b.Left, newFilter);
                        this.VisitBinary(b.Right, newFilter);
                        filter.Filters.Add(newFilter);
                        break;
                    case ExpressionType.Equal:
                        cond.ConditionOperator = FetchConditionOperator.Equal;
                        SetConditionalValues(cond, b);
                        filter.Conditions.Add(cond);
                        break;
                    case ExpressionType.NotEqual:
                        cond.ConditionOperator = FetchConditionOperator.NotEqual;
                        SetConditionalValues(cond, b);
                        filter.Conditions.Add(cond);
                        break;
                    case ExpressionType.LessThan:
                        cond.ConditionOperator = FetchConditionOperator.LowerThan;
                        SetConditionalValues(cond, b);
                        filter.Conditions.Add(cond);
                        break;
                    case ExpressionType.LessThanOrEqual:
                        cond.ConditionOperator = FetchConditionOperator.LowerEqual;
                        SetConditionalValues(cond, b);
                        filter.Conditions.Add(cond);
                        break;
                    case ExpressionType.GreaterThan:
                        cond.ConditionOperator = FetchConditionOperator.GreaterThan;
                        SetConditionalValues(cond, b);
                        filter.Conditions.Add(cond);
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        cond.ConditionOperator = FetchConditionOperator.GreaterEqual;
                        SetConditionalValues(cond, b);
                        filter.Conditions.Add(cond);
                        break;
                    default:
                        throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
                }
            }
            else
            {
                //not binaryexpression, boolean constant?
                throw new ArgumentException();
            }
        }

        private void SetConditionalValues(FetchCondition cond, BinaryExpression b)
        {
            MemberExpression mexp = b.Left as MemberExpression;
            if (mexp.Member.Name.Equals("Value"))
            {
                // need to go one level deeper
                cond.Attribute = (mexp.Expression as MemberExpression).Member.Name;
            }
            else
            {
                cond.Attribute = mexp.Member.Name;
            }
            
            object value = Expression.Lambda(b.Right).Compile().DynamicInvoke();
            // check for null comparison
            if (value == null)
            {
                if (cond.ConditionOperator == FetchConditionOperator.Equal)
                    cond.ConditionOperator = FetchConditionOperator.Null;
                else if (cond.ConditionOperator == FetchConditionOperator.NotEqual)
                    cond.ConditionOperator = FetchConditionOperator.NotNull;
                else
                    throw new Exception("Strange null-comparison");
            }
            else
            {
                cond.Values.Add(Expression.Lambda(b.Right).Compile().DynamicInvoke());
            }
        }
        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c.Value == null)
            {
            }
            else
            {
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        break;
                    case TypeCode.String:
                        break;
                    case TypeCode.Object:
                        stmt.Entity = new FetchEntity(c.Value.GetType().GetGenericArguments()[0].Name);
                        stmt.Entity.Columns = new FetchColumnSet();
                        stmt.Entity.Columns.Columns = new System.Collections.Specialized.StringCollection();
                        break;
                    default:
                        break;
                }
            }
            return c;
        }

        protected void VisitSelect(MethodCallExpression select)
        {
            // visit the source
            this.Visit(select.Arguments[0]);
            // visit the selector
            this.VisitSelector(select.Arguments[1]);
        }

        private void VisitSelector(Expression selector)
        {
            // TODO: this may be too simplistic
            UnaryExpression uexp = selector as UnaryExpression;
            LambdaExpression lxp = (uexp.Operand as LambdaExpression);
            NewExpression nxp = lxp.Body as NewExpression;
            foreach (MemberExpression memxp in nxp.Arguments)
            {
                if (memxp.Expression is MemberExpression)
                {
                    string alias = (memxp.Expression as MemberExpression).Member.Name;
                    //string alias = memxp.Member.Name;
                    if (attributeMap.ContainsKey(alias))
                    {
                        // add the column to the relevant link-thingy
                        FetchLinkEntity link = attributeMap[alias];
                        link.Columns.Columns.Add(memxp.Member.Name);
                    }
                    else
                    {
                        // TODO: duplicated code
                        //else just add the usual way
                        stmt.Entity.Columns.Columns.Add(memxp.Member.Name);
                    }
                }
                else
                {
                    //else just add the usual way
                    stmt.Entity.Columns.Columns.Add(memxp.Member.Name);
                }
            }
        }

        protected void VisitWhere(MethodCallExpression where)
        {
            // visit the source
            this.Visit(where.Arguments[0]);
            // visit predicate
            this.Visit(where.Arguments[1]);
        }

        private void VisitOrderBy(MethodCallExpression m)
        {
            // visit the source
            this.Visit(m.Arguments[0]);
            // BUG: Check that order attribute is on primary (non-linked/joined) entity
            FetchOrder order = new FetchOrder((((m.Arguments[1] as UnaryExpression).Operand as LambdaExpression).Body as MemberExpression).Member.Name);
            stmt.Entity.Orders.Add(order);
        }

        private void VisitJoin(MethodCallExpression join)
        {
            // this will set up the correct Fetchentity
            this.Visit(join.Arguments[0]);
            FetchLinkEntity link;
            // TODO: Handle non-dotted values
            MemberExpression outerMemberExp = (((join.Arguments[2] as UnaryExpression).Operand as LambdaExpression).Body as MemberExpression).Expression as MemberExpression;
            MemberExpression innerMemberExp = (((join.Arguments[3] as UnaryExpression).Operand as LambdaExpression).Body as MemberExpression).Expression as MemberExpression;
            string outerTypeName = outerMemberExp.Expression.Type.Name;
            string innerTypeName = innerMemberExp.Expression.Type.Name;

            // This was commented out to allow intra-table joins - wonder why I put it in?
            //if (!innerTypeName.Equals(stmt.Entity.EntityName))
            //{
            link = new FetchLinkEntity(innerTypeName);
            link.From = innerMemberExp.Member.Name;
            link.To = outerMemberExp.Member.Name;
            //}
            //else
            //    throw new ArgumentException();
            link.Columns = new FetchColumnSet();
            link.Columns.Columns = new System.Collections.Specialized.StringCollection();
            // TODO: this is very bad karma, but I'm too tired now
            link.Alias = "";
            stmt.Entity.LinkEntities.Add(link);

            // now get the attributes
            VisitJoinSelector(join.Arguments[4], outerTypeName, innerTypeName, link);

        }

        private void VisitJoinSelector(Expression joinSelector, string outerTypeName, string innerTypeName, FetchLinkEntity link)
        {
            UnaryExpression uexp = joinSelector as UnaryExpression;
            LambdaExpression lxp = (uexp.Operand as LambdaExpression);
            NewExpression nxp = lxp.Body as NewExpression;

            //if(nxp.Arguments[0].NodeType == ExpressionType.
            foreach (Expression exp in nxp.Arguments)
            {
                string entName;
                // the format of the selector depends on the presence of a where clause it seems
                if (exp is MemberExpression)
                {
                    MemberExpression masgn = exp as MemberExpression;
                    entName = (masgn.Expression as ParameterExpression).Type.Name;
                    if (entName == outerTypeName) // add to outer attributes
                        stmt.Entity.Columns.Columns.Add(masgn.Member.Name);
                    else if (entName == innerTypeName) // add to link-entity columns
                        link.Columns.Columns.Add(masgn.Member.Name);

                }
                else if (exp is ParameterExpression)
                {
                    ParameterExpression pExp = exp as ParameterExpression;
                    entName = pExp.Type.Name;
                    if (entName == innerTypeName)
                        // record the alias and link-node for future use
                        attributeMap.Add(pExp.Name, link);
                    //if(entName == outerTypeName)
                    // do nothing, this will be handled by the regular selector

                }
                else
                {
                    throw new Exception("Unknown Expression type when visiting join-selector");
                }

            }
        }


        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }
    }
}
