using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Serialization;
using Main.Tools.TestClient.Variables;

namespace Main.Tools.TestClient
{
	// Token: 0x0200000A RID: 10
	internal class DataContractAnalyzer : MarshalByRefObject
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00002A33 File Offset: 0x00000C33
		private static bool ShouldCache(object value)
		{
			return !(value is string);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005114 File Offset: 0x00003314
		internal static Variable BuildVariableInfo(string name, object value)
		{
			if (value == null)
			{
				value = new NullObject();
			}
			if (DataContractAnalyzer.ObjectsCache.ContainsKey(value))
			{
				return DataContractAnalyzer.ObjectsCache[value];
			}
			Type type = value.GetType();
			string fullName = type.FullName;
			SerializableType serializableType;
			if (!DataContractAnalyzer.serviceTypeInfoPool.TryGetValue(fullName, out serializableType))
			{
				serializableType = DataContractAnalyzer.CreateServiceTypeInfo(type);
			}
			TypeMemberInfo typeMemberInfo = new TypeMemberInfo(name, serializableType);
			Variable variable = VariableFactory.CreateAssociateVariable(typeMemberInfo, value);
			if (DataContractAnalyzer.ShouldCache(value))
			{
				DataContractAnalyzer.ObjectsCache.Add(value, variable);
			}
			if (typeMemberInfo.Members != null)
			{
				Variable[] array2;
				if (type.IsArray)
				{
					Array array = (Array)value;
					array2 = new Variable[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						object value2 = array.GetValue(i);
						array2[i] = DataContractAnalyzer.BuildVariableInfo("[" + i.ToString() + "]", value2);
					}
				}
				else
				{
					if (DataContractAnalyzer.IsCollectionType(type))
					{
						ICollection collection = (ICollection)value;
						array2 = new Variable[collection.Count];
						int num = 0;
						IEnumerator enumerator = collection.GetEnumerator();
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								array2[num++] = DataContractAnalyzer.BuildVariableInfo("[" + num.ToString() + "]", obj);
							}
							goto IL_02A9;
						}
					}
					if (DataContractAnalyzer.IsDictionaryType(type))
					{
						IDictionary dictionary = (IDictionary)value;
						array2 = new Variable[dictionary.Count];
						int num2 = 0;
						IDictionaryEnumerator enumerator2 = dictionary.GetEnumerator();
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
								array2[num2++] = DataContractAnalyzer.BuildVariableInfo("[" + num2.ToString() + "]", dictionaryEntry);
							}
							goto IL_02A9;
						}
					}
					array2 = new Variable[typeMemberInfo.Members.Count];
					int num3 = 0;
					foreach (PropertyInfo propertyInfo in type.GetProperties())
					{
						if (DataContractAnalyzer.IsSupportedMember(propertyInfo) || value is DictionaryEntry || DataContractAnalyzer.IsKeyValuePairType(type))
						{
							object value3 = propertyInfo.GetValue(value, null);
							array2[num3++] = DataContractAnalyzer.BuildVariableInfo(propertyInfo.Name, value3);
						}
					}
					foreach (FieldInfo fieldInfo in type.GetFields())
					{
						if (DataContractAnalyzer.IsSupportedMember(fieldInfo))
						{
							object value4 = fieldInfo.GetValue(value);
							array2[num3++] = DataContractAnalyzer.BuildVariableInfo(fieldInfo.Name, value4);
						}
					}
				}
				IL_02A9:
				variable.SetChildVariables(array2);
			}
			return variable;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000053F4 File Offset: 0x000035F4
		internal static Variable[] BuildVariableInfos(object result, IDictionary<string, object> outValues)
		{
			Variable[] array = new Variable[outValues.Count + 1];
			DataContractAnalyzer.ObjectsCache.Clear();
			array[0] = DataContractAnalyzer.BuildVariableInfo("(return)", result);
			int num = 1;
			foreach (KeyValuePair<string, object> keyValuePair in outValues)
			{
				array[num++] = DataContractAnalyzer.BuildVariableInfo(keyValuePair.Key, keyValuePair.Value);
			}
			return array;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002A41 File Offset: 0x00000C41
		internal static bool IsCollectionType(Type currentType)
		{
			return currentType.GetCustomAttributes(typeof(CollectionDataContractAttribute), true).Length != 0;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005478 File Offset: 0x00003678
		internal static bool IsDataSet(Type type)
		{
			Type typeFromHandle = typeof(DataSet);
			return type.Equals(typeFromHandle) || typeFromHandle.IsAssignableFrom(type);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002A58 File Offset: 0x00000C58
		internal static bool IsDictionaryType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<, >);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002A7C File Offset: 0x00000C7C
		internal static bool IsKeyValuePairType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002AA0 File Offset: 0x00000CA0
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000054A4 File Offset: 0x000036A4
		internal static bool IsSupportedType(Type currentType)
		{
			if (currentType == typeof(DictionaryEntry))
			{
				return true;
			}
			foreach (Type type in DataContractAnalyzer.typeAttributes)
			{
				if (DataContractAnalyzer.HasAttribute(currentType, type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000054EC File Offset: 0x000036EC
		internal ClientEndpointInfo AnalyzeDataContract(ClientEndpointInfo endpoint)
		{
			Assembly clientAssembly = ClientSettings.ClientAssembly;
			Type type = clientAssembly.GetType(endpoint.OperationContractTypeName);
			if (type == null)
			{
				endpoint.Valid = false;
				return endpoint;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(ServiceContractAttribute), true);
			if (customAttributes != null && customAttributes.Length == 1 && ((ServiceContractAttribute)customAttributes[0]).CallbackContract != null)
			{
				endpoint.Valid = false;
			}
			else
			{
				endpoint.Valid = true;
			}
			endpoint.ClientTypeName = DataContractAnalyzer.GetContractTypeName(type);
			foreach (MethodInfo methodInfo in type.GetMethods())
			{
				bool flag = false;
				object[] customAttributes2 = methodInfo.GetCustomAttributes(typeof(OperationContractAttribute), false);
				if (customAttributes2.Length == 1 && ((OperationContractAttribute)customAttributes2[0]).IsOneWay)
				{
					flag = true;
				}
				ServiceMethodInfo serviceMethodInfo = new ServiceMethodInfo(endpoint, methodInfo.Name, flag);
				endpoint.Methods.Add(serviceMethodInfo);
				foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
				{
					string name = parameterInfo.Name;
					TypeMemberInfo typeMemberInfo;
					if (parameterInfo.ParameterType.IsByRef)
					{
						typeMemberInfo = new TypeMemberInfo(name, DataContractAnalyzer.CreateServiceTypeInfo(parameterInfo.ParameterType.GetElementType()));
					}
					else
					{
						typeMemberInfo = new TypeMemberInfo(name, DataContractAnalyzer.CreateServiceTypeInfo(parameterInfo.ParameterType));
					}
					if (parameterInfo.IsIn || !parameterInfo.IsOut)
					{
						serviceMethodInfo.InputParameters.Add(typeMemberInfo);
					}
					else
					{
						serviceMethodInfo.OtherParameters.Add(typeMemberInfo);
					}
				}
				if (methodInfo.ReturnType != null && !methodInfo.ReturnType.Equals(typeof(void)))
				{
					TypeMemberInfo typeMemberInfo2 = new TypeMemberInfo("(return)", DataContractAnalyzer.CreateServiceTypeInfo(methodInfo.ReturnParameter.ParameterType));
					serviceMethodInfo.OtherParameters.Add(typeMemberInfo2);
				}
			}
			return endpoint;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000056D4 File Offset: 0x000038D4
		private static SerializableType CreateServiceTypeInfo(Type type)
		{
			string fullName = type.FullName;
			if (DataContractAnalyzer.serviceTypeInfoPool.ContainsKey(type.FullName))
			{
				return DataContractAnalyzer.serviceTypeInfoPool[fullName];
			}
			bool flag = false;
			SerializableType serializableType = new SerializableType(type);
			DataContractAnalyzer.serviceTypeInfoPool.Add(fullName, serializableType);
			if (type.IsArray)
			{
				SerializableType serializableType2 = DataContractAnalyzer.CreateServiceTypeInfo(type.GetElementType());
				flag = serializableType2.IsInvalid;
				serializableType.Members.Add(new TypeMemberInfo("[0]", serializableType2));
			}
			else if (DataContractAnalyzer.IsNullableType(type))
			{
				Type[] genericArguments = type.GetGenericArguments();
				SerializableType serializableType3 = DataContractAnalyzer.CreateServiceTypeInfo(genericArguments[0]);
				flag = serializableType3.IsInvalid;
				serializableType.Members.Add(new TypeMemberInfo("Value", serializableType3));
			}
			else if (DataContractAnalyzer.IsCollectionType(type))
			{
				Type baseType = type.BaseType;
				if (baseType.IsGenericType)
				{
					Type[] genericArguments2 = baseType.GetGenericArguments();
					SerializableType serializableType4 = DataContractAnalyzer.CreateServiceTypeInfo(genericArguments2[0]);
					flag = serializableType4.IsInvalid;
					serializableType.Members.Add(new TypeMemberInfo("[0]", serializableType4));
				}
			}
			else if (DataContractAnalyzer.IsDictionaryType(type))
			{
				Type[] genericArguments3 = type.GetGenericArguments();
				Type typeFromHandle = typeof(KeyValuePair<, >);
				Type type2 = typeFromHandle.MakeGenericType(new Type[]
				{
					genericArguments3[0],
					genericArguments3[1]
				});
				SerializableType serializableType5 = DataContractAnalyzer.CreateServiceTypeInfo(type2);
				flag = serializableType5.IsInvalid;
				serializableType.Members.Add(new TypeMemberInfo("[0]", serializableType5));
				if (!DataContractAnalyzer.TypesCache.ContainsKey(fullName))
				{
					DataContractAnalyzer.TypesCache.Add(fullName, type);
				}
			}
			else if (DataContractAnalyzer.IsKeyValuePairType(type))
			{
				Type[] genericArguments4 = type.GetGenericArguments();
				SerializableType serializableType6 = DataContractAnalyzer.CreateServiceTypeInfo(genericArguments4[0]);
				SerializableType serializableType7 = DataContractAnalyzer.CreateServiceTypeInfo(genericArguments4[1]);
				flag = serializableType6.IsInvalid || serializableType7.IsInvalid;
				serializableType.Members.Add(new TypeMemberInfo("Key", serializableType6));
				serializableType.Members.Add(new TypeMemberInfo("Value", serializableType7));
				if (!DataContractAnalyzer.TypesCache.ContainsKey(fullName))
				{
					DataContractAnalyzer.TypesCache.Add(fullName, type);
				}
			}
			else if (DataContractAnalyzer.IsSupportedType(type))
			{
				foreach (PropertyInfo propertyInfo in type.GetProperties())
				{
					if (DataContractAnalyzer.IsSupportedMember(propertyInfo) || type == typeof(DictionaryEntry))
					{
						SerializableType serializableType8 = DataContractAnalyzer.CreateServiceTypeInfo(propertyInfo.PropertyType);
						if (serializableType8.IsInvalid)
						{
							flag = true;
						}
						serializableType.Members.Add(new TypeMemberInfo(propertyInfo.Name, serializableType8));
					}
				}
				foreach (FieldInfo fieldInfo in type.GetFields())
				{
					if (DataContractAnalyzer.IsSupportedMember(fieldInfo))
					{
						SerializableType serializableType9 = DataContractAnalyzer.CreateServiceTypeInfo(fieldInfo.FieldType);
						if (serializableType9.IsInvalid)
						{
							flag = true;
						}
						serializableType.Members.Add(new TypeMemberInfo(fieldInfo.Name, serializableType9));
					}
				}
			}
			if (flag)
			{
				serializableType.MarkAsInvalid();
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(KnownTypeAttribute), false);
			foreach (object obj in customAttributes)
			{
				KnownTypeAttribute knownTypeAttribute = (KnownTypeAttribute)obj;
				serializableType.SubTypes.Add(DataContractAnalyzer.CreateServiceTypeInfo(knownTypeAttribute.Type));
			}
			object[] customAttributes2 = type.GetCustomAttributes(typeof(XmlIncludeAttribute), false);
			foreach (object obj2 in customAttributes2)
			{
				XmlIncludeAttribute xmlIncludeAttribute = (XmlIncludeAttribute)obj2;
				serializableType.SubTypes.Add(DataContractAnalyzer.CreateServiceTypeInfo(xmlIncludeAttribute.Type));
			}
			return serializableType;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005A78 File Offset: 0x00003C78
		private static string GetContractTypeName(Type contractType)
		{
			foreach (Type type in ClientSettings.ClientAssembly.GetTypes())
			{
				if (contractType.IsAssignableFrom(type) && !type.IsInterface)
				{
					return type.FullName;
				}
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002AC4 File Offset: 0x00000CC4
		private static bool HasAttribute(MemberInfo member, Type type)
		{
			return member.GetCustomAttributes(type, true).Length != 0;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005ABC File Offset: 0x00003CBC
		private static bool IsSupportedMember(MemberInfo member)
		{
			foreach (Type type in DataContractAnalyzer.memberAttributes)
			{
				if (DataContractAnalyzer.HasAttribute(member, type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000029 RID: 41
		internal static IDictionary<string, Type> TypesCache = new Dictionary<string, Type>();

		// Token: 0x0400002A RID: 42
		internal static IDictionary<object, Variable> ObjectsCache = new Dictionary<object, Variable>(new DataContractAnalyzer.EqualityComparer());

		// Token: 0x0400002B RID: 43
		private static Type[] memberAttributes = new Type[]
		{
			typeof(DataMemberAttribute),
			typeof(MessageBodyMemberAttribute),
			typeof(MessageHeaderAttribute),
			typeof(MessageHeaderArrayAttribute),
			typeof(XmlAttributeAttribute),
			typeof(XmlElementAttribute),
			typeof(XmlArrayAttribute),
			typeof(XmlTextAttribute)
		};

		// Token: 0x0400002C RID: 44
		private static IDictionary<string, SerializableType> serviceTypeInfoPool = new Dictionary<string, SerializableType>();

		// Token: 0x0400002D RID: 45
		private static Type[] typeAttributes = new Type[]
		{
			typeof(DataContractAttribute),
			typeof(XmlTypeAttribute),
			typeof(MessageContractAttribute)
		};

		// Token: 0x0200000B RID: 11
		internal class EqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x060000A1 RID: 161 RVA: 0x00002ADA File Offset: 0x00000CDA
			public bool Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x00002AE0 File Offset: 0x00000CE0
			public int GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
