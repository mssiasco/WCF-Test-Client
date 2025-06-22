using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Main.Tools.TestClient.Variables;

namespace Main.Tools.TestClient
{
	// Token: 0x02000020 RID: 32
	internal class ServiceExecutor : MarshalByRefObject
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00006C80 File Offset: 0x00004E80
		internal static ServiceInvocationOutputs ExecuteInClientDomain(ServiceInvocationInputs inputs)
		{
			ServiceExecutor serviceExecutor = (ServiceExecutor)inputs.Domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ServiceExecutor).FullName);
			return serviceExecutor.Execute(inputs);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006CC0 File Offset: 0x00004EC0
		internal static void ExtractExceptionInfos(Exception e, out string[] messages, out string[] stackTraces)
		{
			int num = 0;
			Exception ex;
			for (ex = e; ex != null; ex = ex.InnerException)
			{
				num++;
			}
			messages = new string[num];
			stackTraces = new string[num];
			int num2 = 0;
			ex = e;
			while (ex != null)
			{
				messages[num2] = ex.Message;
				stackTraces[num2] = ex.StackTrace;
				ex = ex.InnerException;
				num2++;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006D1C File Offset: 0x00004F1C
		internal static string TranslateToXmlInClientDomain(TestCase testCase, Variable[] inputs)
		{
			AppDomain clientDomain = testCase.Method.Endpoint.ServiceProject.ClientDomain;
			ServiceExecutor serviceExecutor = (ServiceExecutor)clientDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ServiceExecutor).FullName);
			return serviceExecutor.TranslateToXml(testCase, inputs);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006D6C File Offset: 0x00004F6C
		internal static IList<int> ValidateDictionary(DictionaryVariable variable, AppDomain domain)
		{
			ServiceExecutor serviceExecutor = (ServiceExecutor)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ServiceExecutor).FullName);
			return serviceExecutor.ValidateDictionary(variable);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006DA8 File Offset: 0x00004FA8
		internal ServiceInvocationOutputs ConstructClientToCache(string proxyIdentifier, string clientTypeName, string endpointConfigurationName)
		{
			object obj;
			ServiceInvocationOutputs serviceInvocationOutputs = ServiceExecutor.ConstructClient(clientTypeName, endpointConfigurationName, out obj);
			if (serviceInvocationOutputs != null)
			{
				return serviceInvocationOutputs;
			}
			if (ServiceExecutor.cachedProxies.ContainsKey(proxyIdentifier))
			{
				this.DeleteClient(proxyIdentifier);
			}
			ServiceExecutor.cachedProxies.Add(proxyIdentifier, obj);
			return null;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003032 File Offset: 0x00001232
		internal void DeleteClient(string proxyIdentifier)
		{
			if (ServiceExecutor.cachedProxies.ContainsKey(proxyIdentifier))
			{
				ServiceExecutor.CloseClient((ICommunicationObject)ServiceExecutor.cachedProxies[proxyIdentifier]);
				ServiceExecutor.cachedProxies.Remove(proxyIdentifier);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006DE8 File Offset: 0x00004FE8
		private static IDictionary<string, object> BuildParameters(Variable[] inputs)
		{
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (Variable variable in inputs)
			{
				object obj = variable.CreateObject();
				dictionary.Add(variable.Name, obj);
			}
			return dictionary;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006E28 File Offset: 0x00005028
		private static void CloseClientCallBack(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ICommunicationObject communicationObject = (ICommunicationObject)result.AsyncState;
				ServiceExecutor.ProcessClientClose(communicationObject, result);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006E50 File Offset: 0x00005050
		private static void ProcessClientClose(ICommunicationObject client, IAsyncResult result)
		{
			try
			{
				client.EndClose(result);
			}
			catch (TimeoutException)
			{
				client.Abort();
			}
			catch (CommunicationException)
			{
				client.Abort();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006E94 File Offset: 0x00005094
		private static void CloseClient(ICommunicationObject client)
		{
			try
			{
				IAsyncResult asyncResult = client.BeginClose(ServiceExecutor.closeClientCallBack, client);
				if (asyncResult.CompletedSynchronously)
				{
					ServiceExecutor.ProcessClientClose(client, asyncResult);
				}
			}
			catch (TimeoutException)
			{
				client.Abort();
			}
			catch (CommunicationException)
			{
				client.Abort();
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006EF0 File Offset: 0x000050F0
		private static ServiceInvocationOutputs ConstructClient(string clientTypeName, string endpointConfigurationName, out object client)
		{
			Type type = ClientSettings.ClientAssembly.GetType(clientTypeName);
			try
			{
				if (endpointConfigurationName == null)
				{
					client = type.GetConstructor(Type.EmptyTypes).Invoke(null);
				}
				else
				{
					client = type.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { endpointConfigurationName });
				}
			}
			catch (TargetInvocationException ex)
			{
				if (ExceptionUtility.IsFatal(ex))
				{
					throw;
				}
				client = null;
				return new ServiceInvocationOutputs(ExceptionType.ProxyConstructFail, new string[] { ex.InnerException.Message }, new string[] { ex.InnerException.StackTrace }, null);
			}
			return null;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006FA0 File Offset: 0x000051A0
		private static void PopulateInputParameters(string methodName, Variable[] inputs, Type contractType, out MethodInfo method, out ParameterInfo[] parameters, out object[] parameterArray)
		{
			method = contractType.GetMethod(methodName);
			parameters = method.GetParameters();
			parameterArray = new object[parameters.Length];
			IDictionary<string, object> dictionary = ServiceExecutor.BuildParameters(inputs);
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.IsIn || !parameterInfo.IsOut)
				{
					parameterArray[num] = dictionary[parameterInfo.Name];
				}
				num++;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007018 File Offset: 0x00005218
		private ServiceInvocationOutputs Execute(ServiceInvocationInputs inputValues)
		{
			object obj = ServiceExecutor.lockObject;
			ServiceInvocationOutputs serviceInvocationOutputs2;
			lock (obj)
			{
				string clientTypeName = inputValues.ClientTypeName;
				string contractTypeName = inputValues.ContractTypeName;
				string endpointConfigurationName = inputValues.EndpointConfigurationName;
				string methodName = inputValues.MethodName;
				Variable[] inputs = inputValues.GetInputs();
				Type type = ClientSettings.ClientAssembly.GetType(contractTypeName);
				MethodInfo methodInfo;
				ParameterInfo[] array;
				object[] array2;
				try
				{
					ServiceExecutor.PopulateInputParameters(methodName, inputs, type, out methodInfo, out array, out array2);
				}
				catch (TargetInvocationException ex)
				{
					return new ServiceInvocationOutputs(ExceptionType.InvalidInput, new string[] { ex.InnerException.Message }, null, null);
				}
				if (inputValues.StartNewClient || !ServiceExecutor.cachedProxies.ContainsKey(inputValues.ProxyIdentifier))
				{
					ServiceInvocationOutputs serviceInvocationOutputs = this.ConstructClientToCache(inputValues.ProxyIdentifier, clientTypeName, endpointConfigurationName);
					if (serviceInvocationOutputs != null)
					{
						return serviceInvocationOutputs;
					}
				}
				object obj2 = ServiceExecutor.cachedProxies[inputValues.ProxyIdentifier];
				Type baseType = obj2.GetType().BaseType;
				PropertyInfo property = baseType.GetProperty("Endpoint");
				ServiceEndpoint serviceEndpoint = (ServiceEndpoint)property.GetValue(obj2, null);
				ServiceExecutor.ResponseXmlInterceptingBehavior responseXmlInterceptingBehavior;
				if (!serviceEndpoint.Behaviors.Contains(typeof(ServiceExecutor.ResponseXmlInterceptingBehavior)))
				{
					responseXmlInterceptingBehavior = new ServiceExecutor.ResponseXmlInterceptingBehavior(this.extractingXML);
					serviceEndpoint.Behaviors.Add(responseXmlInterceptingBehavior);
				}
				else
				{
					responseXmlInterceptingBehavior = serviceEndpoint.Behaviors.Find<ServiceExecutor.ResponseXmlInterceptingBehavior>();
					responseXmlInterceptingBehavior.SetExtractingXML(this.extractingXML);
				}
				object obj3 = null;
				try
				{
					obj3 = methodInfo.Invoke(obj2, array2);
				}
				catch (TargetInvocationException ex2)
				{
					Exception innerException = ex2.InnerException;
					if (ExceptionUtility.IsFatal(innerException))
					{
						throw;
					}
					string[] array3;
					string[] array4;
					ServiceExecutor.ExtractExceptionInfos(innerException, out array3, out array4);
					return new ServiceInvocationOutputs(ExceptionType.InvokeFail, array3, array4, responseXmlInterceptingBehavior.InterceptedXml);
				}
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				int num = 0;
				foreach (ParameterInfo parameterInfo in array)
				{
					if (parameterInfo.ParameterType.IsByRef)
					{
						object obj4 = array2[num];
						if (obj4 == null)
						{
							obj4 = new NullObject();
						}
						dictionary.Add(parameterInfo.Name, obj4);
					}
					num++;
				}
				if (obj3 == null)
				{
					obj3 = new NullObject();
				}
				serviceInvocationOutputs2 = new ServiceInvocationOutputs(DataContractAnalyzer.BuildVariableInfos(obj3, dictionary), responseXmlInterceptingBehavior.InterceptedXml);
			}
			return serviceInvocationOutputs2;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007290 File Offset: 0x00005490
		private string TranslateToXml(TestCase testCase, Variable[] inputs)
		{
			this.extractingXML = true;
			ServiceInvocationOutputs serviceInvocationOutputs = this.Execute(new ServiceInvocationInputs(inputs, testCase, false));
			return serviceInvocationOutputs.ExceptionMessages[0];
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003062 File Offset: 0x00001262
		private IList<int> ValidateDictionary(DictionaryVariable variable)
		{
			return variable.ValidateDictionary();
		}

		// Token: 0x04000069 RID: 105
		private static AsyncCallback closeClientCallBack = new AsyncCallback(ServiceExecutor.CloseClientCallBack);

		// Token: 0x0400006A RID: 106
		private static IDictionary<string, object> cachedProxies = new Dictionary<string, object>();

		// Token: 0x0400006B RID: 107
		private static object lockObject = new object();

		// Token: 0x0400006C RID: 108
		private bool extractingXML;

		// Token: 0x02000021 RID: 33
		private class ResponseXmlInterceptingBehavior : IEndpointBehavior
		{
			// Token: 0x0600012B RID: 299 RVA: 0x00003091 File Offset: 0x00001291
			public ResponseXmlInterceptingBehavior(bool extractingXML)
			{
				this.responseXmlInterceptor = new ServiceExecutor.ResponseXmlInterceptingBehavior.ResponseXmlInterceptingInspector(extractingXML);
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600012C RID: 300 RVA: 0x000030A5 File Offset: 0x000012A5
			public string InterceptedXml
			{
				get
				{
					return this.responseXmlInterceptor.InterceptedXml;
				}
			}

			// Token: 0x0600012D RID: 301 RVA: 0x00002B6F File Offset: 0x00000D6F
			public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
			{
			}

			// Token: 0x0600012E RID: 302 RVA: 0x000030B2 File Offset: 0x000012B2
			public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
			{
				clientRuntime.MessageInspectors.Add(this.responseXmlInterceptor);
			}

			// Token: 0x0600012F RID: 303 RVA: 0x00002B6F File Offset: 0x00000D6F
			public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
			{
			}

			// Token: 0x06000130 RID: 304 RVA: 0x000030C5 File Offset: 0x000012C5
			public void SetExtractingXML(bool value)
			{
				this.responseXmlInterceptor.SetExtractingXML(value);
			}

			// Token: 0x06000131 RID: 305 RVA: 0x00002B6F File Offset: 0x00000D6F
			public void Validate(ServiceEndpoint endpoint)
			{
			}

			// Token: 0x0400006D RID: 109
			private ServiceExecutor.ResponseXmlInterceptingBehavior.ResponseXmlInterceptingInspector responseXmlInterceptor;

			// Token: 0x02000022 RID: 34
			private class ResponseXmlInterceptingInspector : IClientMessageInspector
			{
				// Token: 0x06000132 RID: 306 RVA: 0x000030D3 File Offset: 0x000012D3
				public ResponseXmlInterceptingInspector(bool extractingXML)
				{
					this.extractingXML = extractingXML;
				}

				// Token: 0x1700007F RID: 127
				// (get) Token: 0x06000133 RID: 307 RVA: 0x000030E2 File Offset: 0x000012E2
				public string InterceptedXml
				{
					get
					{
						return this.interceptedXml;
					}
				}

				// Token: 0x06000134 RID: 308 RVA: 0x000030EA File Offset: 0x000012EA
				public void AfterReceiveReply(ref Message reply, object correlationState)
				{
					if (reply != null)
					{
						this.interceptedXml = reply.ToString();
					}
				}

				// Token: 0x06000135 RID: 309 RVA: 0x000030FD File Offset: 0x000012FD
				public object BeforeSendRequest(ref Message request, IClientChannel channel)
				{
					if (this.extractingXML)
					{
						throw new ExceptionUtility().ThrowHelperError(new ServiceExecutor.StopInvocationException(request.ToString()));
					}
					return null;
				}

				// Token: 0x06000136 RID: 310 RVA: 0x0000311F File Offset: 0x0000131F
				public void SetExtractingXML(bool value)
				{
					this.extractingXML = value;
				}

				// Token: 0x0400006E RID: 110
				private bool extractingXML;

				// Token: 0x0400006F RID: 111
				private string interceptedXml;
			}
		}

		// Token: 0x02000023 RID: 35
		[Serializable]
		private class StopInvocationException : Exception
		{
			// Token: 0x06000137 RID: 311 RVA: 0x00003128 File Offset: 0x00001328
			internal StopInvocationException(string message)
				: base(message)
			{
			}

			// Token: 0x06000138 RID: 312 RVA: 0x00003131 File Offset: 0x00001331
			protected StopInvocationException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}
	}
}
