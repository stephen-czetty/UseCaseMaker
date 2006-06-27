using System;

namespace UseCaseMakerControls
{
	/// <summary>
	/// Descrizione di riepilogo per MouseOverTokenEventArgs.
	/// </summary>
	public class MouseOverTokenEventArgs : EventArgs
	{
		private string token = string.Empty;
		private LinkEnabledRTB item = null;

		public MouseOverTokenEventArgs(LinkEnabledRTB item, string token)
		{
			this.item = item;
			this.token = token;
		}

		public string Token
		{
			get
			{
				return this.token;
			}
		}

		public LinkEnabledRTB Item
		{
			get
			{
				return this.item;
			}
		}
	}
}
