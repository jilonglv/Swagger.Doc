
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 19:14:27
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

	public static class SchemaExtensions
	{
		public static Schema WithValidationProperties(this Schema schema, JsonProperty jsonProperty)
		{
			PropertyInfo propertyInfo = jsonProperty.PropertyInfo();
			if (propertyInfo == null)
			{
				return schema;
			}
			object[] customAttributes = propertyInfo.GetCustomAttributes(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				//object obj = customAttributes[i];
				//RegularExpressionAttribute regularExpressionAttribute = obj as RegularExpressionAttribute;
				//if (regularExpressionAttribute != null)
				//{
				//	schema.pattern = regularExpressionAttribute.Pattern;
				//}
				//RangeAttribute rangeAttribute = obj as RangeAttribute;
				//if (rangeAttribute != null)
				//{
				//	int value;
				//	if (int.TryParse(rangeAttribute.Maximum.ToString(), out value))
				//	{
				//		schema.maximum = new int?(value);
				//	}
				//	int value2;
				//	if (int.TryParse(rangeAttribute.Minimum.ToString(), out value2))
				//	{
				//		schema.minimum = new int?(value2);
				//	}
				//}
				//StringLengthAttribute stringLengthAttribute = obj as StringLengthAttribute;
				//if (stringLengthAttribute != null)
				//{
				//	schema.maxLength = new int?(stringLengthAttribute.MaximumLength);
				//	schema.minLength = new int?(stringLengthAttribute.MinimumLength);
				//}
			}
			if (!jsonProperty.Writable)
			{
				schema.readOnly = new bool?(true);
			}
			return schema;
		}

		public static void PopulateFrom(this PartialSchema partialSchema, Schema schema)
		{
			if (schema == null)
			{
				return;
			}
			partialSchema.type = schema.type;
			partialSchema.format = schema.format;
			if (schema.items != null)
			{
				partialSchema.items = new PartialSchema();
				partialSchema.items.PopulateFrom(schema.items);
			}
			partialSchema.@default = schema.@default;
			partialSchema.maximum = schema.maximum;
			partialSchema.exclusiveMaximum = schema.exclusiveMaximum;
			partialSchema.minimum = schema.minimum;
			partialSchema.exclusiveMinimum = schema.exclusiveMinimum;
			partialSchema.maxLength = schema.maxLength;
			partialSchema.minLength = schema.minLength;
			partialSchema.pattern = schema.pattern;
			partialSchema.maxItems = schema.maxItems;
			partialSchema.minItems = schema.minItems;
			partialSchema.uniqueItems = schema.uniqueItems;
			partialSchema.@enum = schema.@enum;
			partialSchema.multipleOf = schema.multipleOf;
		}
	}
}
