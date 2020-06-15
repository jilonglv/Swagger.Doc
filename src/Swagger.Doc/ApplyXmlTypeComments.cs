
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:58:30
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Xml.XPath;

    public class ApplyXmlTypeComments : IModelFilter
	{
		private const string MemberXPath = "/doc/members/member[@name='{0}']";

		private const string SummaryTag = "summary";

		private readonly XPathNavigator _navigator;

		public ApplyXmlTypeComments(string xmlCommentsPath)
		{
			this._navigator = new XPathDocument(xmlCommentsPath).CreateNavigator();
		}

		public void Apply(Schema model, ModelFilterContext context)
		{
			string commentIdForType = XmlCommentsIdHelper.GetCommentIdForType(context.SystemType);
			XPathNavigator xPathNavigator = this._navigator.SelectSingleNode(string.Format("/doc/members/member[@name='{0}']", commentIdForType));
			if (xPathNavigator != null)
			{
				XPathNavigator xPathNavigator2 = xPathNavigator.SelectSingleNode("summary");
				if (xPathNavigator2 != null)
				{
					model.description = xPathNavigator2.ExtractContent();
				}
			}
			foreach (KeyValuePair<string, Schema> current in model.properties)
			{
				JsonProperty jsonProperty = context.JsonObjectContract.Properties[current.Key];
				if (jsonProperty != null)
				{
					this.ApplyPropertyComments(current.Value, jsonProperty.PropertyInfo());
				}
			}
		}

		private void ApplyPropertyComments(Schema propertySchema, PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				return;
			}
			string commentIdForProperty = XmlCommentsIdHelper.GetCommentIdForProperty(propertyInfo);
			XPathNavigator xPathNavigator = this._navigator.SelectSingleNode(string.Format("/doc/members/member[@name='{0}']", commentIdForProperty));
			if (xPathNavigator == null)
			{
				return;
			}
			XPathNavigator xPathNavigator2 = xPathNavigator.SelectSingleNode("summary");
			if (xPathNavigator2 != null)
			{
				propertySchema.description = xPathNavigator2.ExtractContent();
			}
		}
	}
}
