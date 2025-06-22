using System;

namespace Main.Internal.Performance
{
	// Token: 0x02000067 RID: 103
	internal struct CodeMarkerStartEnd : IDisposable
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000456C File Offset: 0x0000276C
		internal CodeMarkerStartEnd(int begin, int end, bool correlated = false)
		{
			this._buffer = (correlated ? CodeMarkers.AttachCorrelationId(null, Guid.NewGuid()) : null);
			this._end = end;
			this.CodeMarker(begin);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00004593 File Offset: 0x00002793
		public void Dispose()
		{
			if (this._end != 0)
			{
				this.CodeMarker(this._end);
				this._end = 0;
				this._buffer = null;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000045B7 File Offset: 0x000027B7
		private void CodeMarker(int id)
		{
			if (this._buffer == null)
			{
				CodeMarkers.Instance.CodeMarker(id);
				return;
			}
			CodeMarkers.Instance.CodeMarkerEx(id, this._buffer);
		}

		// Token: 0x040001B2 RID: 434
		private int _end;

		// Token: 0x040001B3 RID: 435
		private byte[] _buffer;
	}
}
