using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xrm.DevOps.Data.SupportClasses
{
    static class SupportMethods
    {
        public static void SetSealedPropertyValue(this EntityMetadata entityMetadata, string sPropertyName, object value)
        {
            entityMetadata.GetType().GetProperty(sPropertyName).SetValue(entityMetadata, value, null);
        }

        public static void SetSealedPropertyValue(this AttributeMetadata attributeMetadata, string sPropertyName, object value)
        {
            attributeMetadata.GetType().GetProperty(sPropertyName).SetValue(attributeMetadata, value, null);
        }

        public static void SetFieldValue(this object inputObject, string propertyName, object propertyVal)
        {
            Type type = inputObject.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
            Type propertyType = propertyInfo.PropertyType;
            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
            propertyVal = Convert.ChangeType(propertyVal, targetType);
            propertyInfo.SetValue(inputObject, propertyVal, null);
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}
