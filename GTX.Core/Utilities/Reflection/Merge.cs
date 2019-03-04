using System;
using System.Collections.Generic;
using System.Reflection;

namespace GTX
{
    public static class MergeExtension
    {
        /// <summary>
        /// Merges the specified source.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        public static void Merge(this object destination, object source)
        {
            var sourceType = source.GetType();
            var destType = destination.GetType();
            var sourceProperties = sourceType.GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destProperty = destType.GetProperty(sourceProperty.Name);

                // Merge ICollection property
                if (destProperty != null && IsTypeOrInterface(destProperty, "ICollection`1") && IsTypeOrInterface(sourceProperty, "ICollection`1"))
                {
                    // Get extension method for merging ICollection
                    var method = typeof(MergeExtension).GetMethod("MergeList");

                    // Get source and destination value
                    var sourceValue = sourceProperty.GetValue(source, null);
                    var destValue = destProperty.GetValue(destination, null);

                    // Invoke the extension method to merge source and destination
                    method.MakeGenericMethod(new Type[] { destProperty.PropertyType.GetGenericArguments()[0], sourceProperty.PropertyType.GetGenericArguments()[0] }).Invoke(destProperty, new[] { destValue, sourceValue });
                }
                // Merge property other than List
                else if (destProperty != null && destProperty.PropertyType == sourceProperty.PropertyType && destProperty.CanWrite)
                {
                    var sourceValue = sourceProperty.GetValue(source, null);
                    destProperty.SetValue(destination, sourceValue, null);
                }
            }

        }        

        /// <summary>
        /// Merges the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <exception cref="System.ArgumentException">destination cannot be null</exception>
        public static void MergeList<T, U>(this ICollection<T> destination, ICollection<U> source) where T : new()
        {
            if (destination == null)
                throw new ArgumentException("destination cannot be null");

            if (source == null)
                return;

            destination.Clear();

            var method = destination.GetType().GetMethod("Add");
            foreach (var sourceItem in source)
            {
                // Check if its a string/value type/object   
                if (IsSimpleType(typeof(U)))
                {
                    if (typeof(U) == typeof(T))
                        method.Invoke(destination, new object[] { sourceItem });
                }
                else
                {
                    var item = new T();
                    item.Merge(sourceItem);
                    method.Invoke(destination, new object[] { item });
                }
            }
        }

        /// <summary>
        /// Merges the dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <exception cref="System.ArgumentException">destination cannot be null</exception>
        public static void MergeDictionary<T, U, V>(this IDictionary<T, U> destination, IDictionary<T, V> source) where U : new() where V : new()
        {
            if (destination == null)
                throw new ArgumentException("destination cannot be null");

            if (source == null)
                return;

            destination.Clear();

            var method = destination.GetType().GetMethod("Add");
            foreach (var sourceItem in source)
            {
                // Check if its a string/value type/object   
                if (IsSimpleType(typeof(U)))
                {
                    if (typeof(U) == typeof(V))
                        method.Invoke(destination, new object[] { sourceItem.Key, sourceItem.Value });
                }
                else
                {
                    var item = new U();
                    item.Merge(sourceItem.Value);
                    method.Invoke(destination, new object[] { sourceItem.Key, item });
                }
            }

        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <exception cref="System.ArgumentException">destination cannot be null</exception>
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            if (destination == null)
                throw new ArgumentException("destination cannot be null");

            if (source == null)
                return;

            foreach (var sourceItem in source)
                destination.Add(sourceItem);
        }        

        #region Private Methods

        /// <summary>
        /// Determines whether [is type or interface] [the specified property].
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="interfaceName">Name of the interface.</param>
        /// <returns></returns>
        private static bool IsTypeOrInterface(PropertyInfo property, string interfaceName)
        {
            var interfaceType = property.PropertyType.GetInterface(interfaceName);
            return (property.PropertyType.IsInterface && property.PropertyType.Name == interfaceName) || (interfaceType != null && interfaceType.Name == interfaceName) && (property.PropertyType.BaseType != typeof(Array));
        }

        /// <summary>
        /// Determines whether [is simple type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsSimpleType(Type type)
        {
            return type == typeof(string) || type == typeof(object) || type.IsValueType || type.IsPrimitive;
        }

        #endregion
    }
}