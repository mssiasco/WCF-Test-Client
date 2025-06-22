using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	internal class Variable
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00008F1C File Offset: 0x0000711C
		internal Variable(TypeMemberInfo declaredMember)
		{
			this.currentMember = declaredMember;
			this.declaredMember = declaredMember;
			this.value = this.currentMember.GetDefaultValue();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000384E File Offset: 0x00001A4E
		internal Variable(TypeMemberInfo declaredMember, object obj)
			: this(declaredMember)
		{
			this.value = declaredMember.GetStringRepresentation(obj);
			this.modifiable = false;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000386B File Offset: 0x00001A6B
		internal static IList<Variable> VariablesPool
		{
			get
			{
				return Variable.variablesPool;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008F58 File Offset: 0x00007158
		internal EditorType EditorType
		{
			get
			{
				if (this.declaredMember.EditorType == EditorType.EditableDropDownBox)
				{
					string[] selectionList = this.GetSelectionList();
					if (selectionList == null || selectionList.Length < 1)
					{
						return EditorType.TextBox;
					}
				}
				return this.declaredMember.EditorType;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00003872 File Offset: 0x00001A72
		internal string FriendlyTypeName
		{
			get
			{
				return this.declaredMember.FriendlyTypeName;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00008F90 File Offset: 0x00007190
		internal bool IsKey
		{
			set
			{
				this.isKey = value;
				if (this.isKey && this.value.Equals("(null)", StringComparison.Ordinal))
				{
					if (this.declaredMember.HasMembers())
					{
						this.value = this.TypeName;
					}
					if (this.TypeName.Equals("System.String", StringComparison.Ordinal))
					{
						this.value = string.Empty;
					}
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000387F File Offset: 0x00001A7F
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000389B File Offset: 0x00001A9B
		internal virtual string Name
		{
			get
			{
				if (this.name == null)
				{
					return this.declaredMember.VariableName;
				}
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000038A4 File Offset: 0x00001AA4
		internal string TypeName
		{
			get
			{
				return this.declaredMember.TypeName;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008FF8 File Offset: 0x000071F8
		internal static void SaveToPool(Variable variable)
		{
			if (Variable.variablesPool.Count == Variable.poolSize)
			{
				Variable.variablesPool.RemoveAt(0);
			}
			Variable variable2 = variable.Clone();
			Variable.variablesPool.Add(variable2);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00002D82 File Offset: 0x00000F82
		internal virtual Variable Clone()
		{
			return null;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000038B1 File Offset: 0x00001AB1
		internal virtual bool CopyFrom(Variable variable)
		{
			if (variable == null || this == variable)
			{
				return false;
			}
			this.value = variable.value;
			return true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00002D82 File Offset: 0x00000F82
		internal virtual object CreateObject()
		{
			return null;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009034 File Offset: 0x00007234
		internal Variable[] GetChildVariables()
		{
			if (string.Equals(this.value, "(null)", StringComparison.Ordinal))
			{
				return Variable.empty;
			}
			if (this.modifiable)
			{
				if (this.declaredMember.HasMembers() && (this.childVariables == null || this.value != this.currentMember.TypeName))
				{
					this.currentMember = this.declaredMember;
					string variableName = this.declaredMember.VariableName;
					foreach (SerializableType serializableType in this.declaredMember.SubTypes)
					{
						if (serializableType.TypeName.Equals(this.value))
						{
							this.currentMember = new TypeMemberInfo(variableName, serializableType);
							break;
						}
					}
					this.childVariables = new Variable[this.currentMember.Members.Count];
					int num = 0;
					foreach (TypeMemberInfo typeMemberInfo in this.currentMember.Members)
					{
						this.childVariables[num] = VariableFactory.CreateAssociateVariable(typeMemberInfo);
						if (this.currentMember.TypeProperty.IsKeyValuePair && string.Equals(typeMemberInfo.VariableName, "Key", StringComparison.Ordinal))
						{
							this.childVariables[num].IsKey = true;
						}
						this.childVariables[num].SetServiceMethodInfo(this.serviceMethodInfo);
						if (this.parent != null)
						{
							this.childVariables[num].SetParent(this);
						}
						num++;
					}
				}
				if (this.declaredMember.IsContainer())
				{
					int arrayLength = Variable.GetArrayLength(this.value);
					Variable[] array = this.childVariables;
					this.childVariables = new Variable[arrayLength];
					TypeMemberInfo typeMemberInfo2 = null;
					using (IEnumerator<TypeMemberInfo> enumerator3 = this.declaredMember.Members.GetEnumerator())
					{
						if (enumerator3.MoveNext())
						{
							TypeMemberInfo typeMemberInfo3 = enumerator3.Current;
							typeMemberInfo2 = typeMemberInfo3;
						}
					}
					for (int i = 0; i < arrayLength; i++)
					{
						if (array != null && i < array.Length)
						{
							this.childVariables[i] = array[i];
						}
						else
						{
							this.childVariables[i] = VariableFactory.CreateAssociateVariable("[" + i.ToString() + "]", typeMemberInfo2);
							this.childVariables[i].SetServiceMethodInfo(this.serviceMethodInfo);
							if (this.declaredMember.TypeProperty.IsDictionary || this.parent != null)
							{
								this.childVariables[i].SetParent(this);
								if (this.declaredMember.TypeProperty.IsDictionary)
								{
									this.childVariables[i].GetChildVariables();
								}
							}
						}
					}
				}
			}
			return this.childVariables;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000931C File Offset: 0x0000751C
		internal string[] GetSelectionList()
		{
			string[] selectionList = this.declaredMember.GetSelectionList();
			if (this.isKey && selectionList != null)
			{
				int num = Array.FindIndex<string>(selectionList, new Predicate<string>(Variable.IsNullRepresentation));
				if (num >= 0)
				{
					string[] array = new string[selectionList.Length - 1];
					int num2 = 0;
					for (int i = 0; i < array.Length; i++)
					{
						if (num2 == num)
						{
							num2++;
						}
						array[i] = selectionList[num2];
						num2++;
					}
					return array;
				}
			}
			return selectionList;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000038C9 File Offset: 0x00001AC9
		internal string GetValue()
		{
			if (string.Equals(this.value, this.TypeName, StringComparison.Ordinal) && this.currentMember.HasMembers())
			{
				return this.FriendlyTypeName;
			}
			return this.value;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000938C File Offset: 0x0000758C
		internal bool IsExpandable()
		{
			if (this.childVariables != null && this.childVariables.Length != 0)
			{
				return true;
			}
			if (this.EditorType == EditorType.DropDownBox)
			{
				return !this.declaredMember.TypeName.Equals("System.Boolean") && !this.declaredMember.TypeProperty.IsEnum && !this.declaredMember.TypeProperty.IsDataSet && !string.Equals(this.value, "(null)", StringComparison.Ordinal);
			}
			return this.declaredMember.IsContainer() && !this.value.Equals("(null)") && Variable.GetArrayLength(this.value) > 0;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000038F9 File Offset: 0x00001AF9
		internal void SetChildVariables(Variable[] value)
		{
			this.childVariables = value;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00003902 File Offset: 0x00001B02
		internal void SetServiceMethodInfo(ServiceMethodInfo serviceMethodInfo)
		{
			this.serviceMethodInfo = serviceMethodInfo;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009438 File Offset: 0x00007638
		internal ValidationResult SetValue(string userValue)
		{
			string text = this.value;
			this.ValidateAndCanonicalize(userValue);
			if (this.value == null)
			{
				this.value = text;
				string text2 = string.Format(CultureInfo.CurrentUICulture, StringResources.TypeError, userValue);
				RtlAwareMessageBox.Show(text2, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
				return new ValidationResult(false, false);
			}
			bool flag = false;
			if (this.parent != null)
			{
				Variable variable = this;
				while (variable != null && !variable.isKey)
				{
					variable = variable.parent;
				}
				if (variable != null)
				{
					flag = true;
					variable = variable.parent.parent;
					variable.ValidateAndCanonicalize(variable.value);
				}
			}
			if (this.EditorType == EditorType.EditableDropDownBox && this.declaredMember.IsContainer())
			{
				if (string.Equals(this.value, "(null)", StringComparison.Ordinal))
				{
					if (this.declaredMember.Members.Count > 0)
					{
						return new ValidationResult(true, true);
					}
				}
				else
				{
					if (string.Equals(text, "(null)", StringComparison.Ordinal))
					{
						return new ValidationResult(true, true);
					}
					int arrayLength = Variable.GetArrayLength(text);
					int arrayLength2 = Variable.GetArrayLength(this.value);
					return new ValidationResult(true, arrayLength != arrayLength2);
				}
			}
			if (this.EditorType == EditorType.DropDownBox)
			{
				if (!string.Equals(this.value, "(null)", StringComparison.Ordinal))
				{
					return new ValidationResult(true, true);
				}
				if (this.currentMember.Members.Count > 0)
				{
					return new ValidationResult(true, true);
				}
			}
			return new ValidationResult(true, flag);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000390B File Offset: 0x00001B0B
		internal virtual void ValidateAndCanonicalize(string input)
		{
			if (input == null)
			{
				this.value = null;
				return;
			}
			if (this.isKey && string.Equals(input, "(null)", StringComparison.Ordinal))
			{
				this.value = null;
				return;
			}
			this.value = input;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000393D File Offset: 0x00001B3D
		private static int GetArrayLength(string canonicalizedValue)
		{
			return int.Parse(canonicalizedValue.Substring("length=".Length), CultureInfo.CurrentCulture);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00003959 File Offset: 0x00001B59
		private static bool IsNullRepresentation(string str)
		{
			return string.CompareOrdinal(str, "(null)") == 0;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00003969 File Offset: 0x00001B69
		private void SetParent(Variable parent)
		{
			this.parent = parent;
		}

		// Token: 0x040000BC RID: 188
		protected Variable[] childVariables;

		// Token: 0x040000BD RID: 189
		protected TypeMemberInfo currentMember;

		// Token: 0x040000BE RID: 190
		protected TypeMemberInfo declaredMember;

		// Token: 0x040000BF RID: 191
		protected string name;

		// Token: 0x040000C0 RID: 192
		[NonSerialized]
		protected ServiceMethodInfo serviceMethodInfo;

		// Token: 0x040000C1 RID: 193
		protected string value;

		// Token: 0x040000C2 RID: 194
		private static readonly Variable[] empty = new Variable[0];

		// Token: 0x040000C3 RID: 195
		private static int poolSize = 1;

		// Token: 0x040000C4 RID: 196
		private static IList<Variable> variablesPool = new List<Variable>();

		// Token: 0x040000C5 RID: 197
		[NonSerialized]
		private bool isKey;

		// Token: 0x040000C6 RID: 198
		private bool modifiable = true;

		// Token: 0x040000C7 RID: 199
		[NonSerialized]
		private Variable parent;
	}
}
