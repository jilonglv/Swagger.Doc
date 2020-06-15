
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:20:56
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

	public class Response
	{
		public string description;

		public Schema schema;

		public IDictionary<string, Header> headers;

		public object examples;
	}
}
