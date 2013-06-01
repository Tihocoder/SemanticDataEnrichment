using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SemanticDataEnrichment.UI.Wpf
{
	/// <summary>
	/// Ждущий курсор. Используется в блоке using
	/// </summary>
	internal class WaitCursor : IDisposable
	{
		private Cursor previousCursor;

		public WaitCursor()
		{
			this.previousCursor = Mouse.OverrideCursor;
			Mouse.OverrideCursor = Cursors.Wait;
		}

		#region IDisposable members

		public void Dispose()
		{
			Mouse.OverrideCursor = this.previousCursor;
		}

		#endregion
	}
}
