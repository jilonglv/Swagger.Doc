
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:54:26
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;

	public static class TypeExtensions
	{
		public static string FriendlyId(this Type type, bool fullyQualified = false)
		{
			string text = fullyQualified ? type.FullNameSansTypeParameters().Replace("+", ".") : type.Name;
			if (type.IsGenericType)
			{
				string[] array = (from t in type.GetGenericArguments()
								  select t.FriendlyId(fullyQualified)).ToArray<string>();
				return new StringBuilder(text).Replace(string.Format("`{0}", array.Count<string>()), string.Empty).Append(string.Format("[{0}]", string.Join(",", array).TrimEnd(new char[]
				{
					','
				}))).ToString();
			}
			return text;
		}

		public static string FullNameSansTypeParameters(this Type type)
		{
			string text = type.FullName;
			if (string.IsNullOrEmpty(text))
			{
				text = type.Name;
			}
			int num = text.IndexOf("[[");
			if (num != -1)
			{
				return text.Substring(0, num);
			}
			return text;
		}

		public static string[] GetEnumNamesForSerialization(this Type enumType)
		{
			return enumType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Select(delegate (FieldInfo fieldInfo)
			{
				EnumMemberAttribute enumMemberAttribute = fieldInfo.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault<EnumMemberAttribute>();
				if (enumMemberAttribute != null && !string.IsNullOrWhiteSpace(enumMemberAttribute.Value))
				{
					return enumMemberAttribute.Value;
				}
				return fieldInfo.Name;
			}).ToArray<string>();
		}
	}
}
