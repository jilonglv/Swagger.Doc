
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 19:13:53
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

	internal static class StringExtensions
	{
		internal static string ToCamelCase(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
		}
	}
}
