
/******************************************************
* author :  jilonglv
* email  : jilonglv@gmail.com
* function:
* history:  created by jilonglv 2020/6/12 18:48:50
* clrversion :4.0.30319.42000
******************************************************/

namespace Swagger.Doc
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

	public class SchemaRegistry
	{
		private class SchemaInfo
		{
			public string SchemaId;

			public Schema Schema;
		}

		private readonly JsonSerializerSettings _jsonSerializerSettings;

		private readonly IDictionary<Type, Func<Schema>> _customSchemaMappings;

		private readonly IEnumerable<ISchemaFilter> _schemaFilters;

		private readonly IEnumerable<IModelFilter> _modelFilters;

		private readonly Func<Type, string> _schemaIdSelector;

		private readonly bool _ignoreObsoleteProperties;

		private readonly bool _describeAllEnumsAsStrings;

		private readonly bool _describeStringEnumsInCamelCase;

		private readonly IContractResolver _contractResolver;

		private IDictionary<Type, SchemaRegistry.SchemaInfo> _referencedTypes;

		public IDictionary<string, Schema> Definitions
		{
			get;
			private set;
		}

		public SchemaRegistry(JsonSerializerSettings jsonSerializerSettings, IDictionary<Type, Func<Schema>> customSchemaMappings, IEnumerable<ISchemaFilter> schemaFilters, IEnumerable<IModelFilter> modelFilters, bool ignoreObsoleteProperties, Func<Type, string> schemaIdSelector, bool describeAllEnumsAsStrings, bool describeStringEnumsInCamelCase)
		{
			this._jsonSerializerSettings = jsonSerializerSettings;
			this._customSchemaMappings = customSchemaMappings;
			this._schemaFilters = schemaFilters;
			this._modelFilters = modelFilters;
			this._schemaIdSelector = schemaIdSelector;
			this._ignoreObsoleteProperties = ignoreObsoleteProperties;
			this._describeAllEnumsAsStrings = describeAllEnumsAsStrings;
			this._describeStringEnumsInCamelCase = describeStringEnumsInCamelCase;
			this._contractResolver = (jsonSerializerSettings.ContractResolver ?? new DefaultContractResolver());
			this._referencedTypes = new Dictionary<Type, SchemaRegistry.SchemaInfo>();
			this.Definitions = new Dictionary<string, Schema>();
		}

		public Schema GetOrRegister(Type type)
		{
			Schema result = this.CreateInlineSchema(type);
			while (true)
			{
				if (!this._referencedTypes.Any((KeyValuePair<Type, SchemaRegistry.SchemaInfo> entry) => entry.Value.Schema == null))
				{
					break;
				}
				KeyValuePair<Type, SchemaRegistry.SchemaInfo> keyValuePair = this._referencedTypes.First((KeyValuePair<Type, SchemaRegistry.SchemaInfo> entry) => entry.Value.Schema == null);
				SchemaRegistry.SchemaInfo value = keyValuePair.Value;
				value.Schema = this.CreateDefinitionSchema(keyValuePair.Key);
				this.Definitions.Add(value.SchemaId, value.Schema);
			}
			return result;
		}

		private Schema CreateInlineSchema(Type type)
		{
			if (this._customSchemaMappings.ContainsKey(type))
			{
				return this._customSchemaMappings[type]();
			}
			JsonContract jsonContract = this._contractResolver.ResolveContract(type);
			if (jsonContract is JsonPrimitiveContract)
			{
				return this.CreatePrimitiveSchema((JsonPrimitiveContract)jsonContract);
			}
			JsonDictionaryContract jsonDictionaryContract = jsonContract as JsonDictionaryContract;
			if (jsonDictionaryContract != null)
			{
				if (!jsonDictionaryContract.IsSelfReferencing())
				{
					return this.CreateDictionarySchema(jsonDictionaryContract);
				}
				return this.CreateRefSchema(type);
			}
			else
			{
				JsonArrayContract jsonArrayContract = jsonContract as JsonArrayContract;
				if (jsonArrayContract != null)
				{
					if (!jsonArrayContract.IsSelfReferencing())
					{
						return this.CreateArraySchema(jsonArrayContract);
					}
					return this.CreateRefSchema(type);
				}
				else
				{
					JsonObjectContract jsonObjectContract = jsonContract as JsonObjectContract;
					if (jsonObjectContract != null && jsonObjectContract.IsInferrable())
					{
						return this.CreateRefSchema(type);
					}
					return this.CreateRefSchema(typeof(object));
				}
			}
		}

		private Schema CreateDefinitionSchema(Type type)
		{
			JsonContract jsonContract = this._contractResolver.ResolveContract(type);
			if (jsonContract is JsonDictionaryContract)
			{
				return this.CreateDictionarySchema((JsonDictionaryContract)jsonContract);
			}
			if (jsonContract is JsonArrayContract)
			{
				return this.CreateArraySchema((JsonArrayContract)jsonContract);
			}
			if (jsonContract is JsonObjectContract)
			{
				return this.CreateObjectSchema((JsonObjectContract)jsonContract);
			}
			throw new InvalidOperationException(string.Format("Unsupported type - {0} for Defintitions. Must be Dictionary, Array or Object", type));
		}

		private Schema CreatePrimitiveSchema(JsonPrimitiveContract primitiveContract)
		{
			Type type = Nullable.GetUnderlyingType(primitiveContract.UnderlyingType) ?? primitiveContract.UnderlyingType;
			if (type.IsEnum)
			{
				return this.CreateEnumSchema(primitiveContract, type);
			}
			string fullName;
			switch (fullName = type.FullName)
			{
				case "System.Byte":
				case "System.SByte":
				case "System.Int16":
				case "System.UInt16":
				case "System.Int32":
				case "System.UInt32":
					return new Schema
					{
						type = "integer",
						format = "int32"
					};
				case "System.Int64":
				case "System.UInt64":
					return new Schema
					{
						type = "integer",
						format = "int64"
					};
				case "System.Single":
					return new Schema
					{
						type = "number",
						format = "float"
					};
				case "System.Double":
				case "System.Decimal":
					return new Schema
					{
						type = "number",
						format = "double"
					};
				case "System.Byte[]":
				case "System.SByte[]":
					return new Schema
					{
						type = "string",
						format = "byte"
					};
				case "System.Boolean":
					return new Schema
					{
						type = "boolean"
					};
				case "System.DateTime":
				case "System.DateTimeOffset":
					return new Schema
					{
						type = "string",
						format = "date-time"
					};
			}
			return new Schema
			{
				type = "string"
			};
		}

		private Schema CreateEnumSchema(JsonPrimitiveContract primitiveContract, Type type)
		{
			StringEnumConverter stringEnumConverter = (primitiveContract.Converter as StringEnumConverter) ?? this._jsonSerializerSettings.Converters.OfType<StringEnumConverter>().FirstOrDefault<StringEnumConverter>();
			if (this._describeAllEnumsAsStrings || stringEnumConverter != null)
			{
				bool flag = this._describeStringEnumsInCamelCase || (stringEnumConverter != null && stringEnumConverter.CamelCaseText);
				Schema schema = new Schema();
				schema.type = "string";
				Schema arg_92_0 = schema;
				IList<object> arg_92_1;
				if (!flag)
				{
					arg_92_1 = type.GetEnumNamesForSerialization();
				}
				else
				{
					arg_92_1 = (from name in type.GetEnumNamesForSerialization()
								select name.ToCamelCase()).ToArray<string>();
				}
				arg_92_0.@enum = arg_92_1;
				return schema;
			}
			return new Schema
			{
				type = "integer",
				format = "int32",
				@enum = type.GetEnumValues().Cast<object>().ToArray<object>()
			};
		}

		private Schema CreateDictionarySchema(JsonDictionaryContract dictionaryContract)
		{
			Type type = dictionaryContract.DictionaryValueType ?? typeof(object);
			return new Schema
			{
				type = "object",
				additionalProperties = this.CreateInlineSchema(type)
			};
		}

		private Schema CreateArraySchema(JsonArrayContract arrayContract)
		{
			Type type = arrayContract.CollectionItemType ?? typeof(object);
			return new Schema
			{
				type = "array",
				items = this.CreateInlineSchema(type)
			};
		}

		private Schema CreateObjectSchema(JsonObjectContract jsonContract)
		{
			Dictionary<string, Schema> properties = (from p in jsonContract.Properties
													 where !p.Ignored
													 where !this._ignoreObsoleteProperties || !p.IsObsolete()
													 select p).ToDictionary((JsonProperty prop) => prop.PropertyName, (JsonProperty prop) => this.CreateInlineSchema(prop.PropertyType).WithValidationProperties(prop));
			List<string> list = (from prop in jsonContract.Properties
								 where prop.IsRequired()
								 select prop into propInfo
								 select propInfo.PropertyName).ToList<string>();
			Schema schema = new Schema
			{
				required = list.Any<string>() ? list : null,
				properties = properties,
				type = "object"
			};
			foreach (ISchemaFilter current in this._schemaFilters)
			{
				current.Apply(schema, this, jsonContract.UnderlyingType);
			}
			ModelFilterContext context = new ModelFilterContext(jsonContract.UnderlyingType, jsonContract, this);
			foreach (IModelFilter current2 in this._modelFilters)
			{
				current2.Apply(schema, context);
			}
			return schema;
		}

		private Schema CreateRefSchema(Type type)
		{
			if (!this._referencedTypes.ContainsKey(type))
			{
				string schemaId = this._schemaIdSelector(type);
				if (this._referencedTypes.Any((KeyValuePair<Type, SchemaRegistry.SchemaInfo> entry) => entry.Value.SchemaId == schemaId))
				{
					Type key = this._referencedTypes.First((KeyValuePair<Type, SchemaRegistry.SchemaInfo> entry) => entry.Value.SchemaId == schemaId).Key;
					throw new InvalidOperationException(string.Format("Conflicting schemaIds: Duplicate schemaIds detected for types {0} and {1}. See the config setting - \"UseFullTypeNameInSchemaIds\" for a potential workaround", type.FullName, key.FullName));
				}
				this._referencedTypes.Add(type, new SchemaRegistry.SchemaInfo
				{
					SchemaId = schemaId
				});
			}
			return new Schema
			{
				@ref = "#/definitions/" + this._referencedTypes[type].SchemaId
			};
		}
	}
}
