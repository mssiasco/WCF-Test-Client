using System;

namespace Main.Internal.Performance
{
	// Token: 0x02000068 RID: 104
	internal struct CodeMarkerExStartEnd : IDisposable
	{
		// Token: 0x0600031D RID: 797 RVA: 0x000045E0 File Offset: 0x000027E0
		internal CodeMarkerExStartEnd(int begin, int end, byte[] aBuff, bool correlated = false)
		{
			this._aBuff = (correlated ? CodeMarkers.AttachCorrelationId(aBuff, Guid.NewGuid()) : aBuff);
			this._end = end;
			CodeMarkers.Instance.CodeMarkerEx(begin, this._aBuff);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00004613 File Offset: 0x00002813
		internal CodeMarkerExStartEnd(int begin, int end, Guid guidData, bool correlated = false)
		{
			this = new CodeMarkerExStartEnd(begin, end, guidData.ToByteArray(), correlated);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00004626 File Offset: 0x00002826
		internal CodeMarkerExStartEnd(int begin, int end, string stringData, bool correlated = false)
		{
			this = new CodeMarkerExStartEnd(begin, end, CodeMarkers.StringToBytesZeroTerminated(stringData), correlated);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00004638 File Offset: 0x00002838
		internal CodeMarkerExStartEnd(int begin, int end, uint uintData, bool correlated = false)
		{
			this = new CodeMarkerExStartEnd(begin, end, BitConverter.GetBytes(uintData), correlated);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000464A File Offset: 0x0000284A
		internal CodeMarkerExStartEnd(int begin, int end, ulong ulongData, bool correlated = false)
		{
			this = new CodeMarkerExStartEnd(begin, end, BitConverter.GetBytes(ulongData), correlated);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000465C File Offset: 0x0000285C
		public void Dispose()
		{
			if (this._end != 0)
			{
				CodeMarkers.Instance.CodeMarkerEx(this._end, this._aBuff);
				this._end = 0;
				this._aBuff = null;
			}
		}

		// Token: 0x040001B4 RID: 436
		private int _end;

		// Token: 0x040001B5 RID: 437
		private byte[] _aBuff;
	}
}
