
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:02:28
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class Info
	{
		public string version;

		public string title;

		public string description;

		public string termsOfService;

		public Contact contact;

		public License license;

		public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
	}
}
