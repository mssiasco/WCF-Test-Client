using System;
using System.Collections.Generic;
using Main.Tools.TestClient.Variables;

namespace Main.Tools.TestClient
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	internal class ServiceMethodInfo
	{
		// Token: 0x0600015E RID: 350 RVA: 0x000032DF File Offset: 0x000014DF
		internal ServiceMethodInfo(ClientEndpointInfo endpoint, string methodName, bool isOneWay)
		{
			this.endpoint = endpoint;
			this.methodName = methodName;
			this.isOneWay = isOneWay;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000331D File Offset: 0x0000151D
		internal ClientEndpointInfo Endpoint
		{
			get
			{
				return this.endpoint;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003325 File Offset: 0x00001525
		internal IList<TypeMemberInfo> InputParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00007368 File Offset: 0x00005568
		internal List<TypeMemberInfo> InvalidMembers
		{
			get
			{
				if (this.invalidParameters == null)
				{
					this.invalidParameters = new List<TypeMemberInfo>();
					this.parameters.Find(new Predicate<TypeMemberInfo>(this.CheckAndSaveInvalidMembers));
					this.otherParameters.Find(new Predicate<TypeMemberInfo>(this.CheckAndSaveInvalidMembers));
				}
				return this.invalidParameters;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000332D File Offset: 0x0000152D
		internal bool IsOneWay
		{
			get
			{
				return this.isOneWay;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003335 File Offset: 0x00001535
		internal string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000333D File Offset: 0x0000153D
		internal IList<TypeMemberInfo> OtherParameters
		{
			get
			{
				return this.otherParameters;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003345 File Offset: 0x00001545
		internal IList<TestCase> TestCases
		{
			get
			{
				return this.testCases;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000334D File Offset: 0x0000154D
		internal bool Valid
		{
			get
			{
				return this.InvalidMembers.Count == 0;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000073C0 File Offset: 0x000055C0
		internal TestCase CreateTestCase()
		{
			TestCase testCase = new TestCase(this);
			this.testCases.Add(testCase);
			return testCase;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000073E4 File Offset: 0x000055E4
		internal Variable[] GetVariables()
		{
			Variable[] array = new Variable[this.parameters.Count];
			int num = 0;
			foreach (TypeMemberInfo typeMemberInfo in this.parameters)
			{
				array[num] = VariableFactory.CreateAssociateVariable(typeMemberInfo);
				array[num].SetServiceMethodInfo(this);
				string[] selectionList = array[num].GetSelectionList();
				if (selectionList != null && selectionList.Length == 2 && selectionList[0] == "(null)")
				{
					array[num].SetValue(selectionList[1]);
				}
				num++;
			}
			return array;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000335D File Offset: 0x0000155D
		private bool CheckAndSaveInvalidMembers(TypeMemberInfo member)
		{
			if (member.IsInvalid)
			{
				this.invalidParameters.Add(member);
			}
			return false;
		}

		// Token: 0x04000081 RID: 129
		private ClientEndpointInfo endpoint;

		// Token: 0x04000082 RID: 130
		private List<TypeMemberInfo> invalidParameters;

		// Token: 0x04000083 RID: 131
		private bool isOneWay;

		// Token: 0x04000084 RID: 132
		private string methodName;

		// Token: 0x04000085 RID: 133
		private List<TypeMemberInfo> otherParameters = new List<TypeMemberInfo>();

		// Token: 0x04000086 RID: 134
		private List<TypeMemberInfo> parameters = new List<TypeMemberInfo>();

		// Token: 0x04000087 RID: 135
		private List<TestCase> testCases = new List<TestCase>();
	}
}
