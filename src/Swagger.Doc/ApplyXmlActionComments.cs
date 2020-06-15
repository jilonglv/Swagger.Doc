
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:45:30
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.XPath;

    public class ApplyXmlActionComments : IOperationFilter
    {
		private const string MemberXPath = "/doc/members/member[@name='{0}']";

		private const string SummaryTag = "summary";

		private const string RemarksTag = "remarks";

		private const string ParameterTag = "param";

		private const string ResponseTag = "response";

		private readonly XPathNavigator _navigator;

		public ApplyXmlActionComments(string xmlCommentsPath)
		{
			this._navigator = new XPathDocument(xmlCommentsPath).CreateNavigator();
		}
		public void Load()
		{
			//member
			//if (_navigator.Count > 0)
			//{
			//	Response value = operation.responses.First<KeyValuePair<string, Response>>().Value;
			//	operation.responses.Clear();
			//	while (_navigator.MoveNext())
			//	{
			//		string attribute = xPathNodeIterator.Current.GetAttribute("code", "");
			//		string description = xPathNodeIterator.Current.ExtractContent();
			//		Response value2 = new Response
			//		{
			//			description = description,
			//			schema = attribute.StartsWith("2") ? value.schema : null
			//		};
			//		operation.responses[attribute] = value2;
			//	}
			//}
		}
		public void Apply(Operation operation, string commentIdForMethod /*,SchemaRegistry schemaRegistry, ApiDescription apiDescription*/)
		{
            //ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = apiDescription.ActionDescriptor as ReflectedHttpActionDescriptor;
            //if (reflectedHttpActionDescriptor == null)
            //{
            //    return;
            //}
            //string commentIdForMethod = XmlCommentsIdHelper.GetCommentIdForMethod(reflectedHttpActionDescriptor.MethodInfo);
            XPathNavigator xPathNavigator = this._navigator.SelectSingleNode(string.Format("/doc/members/member[@name='{0}']", commentIdForMethod));
            if (xPathNavigator == null)
            {
                return;
            }
            XPathNavigator xPathNavigator2 = xPathNavigator.SelectSingleNode("summary");
            if (xPathNavigator2 != null)
            {
                operation.summary = xPathNavigator2.ExtractContent();
            }
            XPathNavigator xPathNavigator3 = xPathNavigator.SelectSingleNode("remarks");
            if (xPathNavigator3 != null)
            {
                operation.description = xPathNavigator3.ExtractContent();
            }
            ApplyXmlActionComments.ApplyParamComments(operation, xPathNavigator);
            ApplyXmlActionComments.ApplyResponseComments(operation, xPathNavigator);
        }

		private static void ApplyParamComments(Operation operation, XPathNavigator methodNode)
		{
			if (operation.parameters == null)
			{
				return;
			}
			XPathNodeIterator xPathNodeIterator = methodNode.Select("param");
			while (xPathNodeIterator.MoveNext())
			{
				XPathNavigator paramNode = xPathNodeIterator.Current;
				Parameter parameter = operation.parameters.SingleOrDefault((Parameter param) => param.name == paramNode.GetAttribute("name", ""));
				if (parameter != null)
				{
					parameter.description = paramNode.ExtractContent();
				}
			}
		}

		private static void ApplyResponseComments(Operation operation, XPathNavigator methodNode)
		{
			XPathNodeIterator xPathNodeIterator = methodNode.Select("response");
			if (xPathNodeIterator.Count > 0)
			{
				Response value = operation.responses.First<KeyValuePair<string, Response>>().Value;
				operation.responses.Clear();
				while (xPathNodeIterator.MoveNext())
				{
					string attribute = xPathNodeIterator.Current.GetAttribute("code", "");
					string description = xPathNodeIterator.Current.ExtractContent();
					Response value2 = new Response
					{
						description = description,
						schema = attribute.StartsWith("2") ? value.schema : null
					};
					operation.responses[attribute] = value2;
				}
			}
		}
	}
}
