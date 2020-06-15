
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:21:25
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

	public class Tag
	{
		public string name;

		public string description;

		public ExternalDocs externalDocs;

		public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
	}
}
