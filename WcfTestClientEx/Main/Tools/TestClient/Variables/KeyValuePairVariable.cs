using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	internal class KeyValuePairVariable : Variable
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00003762 File Offset: 0x00001962
		internal KeyValuePairVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00003772 File Offset: 0x00001972
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000377A File Offset: 0x0000197A
		internal bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00003783 File Offset: 0x00001983
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00003799 File Offset: 0x00001999
		internal override string Name
		{
			get
			{
				if (this.isValid)
				{
					return base.Name;
				}
				return "[ # ]";
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008D50 File Offset: 0x00006F50
		internal override object CreateObject()
		{
			base.GetChildVariables();
			Type type = DataContractAnalyzer.TypesCache[this.currentMember.TypeName];
			return Activator.CreateInstance(type, new object[]
			{
				this.childVariables[0].CreateObject(),
				this.childVariables[1].CreateObject()
			});
		}

		// Token: 0x040000B9 RID: 185
		private const string duplicateKeyMark = "[ # ]";

		// Token: 0x040000BA RID: 186
		private bool isValid = true;
	}
}
