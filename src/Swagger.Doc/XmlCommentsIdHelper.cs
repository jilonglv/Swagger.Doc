
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:59:43
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

	public class XmlCommentsIdHelper
	{
		public static string GetCommentIdForMethod(MethodInfo methodInfo)
		{
			StringBuilder stringBuilder = new StringBuilder("M:");
			XmlCommentsIdHelper.AppendFullTypeName(methodInfo.DeclaringType, stringBuilder, false);
			stringBuilder.Append(".");
			XmlCommentsIdHelper.AppendMethodName(methodInfo, stringBuilder);
			return stringBuilder.ToString();
		}

		public static string GetCommentIdForType(Type type)
		{
			StringBuilder stringBuilder = new StringBuilder("T:");
			XmlCommentsIdHelper.AppendFullTypeName(type, stringBuilder, false);
			return stringBuilder.ToString();
		}

		public static string GetCommentIdForProperty(PropertyInfo propertyInfo)
		{
			StringBuilder stringBuilder = new StringBuilder("P:");
			XmlCommentsIdHelper.AppendFullTypeName(propertyInfo.DeclaringType, stringBuilder, false);
			stringBuilder.Append(".");
			XmlCommentsIdHelper.AppendPropertyName(propertyInfo, stringBuilder);
			return stringBuilder.ToString();
		}

		private static void AppendFullTypeName(Type type, StringBuilder builder, bool expandGenericArgs = false)
		{
			builder.Append(type.Namespace);
			builder.Append(".");
			XmlCommentsIdHelper.AppendTypeName(type, builder, expandGenericArgs);
		}

		private static void AppendTypeName(Type type, StringBuilder builder, bool expandGenericArgs)
		{
			if (type.IsNested)
			{
				XmlCommentsIdHelper.AppendTypeName(type.DeclaringType, builder, false);
				builder.Append(".");
			}
			builder.Append(type.Name);
			if (expandGenericArgs)
			{
				XmlCommentsIdHelper.ExpandGenericArgsIfAny(type, builder);
			}
		}

		public static void ExpandGenericArgsIfAny(Type type, StringBuilder builder)
		{
			if (type.IsGenericType)
			{
				StringBuilder stringBuilder = new StringBuilder("{");
				Type[] genericArguments = type.GetGenericArguments();
				Type[] array = genericArguments;
				for (int i = 0; i < array.Length; i++)
				{
					Type type2 = array[i];
					XmlCommentsIdHelper.AppendFullTypeName(type2, stringBuilder, true);
					stringBuilder.Append(",");
				}
				stringBuilder.Replace(",", "}", stringBuilder.Length - 1, 1);
				builder.Replace(string.Format("`{0}", genericArguments.Length), stringBuilder.ToString());
				return;
			}
			if (type.IsArray)
			{
				XmlCommentsIdHelper.ExpandGenericArgsIfAny(type.GetElementType(), builder);
			}
		}

		private static void AppendMethodName(MethodInfo methodInfo, StringBuilder builder)
		{
			builder.Append(methodInfo.Name);
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length == 0)
			{
				return;
			}
			builder.Append("(");
			ParameterInfo[] array = parameters;
			for (int i = 0; i < array.Length; i++)
			{
				ParameterInfo parameterInfo = array[i];
				XmlCommentsIdHelper.AppendFullTypeName(parameterInfo.ParameterType, builder, true);
				builder.Append(",");
			}
			builder.Replace(",", ")", builder.Length - 1, 1);
		}

		private static void AppendPropertyName(PropertyInfo propertyInfo, StringBuilder builder)
		{
			builder.Append(propertyInfo.Name);
		}
	}
}
