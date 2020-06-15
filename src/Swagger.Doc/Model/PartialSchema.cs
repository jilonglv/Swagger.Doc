
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:18:46
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

	public class PartialSchema
	{
		public string type;

		public string format;

		public PartialSchema items;

		public string collectionFormat;

		public object @default;

		public int? maximum;

		public bool? exclusiveMaximum;

		public int? minimum;

		public bool? exclusiveMinimum;

		public int? maxLength;

		public int? minLength;

		public string pattern;

		public int? maxItems;

		public int? minItems;

		public bool? uniqueItems;

		public IList<object> @enum;

		public int? multipleOf;
	}
}
