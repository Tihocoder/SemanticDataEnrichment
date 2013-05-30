using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticDataEnrichment.Core
{
	public class WorkConsloeException : Exception
	{
		private string ConsoleErrors { get; set; }

		public WorkConsloeException(string message) 
			: this(message, String.Empty, null)
		{ }

		public WorkConsloeException(string message, string consoleErrors)
			: this(message, consoleErrors, null)
		{ }

		public WorkConsloeException(string message, Exception innerException)
			: this(message, String.Empty, innerException)
		{ }

		public WorkConsloeException(string message, string consoleErrors, Exception innerException)
			: base(message, innerException)
		{
			ConsoleErrors = consoleErrors;
		}
	}
}
