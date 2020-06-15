
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 19:05:38
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IOperationFilter
    {
        void Apply(Operation operation,string commentIdForMethod /*SchemaRegistry schemaRegistry, ApiDescription apiDescription*/);
    }
}
