using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Main.Tools.TestClient.Variables;
using Microsoft.VisualStudio.VirtualTreeGrid;

namespace Main.Tools.TestClient
{
	// Token: 0x02000015 RID: 21
	internal class ParameterTreeAdapter : IMultiColumnBranch, IBranch
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005C84 File Offset: 0x00003E84
		internal ParameterTreeAdapter(ITree virtualTree, VirtualTreeControl virtualTreeControl, Variable[] variables, bool readOnly, ParameterTreeAdapter parent)
		{
			this.virtualTree = virtualTree;
			this.virtualTreeControl = virtualTreeControl;
			this.variables = variables;
			this.readOnly = readOnly;
			this.parent = parent;
			this.children = new ParameterTreeAdapter[variables.Length];
			this.OnBranchModification += this.ParameterTreeAdapter_OnBranchModification;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000C9 RID: 201 RVA: 0x00005CDC File Offset: 0x00003EDC
		// (remove) Token: 0x060000CA RID: 202 RVA: 0x00005D14 File Offset: 0x00003F14
		public event BranchModificationEventHandler OnBranchModification;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000CB RID: 203 RVA: 0x00005D4C File Offset: 0x00003F4C
		// (remove) Token: 0x060000CC RID: 204 RVA: 0x00005D84 File Offset: 0x00003F84
		internal event ParameterTreeAdapter.ValueUpdatedEventHandler OnValueUpdated;

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002C9B File Offset: 0x00000E9B
		public int ColumnCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005DBC File Offset: 0x00003FBC
		public BranchFeatures Features
		{
			get
			{
				BranchFeatures branchFeatures = BranchFeatures.Expansions | BranchFeatures.BranchRelocation | BranchFeatures.Realigns | BranchFeatures.PositionTracking;
				return branchFeatures | BranchFeatures.ImmediateSelectionLabelEdits;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002B45 File Offset: 0x00000D45
		public int UpdateCounter
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002C9E File Offset: 0x00000E9E
		public int VisibleItemCount
		{
			get
			{
				return this.variables.Length;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public void Close()
		{
			if (this.dataSetPropertyDescriptor != null)
			{
				this.dataSetPropertyDescriptor.Close();
			}
			if (this.children != null)
			{
				foreach (ParameterTreeAdapter parameterTreeAdapter in this.children)
				{
					if (parameterTreeAdapter != null)
					{
						parameterTreeAdapter.Close();
					}
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005E24 File Offset: 0x00004024
		public VirtualTreeLabelEditData BeginLabelEdit(int row, int column, VirtualTreeLabelEditActivationStyles activationStyle)
		{
			if (column != 1)
			{
				return VirtualTreeLabelEditData.Invalid;
			}
			if (this.readOnly && !(this.variables[row] is DataSetVariable))
			{
				return VirtualTreeLabelEditData.Invalid;
			}
			VirtualTreeLabelEditData @default = VirtualTreeLabelEditData.Default;
			if (this.variables[row].EditorType == EditorType.TextBox)
			{
				ParameterTreeAdapter.DummyContainer dummyContainer = new ParameterTreeAdapter.DummyContainer();
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dummyContainer)["DummyProperty"];
				@default.CustomInPlaceEdit = TypeEditorHost.Create(propertyDescriptor, dummyContainer);
			}
			else
			{
				ParameterTreeAdapter.ChoiceContainer choiceContainer = new ParameterTreeAdapter.ChoiceContainer(this, row, column);
				if (this.variables[row].EditorType == EditorType.EditableDropDownBox)
				{
					ParameterTreeAdapter.ChoiceConverter.StaticExclusive = false;
				}
				else
				{
					if (this.variables[row] is DataSetVariable && this.ShouldPopupDataSetEditor(row))
					{
						this.dataSetPropertyDescriptor = new DataSetPropertyDescriptor(this.variables[row], this.readOnly, this);
						TypeEditorHost typeEditorHost = TypeEditorHost.Create(this.dataSetPropertyDescriptor, null);
						typeEditorHost.KeyDown += this.dataSetHost_KeyDown;
						return new VirtualTreeLabelEditData(typeEditorHost);
					}
					if (this.variables[row].EditorType == EditorType.DropDownBox)
					{
						ParameterTreeAdapter.ChoiceConverter.StaticExclusive = true;
					}
				}
				ParameterTreeAdapter.ChoiceConverter.StaticChoices = this.variables[row].GetSelectionList();
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(choiceContainer)["Choice"];
				@default.CustomInPlaceEdit = TypeEditorHost.Create(propertyDescriptor2, choiceContainer);
			}
			return @default;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public SubItemCellStyles ColumnStyles(int column)
		{
			if (column != 0)
			{
				return SubItemCellStyles.Simple;
			}
			return SubItemCellStyles.Expandable;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005F60 File Offset: 0x00004160
		public LabelEditResult CommitLabelEdit(int row, int column, string newText)
		{
			if (newText == null || this.readOnly || (this.ShouldPopupDataSetEditor(row) && !SerializableType.IsNullRepresentation(newText)))
			{
				this.virtualTreeControl.EndLabelEdit(true);
				return LabelEditResult.CancelEdit;
			}
			ValidationResult validationResult = this.variables[row].SetValue(newText);
			if (!validationResult.Valid)
			{
				this.virtualTreeControl.EndLabelEdit(true);
				return LabelEditResult.CancelEdit;
			}
			if (validationResult.RefreshRequired)
			{
				this.virtualTreeControl.BeginUpdate();
				this.relativeRow = row;
				this.virtualTree.ListShuffle = true;
				this.virtualTree.Realign(this);
				this.virtualTreeControl.EndUpdate();
				this.virtualTree.ListShuffle = false;
			}
			this.PropagateValueUpdateEvent();
			return LabelEditResult.AcceptEdit;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006010 File Offset: 0x00004210
		public VirtualTreeAccessibilityData GetAccessibilityData(int row, int column)
		{
			string text = "{0} {1} {2}";
			if (column == 0 && this.IsExpandable(row, column))
			{
				text += " {3}";
			}
			return new VirtualTreeAccessibilityData(text, ParameterTreeAdapter.AccessibilityReplacementFields);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public VirtualTreeDisplayData GetDisplayData(int row, int column, VirtualTreeDisplayDataMasks requiredData)
		{
			return VirtualTreeDisplayData.Empty;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002B45 File Offset: 0x00000D45
		public int GetJaggedColumnCount(int row)
		{
			return 0;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006048 File Offset: 0x00004248
		public object GetObject(int row, int column, ObjectStyle style, ref int options)
		{
			if (style == ObjectStyle.TrackingObject)
			{
				return new RowCol(row, column);
			}
			if (this.IsExpandable(row, column))
			{
				return this.children[row] = new ParameterTreeAdapter(this.virtualTree, this.virtualTreeControl, this.variables[row].GetChildVariables(), this.readOnly, this);
			}
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000060A0 File Offset: 0x000042A0
		public string GetText(int row, int column)
		{
			if (column == 0)
			{
				return this.variables[row].Name;
			}
			if (column == 2)
			{
				if (this.variables[row].TypeName.Equals(typeof(NullObject).FullName))
				{
					return "NullObject";
				}
				return this.variables[row].FriendlyTypeName;
			}
			else
			{
				if (column == 1)
				{
					return this.variables[row].GetValue();
				}
				return "";
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public string GetTipText(int row, int column, ToolTipType tipType)
		{
			return this.GetText(row, column);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002CC1 File Offset: 0x00000EC1
		public bool IsExpandable(int row, int column)
		{
			return column == 0 && this.variables[row].IsExpandable();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006110 File Offset: 0x00004310
		public LocateObjectData LocateObject(object obj, ObjectStyle style, int locateOptions)
		{
			LocateObjectData locateObjectData = default(LocateObjectData);
			if (style == ObjectStyle.TrackingObject)
			{
				RowCol rowCol = (RowCol)obj;
				locateObjectData.Row = rowCol.Row;
				locateObjectData.Column = rowCol.Col;
				locateObjectData.Options = 1;
			}
			else if (style == ObjectStyle.ExpandedBranch)
			{
				ParameterTreeAdapter parameterTreeAdapter = (ParameterTreeAdapter)obj;
				ParameterTreeAdapter parameterTreeAdapter2 = parameterTreeAdapter.parent;
				locateObjectData.Row = -1;
				for (int i = 0; i < parameterTreeAdapter2.children.Length; i++)
				{
					if (parameterTreeAdapter2.children[i] == parameterTreeAdapter)
					{
						locateObjectData.Row = i;
					}
				}
				locateObjectData.Column = 0;
				if (locateObjectData.Row == this.relativeRow)
				{
					locateObjectData.Options = 0;
				}
				else
				{
					locateObjectData.Options = 1;
				}
			}
			return locateObjectData;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002B6F File Offset: 0x00000D6F
		public void OnDragEvent(object sender, int row, int column, DragEventType eventType, DragEventArgs args)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002B6F File Offset: 0x00000D6F
		public void OnGiveFeedback(GiveFeedbackEventArgs args, int row, int column)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002B6F File Offset: 0x00000D6F
		public void OnQueryContinueDrag(QueryContinueDragEventArgs args, int row, int column)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public VirtualTreeStartDragData OnStartDrag(object sender, int row, int column, DragReason reason)
		{
			return VirtualTreeStartDragData.Empty;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002B6F File Offset: 0x00000D6F
		public void ParameterTreeAdapter_OnBranchModification(object sender, BranchModificationEventArgs e)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000061C4 File Offset: 0x000043C4
		public void PropagateValueUpdateEvent()
		{
			ParameterTreeAdapter parameterTreeAdapter = this;
			while (parameterTreeAdapter.parent != null)
			{
				parameterTreeAdapter = parameterTreeAdapter.parent;
			}
			if (parameterTreeAdapter.OnValueUpdated != null)
			{
				parameterTreeAdapter.OnValueUpdated();
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002B45 File Offset: 0x00000D45
		public StateRefreshChanges SynchronizeState(int row, int column, IBranch matchBranch, int matchRow, int matchColumn)
		{
			return StateRefreshChanges.None;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002B45 File Offset: 0x00000D45
		public StateRefreshChanges ToggleState(int row, int column)
		{
			return StateRefreshChanges.None;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002CDC File Offset: 0x00000EDC
		internal Variable[] GetVariables()
		{
			return this.variables;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000061F8 File Offset: 0x000043F8
		private void dataSetHost_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Alt && !e.Control && !e.Shift && e.KeyData == Keys.Space)
			{
				this.dataSetPropertyDescriptor = (sender as TypeEditorHost).CurrentPropertyDescriptor as DataSetPropertyDescriptor;
				((DataSetUITypeEditor)this.dataSetPropertyDescriptor.GetEditor(null)).EditValue(null, this.dataSetPropertyDescriptor.GetValue(this));
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private bool ShouldPopupDataSetEditor(int row)
		{
			return this.variables[row] is DataSetVariable && (this.readOnly || !SerializableType.IsNullRepresentation(this.variables[row].GetValue()));
		}

		// Token: 0x04000043 RID: 67
		internal const int NumColumns = 3;

		// Token: 0x04000044 RID: 68
		private ParameterTreeAdapter[] children;

		// Token: 0x04000045 RID: 69
		private ParameterTreeAdapter parent;

		// Token: 0x04000046 RID: 70
		private bool readOnly;

		// Token: 0x04000047 RID: 71
		private int relativeRow;

		// Token: 0x04000048 RID: 72
		private Variable[] variables;

		// Token: 0x04000049 RID: 73
		private ITree virtualTree;

		// Token: 0x0400004A RID: 74
		private VirtualTreeControl virtualTreeControl;

		// Token: 0x0400004B RID: 75
		private DataSetPropertyDescriptor dataSetPropertyDescriptor;

		// Token: 0x0400004C RID: 76
		internal static AccessibilityReplacementField[] AccessibilityReplacementFields = new AccessibilityReplacementField[]
		{
			AccessibilityReplacementField.DisplayText,
			AccessibilityReplacementField.GlobalRowText1,
			AccessibilityReplacementField.ColumnHeader,
			AccessibilityReplacementField.ChildRowCountText
		};

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x060000EA RID: 234
		internal delegate void ValueUpdatedEventHandler();

		// Token: 0x02000017 RID: 23
		private enum ColumnIndex
		{
			// Token: 0x04000050 RID: 80
			VariableName,
			// Token: 0x04000051 RID: 81
			VariableValue,
			// Token: 0x04000052 RID: 82
			VariableType
		}

		// Token: 0x02000018 RID: 24
		private sealed class ChoiceContainer
		{
			// Token: 0x060000ED RID: 237 RVA: 0x00002D2E File Offset: 0x00000F2E
			internal ChoiceContainer(ParameterTreeAdapter branch, int row, int column)
			{
				this.branch = branch;
				this.row = row;
				this.column = column;
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060000EE RID: 238 RVA: 0x00002D4B File Offset: 0x00000F4B
			// (set) Token: 0x060000EF RID: 239 RVA: 0x00006264 File Offset: 0x00004464
			[TypeConverter(typeof(ParameterTreeAdapter.ChoiceConverter))]
			public string Choice
			{
				get
				{
					return this.branch.GetText(this.row, this.column);
				}
				set
				{
					if (new List<string>(ParameterTreeAdapter.ChoiceConverter.StaticChoices).Contains(value))
					{
						this.branch.CommitLabelEdit(this.row, this.column, value);
						return;
					}
					this.branch.CommitLabelEdit(this.row, this.column, null);
				}
			}

			// Token: 0x04000053 RID: 83
			private ParameterTreeAdapter branch;

			// Token: 0x04000054 RID: 84
			private int column;

			// Token: 0x04000055 RID: 85
			private int row;
		}

		// Token: 0x02000019 RID: 25
		private sealed class ChoiceConverter : StringConverter
		{
			// Token: 0x060000F0 RID: 240 RVA: 0x00002D64 File Offset: 0x00000F64
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return new TypeConverter.StandardValuesCollection(ParameterTreeAdapter.ChoiceConverter.StaticChoices);
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x00002D70 File Offset: 0x00000F70
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return ParameterTreeAdapter.ChoiceConverter.StaticExclusive;
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x00002D77 File Offset: 0x00000F77
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x04000056 RID: 86
			internal static string[] StaticChoices;

			// Token: 0x04000057 RID: 87
			internal static bool StaticExclusive;
		}

		// Token: 0x0200001A RID: 26
		private sealed class DummyContainer
		{
			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002D82 File Offset: 0x00000F82
			[Editor(typeof(UITypeEditor), typeof(UITypeEditor))]
			public string DummyProperty
			{
				get
				{
					return null;
				}
			}
		}
	}
}
