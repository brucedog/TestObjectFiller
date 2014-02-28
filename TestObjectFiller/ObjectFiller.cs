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
            FillObjectProperties(objectTobeFilled, objectsType);
            
            return objectTobeFilled;
        }

        #region private methods
        
        private static void FillObjectProperties<T>(T objectTobeFilled, Type objectsType)
        {
            PropertyInfo[] objectsProperties = objectsType.GetProperties();

            foreach (PropertyInfo propertyInfo in objectsProperties)
            {
                Type propertyType = propertyInfo.PropertyType;
                if (propertyType.Namespace == null) continue;

                dynamic value = null;
                // Make collection not null and try and add item to list.
                if (propertyType.Namespace.Contains("Collection"))
                {
                    // TODO this cannot be handle 
                    if (propertyType.Name.Contains("Hash"))
                        continue;
                    
                    if (propertyType.GetGenericArguments().Length == 1)
                    {
                        // This is done to create a concrete implementation for an IList or IEnumerable
                        Type listType = typeof(List<>).MakeGenericType(propertyType.GetGenericArguments());
                        value = Activator.CreateInstance(listType);

                        var itemType = propertyType.GetGenericArguments()[0];
                        dynamic temp = FillObject<T>(itemType);
                        value.Add(temp);
                    }
                    else
                    {
                        value = Activator.CreateInstance(propertyType);
                    }

                    if (propertyType.Name.Equals("ArrayList"))
                        FillArrayList<T>(propertyType, value);
                    else if (propertyType.Name.Contains("Dic"))
                        FillDictionary<T>(propertyType, value);

                }
                else if (propertyType.Namespace.Contains("System"))
                {
                    value = GetBasicDataTypeValue(propertyType);
                }
                else if (IsComplexObject<T>(propertyType))
                {
                    value = Activator.CreateInstance(propertyType);
                    Type itsType = value.GetType();

                    FillObjectProperties(value, itsType);
                }

                propertyInfo.SetValue(objectTobeFilled, value, null);
            }
        }

        private static void FillArrayList<T>(Type propertyType, dynamic value)
        {
            if (!propertyType.Name.Contains("ArrayList"))
                return;

            ArrayList arrayList = value;

            arrayList.Add(new object());
        }

        /// <summary>
        /// Fills the dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="instance">The instance.</param>
        private static void FillDictionary<T>(Type propertyType, dynamic instance)
        {
            if (!propertyType.Name.Contains("Dic")) 
                return;

            dynamic key = null;
            dynamic value = null;
            for (int i = 0; i < propertyType.GetGenericArguments().Length; i++)
            {
                var itemType = propertyType.GetGenericArguments()[i];
                dynamic temp = FillObject<T>(itemType);

                if (i == 0)
                    key = temp;
                else
                    value = temp;
            }

            instance.Add(key, value);
        }

        /// <summary>
        /// Fills the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        private static dynamic FillObject<T>(Type itemType)
        {
            dynamic temp;
            if (IsComplexObject<T>(itemType))
            {
                temp = Activator.CreateInstance(itemType);
                Type itsType = temp.GetType();
                FillObjectProperties(temp, itsType);
            }
            else
            {
                temp = GetBasicDataTypeValue(itemType);
            }
            return temp;
        }

        /// <summary>
        /// Determines whether [is complex object] [the specified item type].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        private static bool IsComplexObject<T>(Type itemType)
        {
            return !itemType.IsSealed && itemType.GetProperties().Length > 0;
        }


        /// <summary>
        /// Gets a value for a basic data type.
        /// TODO need to implement rule to override default values
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>random value for that properties type</returns>
        private static object GetBasicDataTypeValue(Type propertyType)
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
                case "Guid":
                    return Guid.NewGuid();
                default:
                    return null;
            }
        }
        #endregion
    }
}
