
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:53:13
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

	public static class JsonPropertyExtensions
	{
		public static bool IsRequired(this JsonProperty jsonProperty)
		{
			return false;
			//return jsonProperty.HasAttribute<RequiredAttribute>();
		}

		public static bool IsObsolete(this JsonProperty jsonProperty)
		{
			return jsonProperty.HasAttribute<ObsoleteAttribute>();
		}

		public static bool HasAttribute<T>(this JsonProperty jsonProperty)
		{
			PropertyInfo propertyInfo = jsonProperty.PropertyInfo();
			return propertyInfo != null && Attribute.IsDefined(propertyInfo, typeof(T));
		}

		public static PropertyInfo PropertyInfo(this JsonProperty jsonProperty)
		{
			if (jsonProperty.UnderlyingName == null)
			{
				return null;
			}
			return jsonProperty.DeclaringType.GetProperty(jsonProperty.UnderlyingName, jsonProperty.PropertyType);
		}
	}
}
