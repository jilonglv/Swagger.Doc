
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:49:41
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Text;

	public class ModelFilterContext
	{
		public Type SystemType
		{
			get;
			private set;
		}

		public JsonObjectContract JsonObjectContract
		{
			get;
			private set;
		}

		public SchemaRegistry SchemaRegistry
		{
			get;
			private set;
		}

		public ModelFilterContext(Type systemType, JsonObjectContract jsonObjectContract, SchemaRegistry schemaRegistry)
		{
			this.SystemType = systemType;
			this.JsonObjectContract = jsonObjectContract;
			this.SchemaRegistry = schemaRegistry;
		}
	}
}
