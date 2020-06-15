
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:51:51
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISchemaFilter
    {
        void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type);
    }
}
