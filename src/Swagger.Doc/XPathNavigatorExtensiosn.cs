
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 19:01:43
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.XPath;

    public static class XPathNavigatorExtensiosn
	{
		private static Regex ParamPattern = new Regex("<(see|paramref) (name|cref)=\"([TPF]{1}:)?(?<display>.+?)\" />");

		private static Regex ConstPattern = new Regex("<c>(?<display>.+?)</c>");

		public static string ExtractContent(this XPathNavigator node)
		{
			if (node == null)
			{
				return null;
			}
			return XPathNavigatorExtensiosn.ConstPattern.Replace(XPathNavigatorExtensiosn.ParamPattern.Replace(node.InnerXml, new MatchEvaluator(XPathNavigatorExtensiosn.GetParamRefName)), new MatchEvaluator(XPathNavigatorExtensiosn.GetConstRefName)).Trim();
		}

		private static string GetConstRefName(Match match)
		{
			if (match.Groups.Count != 2)
			{
				return null;
			}
			return match.Groups["display"].Value;
		}

		private static string GetParamRefName(Match match)
		{
			if (match.Groups.Count != 5)
			{
				return null;
			}
			return "{" + match.Groups["display"].Value + "}";
		}
	}
}
