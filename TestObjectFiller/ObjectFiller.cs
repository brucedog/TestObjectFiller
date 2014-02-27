using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestObjectFiller
{
    /// <summary>
    /// Class fills an objects properties for test data
    /// </summary>
    public class ObjectFiller
    {
        // todo
        private static bool _isRuleOverrided;
        private static Random _random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Fills the the object's properties with random data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectTobeFilled">The object tobe filled.</param>
        /// <param name="objectsType">Type of the objects.</param>
        /// <returns>object with filled data</returns>
        public static T FillThisObject<T>(T objectTobeFilled, Type objectsType)
        {
            FillProperties(objectTobeFilled, objectsType);
            
            return objectTobeFilled;
        }
        
        private static void FillProperties<T>(T objectTobeFilled, Type objectsType)
        {
            PropertyInfo[] objectsProperties = objectsType.GetProperties();

            foreach (PropertyInfo propertyInfo in objectsProperties)
            {
                Type propertyType = propertyInfo.PropertyType;
                if (propertyType.Namespace == null) continue;

                // Make collection not null and try and add item to list.
                // TODO going to change logic to just handle the type of collections directly
                if (propertyType.Namespace.Contains("Collection"))
                {
                    dynamic instance;
                    if (propertyType.GetGenericArguments().Length > 0 && !propertyType.Name.Contains("Dic"))
                    {
                        // This is done to create a concrete implementation for an IList or IEnumerable
                        Type listType = typeof(List<>).MakeGenericType(propertyType.GetGenericArguments());
                        instance = Activator.CreateInstance(listType);
                    }
                    else
                    {
                        instance = Activator.CreateInstance(propertyType);
                    }
                    // TODO should be separate method of logic to handle dictionary
                    if (propertyType.Name.Contains("Dic"))
                    {
                        dynamic key = null;
                        dynamic value = null;
                        for (int i = 0; i < propertyType.GetGenericArguments().Length; i++)
                        {
                            dynamic temp;
                            var itemType = propertyType.GetGenericArguments()[i];
                            if (!itemType.IsSealed && itemType.GetProperties().Length > 0)
                            {
                                temp = Activator.CreateInstance(itemType);
                                Type itsType = temp.GetType();
                                FillProperties(temp, itsType);
                            }
                            else
                            {
                                temp = GetValue(itemType);
                            }

                            if (i == 0)
                            {
                                key = temp;
                            }
                            else
                            {
                                value = temp;
                            }
                        }

                        instance.Add(key, value);
                    }
                    else if (propertyType.GetGenericArguments().Length > 0)
                    {
                        var itemType = propertyType.GetGenericArguments()[0];
                        if (!itemType.IsSealed && itemType.GetProperties().Length > 0)
                        {
                            dynamic customObjectProperty = Activator.CreateInstance(itemType);
                            Type itsType = customObjectProperty.GetType();
                            FillProperties(customObjectProperty, itsType);
                            instance.Add(customObjectProperty);
                        }
                        else
                        {
                            dynamic objectToSet = GetValue(itemType);
                            instance.Add(objectToSet);
                        }
                    }

                    propertyInfo.SetValue(objectTobeFilled, instance, null);
                }
                else if (propertyType.Namespace.Contains("System"))
                {
                    var objectToSet = GetValue(propertyType);

                    propertyInfo.SetValue(objectTobeFilled, objectToSet, null);
                }
                else
                {                    
                    var customObjectProperty = Activator.CreateInstance(propertyType);
                    Type itsType = customObjectProperty.GetType();
                    FillProperties(customObjectProperty, itsType);

                    propertyInfo.SetValue(objectTobeFilled, customObjectProperty, null);
                }
            }
        }

        #region private methods

        /// <summary>
        /// Gets a value for a basic data type.
        /// TODO need to implement rule to override default values
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>random value for that properties type</returns>
        private static object GetValue(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case "String":
                    return "random_String_" + _random.Next();
                case "DateTime":
                    return DateTime.Now;
                case "Int32":
                    return _random.Next();
                case "Double":
                    return _random.NextDouble();
                case "Float":
                    double mantissa = (_random.NextDouble()*2.0) - 1.0;
                    double exponent = Math.Pow(2.0, _random.Next(-126, 128));
                    return (float) (mantissa*exponent);
                case "Decimal":
                    return Decimal.MaxValue;
                case "Object":
                    return new object();
                case "Int64":
                    return long.MaxValue;
                case "SByte":
                    return SByte.MaxValue;
                case "Char":
                    return 'x';
                case "Single":
                    return Single.MaxValue;
                case "Boolean":
                    return true;
                case "Array[]":
                    return new Array[1];
                default:
                    return null;
            }
        }
        #endregion
    }
}
