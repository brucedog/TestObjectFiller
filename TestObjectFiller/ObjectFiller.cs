using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestObjectFiller
{
    /// <summary>
    /// Class fills an objects properties for test data
    /// </summary>
    public class ObjectFiller
    {
        // todo
        private static bool _isRuleOverrided;
        private static Random _random = new Random(56465321);

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

                if (propertyType.Namespace.Contains("System"))
                {
                    switch (propertyType.Name)
                    {
                        case "String":
                            SetStringPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "DateTime":
                            SetDateTimePropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Int32":
                            SetIntegerPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Double":
                            SetDoublePropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Float":
                            SetFloatPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Decimal":
                            SetDecimalPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Object":
                            SetObjectPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Int64":
                            SetLongPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "SByte":
                            SetSbytePropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Char":
                            SetCharPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Single":
                            SetSinglePropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Boolean":
                            SetBooleanPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "Array[]":
                            SetArrayPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                        case "List":
                            SetListPropertyValue(objectTobeFilled, propertyInfo);
                            break;
                    }
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

        private static void SetListPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {                
                propertyInfo.SetValue(objectTobeFilled, new List<T>(), null);
            }
        }

        private static void SetArrayPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, new Array[1], null);
            }
        }

        private static void SetDecimalPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, Decimal.MaxValue, null);
            }
        }

        private static void SetSinglePropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, Single.MaxValue, null);
            }
        }

        private static void SetObjectPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, new object(), null);
            }
        }

        private static void SetLongPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, long.MaxValue, null);
            }
        }

        private static void SetCharPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, 'x', null);
            }
        }

        private static void SetSbytePropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, SByte.MaxValue, null);
            }
        }

        private static void SetBooleanPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, true, null);
            }
        }

        private static void SetFloatPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                double mantissa = (_random.NextDouble() * 2.0) - 1.0;
                double exponent = Math.Pow(2.0, _random.Next(-126, 128));
                propertyInfo.SetValue(objectTobeFilled, (float)(mantissa * exponent), null);
            }
        }

        private static void SetDoublePropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, _random.NextDouble(), null);
            }
        }

        private static void SetIntegerPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, _random.Next(), null);
            }
        }

        private static void SetDateTimePropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, DateTime.Now, null);
            }
        }

        private static void SetStringPropertyValue<T>(T objectTobeFilled, PropertyInfo propertyInfo)
        {
            if (_isRuleOverrided)
            {
                // todo
            }
            else
            {
                propertyInfo.SetValue(objectTobeFilled, "random_String_" + _random.NextDouble(), null);
            }
        }

        #endregion
    }
}
