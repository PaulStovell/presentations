using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using stunnware.CRM.Fetch;
using System.Xml;
using LinqToCrm.CrmWebService;


namespace LinqtoCRM {

    public class CrmQueryProvider : QueryProvider {
        CrmService _service;

        public CrmQueryProvider(CrmService service)
        {
            _service = service;
        }

        public IQueryable<T> Linq<T>()
        {
            return new Query<T>(this);
        }

        /// <summary>
        /// This method is apparently used by the debugger to visualize the current query
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override string GetQueryText(Expression expression)
        {
            return null;
            //return this.Translate(expression).FetchXML;
        }

        public override object Execute(Expression expression) 
        {
            // TODO: implement once selector is sorted
            throw new NotImplementedException();
        }

        // TODO: Temporary meassure
        internal IEnumerator<T> ExecuteQuery<T>(Expression expression)
        {
            TranslateResult transResult = this.Translate(expression);

            string result = _service.Fetch(transResult.FetchXML);
            // stick this code in some builder class
            PropertyInfo[] props = typeof(T).GetProperties();
            Type[] types = props.Select(p => p.PropertyType).ToArray();
            //Type[] types = from p in props select p.PropertyType; // alternative syntax
            ConstructorInfo cInf = typeof(T).GetConstructor(types);

            // The construcotr is called with null as getting the arguments in the right order would be a major bore
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(result);
            XmlNodeList nodelist = xmldoc.SelectNodes("/resultset/result");

            foreach (XmlNode node in nodelist)
            {
                yield return (T)cInf.Invoke(
                    //props.Select(p => GetValFromXML(p, node.SelectSingleNode("substring-after(" + p.Name + ",'.')"))).ToArray());
                    props.Select(p => GetValFromXML(p, node)).ToArray());
            }
        }

        internal class TranslateResult {
            internal string FetchXML;
            internal LambdaExpression Projector;
        }

        private TranslateResult Translate(Expression expression) {
            expression = Evaluator.PartialEval(expression);
            string fetchXML = new QueryFormatter().Format(expression);
            
            //LambdaExpression projector = new ProjectionBuilder().Build(proj.Projector);
            return new TranslateResult { FetchXML = fetchXML , Projector = null};
            //return null;
        }

        private object GetValFromXML(PropertyInfo propertyInfo, XmlNode resNode)
        {
            // TODO: This code is not very gratifying
            XmlNode node = null;
            node = resNode.SelectSingleNode(propertyInfo.Name);

            if (node == null) // we have to look closer
            {
                foreach (XmlNode xNode in resNode.ChildNodes)
                {
                    // "Endswith" is necessary because CRM joined attribute with name of join-attribute (ie. "parentcustomerid.foo")
                    if (xNode.Name.EndsWith("."+propertyInfo.Name))
                    {
                        if (node != null)
                            throw new Exception("It seems your query has attributes with similar-ending names -- that's a problem.");
                        else
                        node = xNode;
                    }
                }
            }

            if (node == null)
            {
                return null;
            }

            string nodeText = node.InnerText;

            if (propertyInfo.PropertyType == typeof(string))
            {
                return nodeText;
            }

            if (propertyInfo.PropertyType == typeof(CrmBoolean))
            {
                CrmBoolean value = new CrmBoolean();
                value.Value = (nodeText == "1");
                value.name = node.Attributes["name"].Value;
                return value;
            }

            if (propertyInfo.PropertyType == typeof(CrmNumber))
            {
                CrmNumber value = new CrmNumber();
                value.Value = int.Parse(nodeText);
                value.formattedvalue = node.Attributes["formattedvalue"].Value;
                return value;
            }

            if (propertyInfo.PropertyType == typeof(CrmDateTime))
            {
                CrmDateTime value = new CrmDateTime();
                value.Value = nodeText;
                value.time = node.Attributes["time"].Value;
                value.date = node.Attributes["date"].Value;
                return value;
            }

            if (propertyInfo.PropertyType == typeof(CrmMoney))
            {
                CrmMoney value = new CrmMoney();
                value.Value = decimal.Parse(nodeText);
                value.formattedvalue = node.Attributes["formattedvalue"].Value;
                return value;
            }

            if (propertyInfo.PropertyType == typeof(CrmDecimal))
            {
                CrmDecimal value = new CrmDecimal();
                value.Value = decimal.Parse(nodeText);
                value.formattedvalue = node.Attributes["formattedvalue"].Value;
                return value;
            }

            if (propertyInfo.PropertyType == typeof(CrmFloat))
            {
                CrmFloat value = new CrmFloat();
                value.Value = float.Parse(nodeText);
                value.formattedvalue = node.Attributes["formattedvalue"].Value;
                return value;
            }

            if (propertyInfo.PropertyType == typeof(Picklist))
            {
                Picklist value = new Picklist();
                value.Value = int.Parse(nodeText);
                value.name = node.Attributes["name"].Value;
                return value;
            }

            if (propertyInfo.PropertyType == typeof(Lookup))
            {
                Lookup value = new Lookup();
                value.Value = new Guid(nodeText);
                value.name = node.Attributes["name"].Value;
                //value.type
                return value;
            }

            if (propertyInfo.PropertyType == typeof(Customer))
            {
                Customer value = new Customer();
                value.Value = new Guid(nodeText);
                value.name = node.Attributes["name"].Value;
                value.type = (node.Attributes["type"].Value == "1") ? EntityName.account.ToString() : EntityName.contact.ToString();
                return value;
            }

            if (propertyInfo.PropertyType == typeof(Owner))
            {
                Owner value = new Owner();
                value.Value = new Guid(nodeText);
                value.name = node.Attributes["name"].Value;
                value.type = (node.Attributes["type"].Value == "8") ? EntityName.systemuser.ToString() : EntityName.team.ToString();
                return value;
            }
            if (propertyInfo.PropertyType == typeof(Key))
            {
                Key value = new Key();
                value.Value = new Guid(nodeText);
                return value;
            }
            throw new Exception("Unknown return type encountered");
        }

    } 
}
