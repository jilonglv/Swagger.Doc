
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 16:24:46
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    public class CovertXmlToSwaggerDoc
    {
        private static ConcurrentDictionary<string, SwaggerDocument> _cache =
            new ConcurrentDictionary<string, SwaggerDocument>();
        public static SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            //只读取一次
            if (!_cache.TryGetValue(cacheKey, out srcDoc))
            {
                //srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);
                srcDoc = CreateDocument(rootUrl, apiVersion);
                //string xmlpath = string.Format("{0}/bin/SwaggerDemo.XML", System.AppDomain.CurrentDomain.BaseDirectory);
                var xmlpath = @"C:\Users\joilo\Downloads\SwaggerDemo\SwaggerDemo\bin\SwaggerDemo.XML";
                IncludeXmlComments(srcDoc, xmlpath);
                //GetControllerDesc(srcDoc, xmlpath);

                //srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc(srcDoc,xmlpath) } };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }
        static SwaggerDocument CreateDocument(string rootUrl, string apiVersion)
        {
            return new SwaggerDocument()
            {
                host = rootUrl,
                info = new Info()
                {
                    version = apiVersion,
                    license = new License()
                    {
                        name = "Apache 2.0",
                        url = "http://www.apache.org/licenses/LICENSE-2.0.html"
                    }
                }
            };
        }
        public static void IncludeXmlComments(SwaggerDocument doc, string filePath)
        {
            //var oper= new ApplyXmlActionComments(filePath);
            //oper.Load();
            //var model= new ApplyXmlTypeComments(filePath);
            //this.OperationFilter(() => new ApplyXmlActionComments(filePath));
            //         this.ModelFilter(() => new ApplyXmlTypeComments(filePath));
        }
        /// <summary>
        /// 从API文档中读取控制器描述
        /// </summary>
        /// <returns>所有控制器描述</returns>
        //public static void GetControllerDesc(SwaggerDocument doc, string xmlpath, Func<XmlNode, Tuple<bool, string, string>> func = null)
        //{
        //    //string xmlpath = string.Format("{0}/bin/SwaggerDemo.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        //    ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
        //    if (File.Exists(xmlpath))
        //    {
        //        XmlDocument xmldoc = new XmlDocument();
        //        xmldoc.Load(xmlpath);
        //        string type = string.Empty, path = string.Empty, controllerName = string.Empty;

        //        string[] arrPath;
        //        int length = -1, cCount = "Controller".Length;

        //        var oper = new ApplyXmlActionComments(xmlpath);

        //        foreach (XmlNode node in xmldoc.SelectNodes("//member"))
        //        {
        //            //if (func == null)
        //            //    continue;
        //            //var tuple = func(node);
        //            //if (tuple.Item1)
        //            //    controllerDescDict[tuple.Item2] = tuple.Item3;
        //            type = node.Attributes["name"].Value;
        //            if (type.StartsWith("T:"))
        //            {
        //                //控制器
        //                arrPath = type.Split('.');
        //                length = arrPath.Length;
        //                controllerName = arrPath[length - 1];
        //                if (controllerName.EndsWith("Controller"))
        //                {
        //                    //获取控制器注释
        //                    var summaryNode = node.SelectSingleNode("summary");
        //                    string key = controllerName.Remove(controllerName.Length - cCount, cCount);
        //                    if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
        //                    {
        //                        controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
        //                    }
        //                }
        //            }
        //            else if (type.StartsWith("M:"))
        //            {
        //                CreateMethod(node);
        //                Operation operation = new Operation();
        //                oper.Apply(operation, type);
        //            }
        //        }
        //    }

        //    doc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", controllerDescDict } };

        //    //return controllerDescDict;
        //}
        //static void CreateMethod(XmlNode node)
        //{
        //    PathItem pathItem = new PathItem();
        //    //code
        //    var codeNode = node.SelectSingleNode("code");
        //    if (codeNode != null)
        //    {
        //        var codeText = codeNode.InnerText.Trim().ToLower();
        //        var codeArray = codeText.Split(new char[] { ' ' });
        //        if (codeArray.Length > 0)
        //        {
        //            var methodName = codeArray[0];
        //            switch (methodName)
        //            {
        //                case "get":
        //                    pathItem.get = CreateOperation(apiDesc2, schemaRegistry);
        //                    break;
        //                case "put":
        //                    pathItem.put = CreateOperation(apiDesc2, schemaRegistry);
        //                    break;
        //                case "post":
        //                    pathItem.post = CreateOperation(apiDesc2, schemaRegistry);
        //                    break;
        //                case "delete":
        //                    pathItem.delete = CreateOperation(apiDesc2, schemaRegistry);
        //                    break;
        //                case "options":
        //                    pathItem.options = CreateOperation(apiDesc2, schemaRegistry);
        //                    break;
        //                case "head":
        //                    pathItem.head = CreateOperation(apiDesc2, schemaRegistry);
        //                    break;
        //                case "patch":
        //                    pathItem.patch = CreateOperation(apiDesc2, schemaRegistry);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    var parmNodes = node.SelectNodes("param");
        //    if (parmNodes != null)
        //    {
        //        foreach (XmlNode p in parmNodes)
        //        {
        //            var n= p.Attributes["name"].Value;

        //        }
        //    }
        //}
        //private static PathItem CreatePathItem(IEnumerable<ApiDescription> apiDescriptions, SchemaRegistry schemaRegistry)
        //{
        //    PathItem pathItem = new PathItem();
        //    IEnumerable<IGrouping<string, ApiDescription>> enumerable = from apiDesc in apiDescriptions
        //                                                                group apiDesc by apiDesc.HttpMethod.get_Method().ToLower();
        //    foreach (IGrouping<string, ApiDescription> current in enumerable)
        //    {
        //        string key = current.Key;
        //        ApiDescription apiDesc2 = (current.Count<ApiDescription>() == 1) ? current.First<ApiDescription>() : _options.ConflictingActionsResolver(current);
        //        string key2;
        //        switch (key2 = key)
        //        {
        //            case "get":
        //                pathItem.get = CreateOperation(apiDesc2, schemaRegistry);
        //                break;
        //            case "put":
        //                pathItem.put = CreateOperation(apiDesc2, schemaRegistry);
        //                break;
        //            case "post":
        //                pathItem.post = CreateOperation(apiDesc2, schemaRegistry);
        //                break;
        //            case "delete":
        //                pathItem.delete = CreateOperation(apiDesc2, schemaRegistry);
        //                break;
        //            case "options":
        //                pathItem.options = CreateOperation(apiDesc2, schemaRegistry);
        //                break;
        //            case "head":
        //                pathItem.head = CreateOperation(apiDesc2, schemaRegistry);
        //                break;
        //            case "patch":
        //                pathItem.patch = CreateOperation(apiDesc2, schemaRegistry);
        //                break;
        //        }
        //    }
        //    return pathItem;
        //}
        //private static Operation CreateOperation(ApiDescription apiDesc, SchemaRegistry schemaRegistry)
        //{
        //    List<Parameter> list = apiDesc.ParameterDescriptions.Select(delegate (ApiParameterDescription paramDesc)
        //    {
        //        string parameterLocation = GetParameterLocation(apiDesc, paramDesc);
        //        return CreateParameter(parameterLocation, paramDesc, schemaRegistry);
        //    }).ToList<Parameter>();
        //    Dictionary<string, Response> dictionary = new Dictionary<string, Response>();
        //    Type type = apiDesc.ResponseType();
        //    if (type == null || type == typeof(void))
        //    {
        //        dictionary.Add("204", new Response
        //        {
        //            description = "No Content"
        //        });
        //    }
        //    else
        //    {
        //        dictionary.Add("200", new Response
        //        {
        //            description = "OK",
        //            schema = schemaRegistry.GetOrRegister(type)
        //        });
        //    }
        //    Operation operation = new Operation
        //    {
        //        tags = new string[]
        //        {
        //            _options.GroupingKeySelector(apiDesc)
        //        },
        //        operationId = apiDesc.FriendlyId(),
        //        produces = apiDesc.Produces().ToList<string>(),
        //        consumes = apiDesc.Consumes().ToList<string>(),
        //        parameters = list.Any<Parameter>() ? list : null,
        //        responses = dictionary,
        //        deprecated = apiDesc.IsObsolete()
        //    };
        //    foreach (IOperationFilter current in _options.OperationFilters)
        //    {
        //        current.Apply(operation, schemaRegistry, apiDesc);
        //    }
        //    return operation;
        //}
        //private static string GetParameterLocation(ApiDescription apiDesc, ApiParameterDescription paramDesc)
        //{
        //    if (apiDesc.RelativePathSansQueryString().Contains("{" + paramDesc.Name + "}"))
        //    {
        //        return "path";
        //    }
        //    if (paramDesc.Source == ApiParameterSource.FromBody && apiDesc.HttpMethod != HttpMethod.get_Get())
        //    {
        //        return "body";
        //    }
        //    return "query";
        //}
        //private static Parameter CreateParameter(string location, ApiParameterDescription paramDesc, SchemaRegistry schemaRegistry)
        //{
        //    Parameter parameter = new Parameter
        //    {
        //        @in = location,
        //        name = paramDesc.Name
        //    };
        //    if (paramDesc.ParameterDescriptor == null)
        //    {
        //        parameter.type = "string";
        //        parameter.required = new bool?(true);
        //        return parameter;
        //    }
        //    parameter.required = new bool?(location == "path" || !paramDesc.ParameterDescriptor.IsOptional);
        //    parameter.@default = paramDesc.ParameterDescriptor.DefaultValue;
        //    Schema orRegister = schemaRegistry.GetOrRegister(paramDesc.ParameterDescriptor.ParameterType);
        //    if (parameter.@in == "body")
        //    {
        //        parameter.schema = orRegister;
        //    }
        //    else
        //    {
        //        parameter.PopulateFrom(orRegister);
        //    }
        //    return parameter;
        //}
        public class ApiDescription
        {
        }
        public class ApiParameterDescription
        {
        }
    }
}
