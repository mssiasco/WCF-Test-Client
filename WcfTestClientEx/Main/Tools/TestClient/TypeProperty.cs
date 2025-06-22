using System;

namespace Main.Tools.TestClient
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	internal class TypeProperty
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00003559 File Offset: 0x00001759
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00003561 File Offset: 0x00001761
		public bool IsDataSet
		{
			get
			{
				return this.isDataSet;
			}
			set
			{
				this.isDataSet = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000356A File Offset: 0x0000176A
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00003572 File Offset: 0x00001772
		public bool IsNumeric
		{
			get
			{
				return this.isNumeric;
			}
			set
			{
				this.isNumeric = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000357B File Offset: 0x0000177B
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00003583 File Offset: 0x00001783
		internal bool IsArray
		{
			get
			{
				return this.isArray;
			}
			set
			{
				this.isArray = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000358C File Offset: 0x0000178C
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00003594 File Offset: 0x00001794
		internal bool IsCollection
		{
			get
			{
				return this.isCollection;
			}
			set
			{
				this.isCollection = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000359D File Offset: 0x0000179D
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000035A5 File Offset: 0x000017A5
		internal bool IsComposite
		{
			get
			{
				return this.isComposite;
			}
			set
			{
				this.isComposite = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000035AE File Offset: 0x000017AE
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000035B6 File Offset: 0x000017B6
		internal bool IsDictionary
		{
			get
			{
				return this.isDictionary;
			}
			set
			{
				this.isDictionary = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000035BF File Offset: 0x000017BF
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000035C7 File Offset: 0x000017C7
		internal bool IsEnum
		{
			get
			{
				return this.isEnum;
			}
			set
			{
				this.isEnum = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000035D0 File Offset: 0x000017D0
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000035D8 File Offset: 0x000017D8
		internal bool IsKeyValuePair
		{
			get
			{
				return this.isKeyValuePair;
			}
			set
			{
				this.isKeyValuePair = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000035E1 File Offset: 0x000017E1
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000035E9 File Offset: 0x000017E9
		internal bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
			set
			{
				this.isNullable = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000035F2 File Offset: 0x000017F2
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000035FA File Offset: 0x000017FA
		internal bool IsStruct
		{
			get
			{
				return this.isStruct;
			}
			set
			{
				this.isStruct = value;
			}
		}

		// Token: 0x040000AB RID: 171
		private bool isArray;

		// Token: 0x040000AC RID: 172
		private bool isCollection;

		// Token: 0x040000AD RID: 173
		private bool isComposite;

		// Token: 0x040000AE RID: 174
		private bool isDataSet;

		// Token: 0x040000AF RID: 175
		private bool isDictionary;

		// Token: 0x040000B0 RID: 176
		private bool isEnum;

		// Token: 0x040000B1 RID: 177
		private bool isKeyValuePair;

		// Token: 0x040000B2 RID: 178
		private bool isNullable;

		// Token: 0x040000B3 RID: 179
		private bool isNumeric;

		// Token: 0x040000B4 RID: 180
		private bool isStruct;
	}
}
