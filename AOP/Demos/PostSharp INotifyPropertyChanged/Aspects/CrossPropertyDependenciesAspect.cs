using System;
using Demo.Aspects;
using PostSharp.Extensibility;
using PostSharp.Laos;

namespace Demo.Aspects
{
    /// <summary>
    /// Analyses a type for any occurrance of the <see cref="DependsOnAttribute"/> attribute, and if found, wires up property change notifications on the 
    /// dependency properties.
    /// </summary>
    [MulticastAttributeUsage(MulticastTargets.Class | MulticastTargets.Struct), Serializable]
    public class CrossPropertyDependenciesAspect : CompoundAspect
    {
        /// <summary>
        /// Eveluates a type and looks for <see cref="DependsOnAttribute"/> attributes, and then applies the <see cref="NotifyChangeAttribute"/> 
        /// to those properties.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="collection">The collection.</param>
        public override void ProvideAspects(object element, LaosReflectionAspectCollection collection)
        {
            var targetType = element as Type;
            if (targetType == null) return;

            // Find all properties with a "DependsOn" attribute
            foreach (var originalProperty in targetType.GetProperties())
            {
                var dependsOnAttribute = GetCustomAttribute(originalProperty.GetGetMethod(), typeof(DependsOnAttribute)) as DependsOnAttribute;
                if (dependsOnAttribute == null) continue;
                
                // Look up the properties listed in the DependsOn attribute. On each on, inject the NotifyChange attribute on its setter, so that when those properties
                // change they will also signal that this property has changed.
                foreach (var affectedByPropertyName in dependsOnAttribute.DependencyPropertyNames)
                {
                    var affectedByPropertyInfo = targetType.GetProperty(affectedByPropertyName);
                    if (affectedByPropertyInfo == null)
                    {
                        throw new Exception(
                            string.Format(
                                "The property '{0}' on type '{1}' is marked as being dependant on sibling property '{2}', but the property '{2}' cannot be found.",
                                originalProperty.Name, originalProperty.DeclaringType, affectedByPropertyName));
                    }
                    var setter = affectedByPropertyInfo.GetSetMethod(true);
                    if (setter == null)
                    {
                        throw new Exception(
                            string.Format(
                                "The property '{0}' on type '{1}' is marked as being dependant on sibling property '{2}', but the property '{2}' does not have a setter.",
                                originalProperty.Name, originalProperty.DeclaringType, affectedByPropertyName));
                    }
                    collection.AddAspect(setter, new NotifyChangeAttribute(originalProperty.Name));
                }
            }
        }
    }
}