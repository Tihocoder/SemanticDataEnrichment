using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticDataEnrichment.Core.SemanticElements
{
	/// <summary>
	/// Логическое свойство какого-либо объекта онтологии
	/// </summary>
	public class SemanticProperty : SemanticElement
	{
		public SemanticProperty(string internalName)
			: this(internalName, string.Empty)
		{ }

		public SemanticProperty(string internalName, string value)
			: this(internalName, value, string.Empty)
		{ }

		public SemanticProperty(string internalName, string value, string friendlyName)
		{
			this.internalName = internalName;
			this.friendlyName = friendlyName;
			this.value = value;
		}

		private string value;
		/// <summary>
		/// Значение свойства онтологии
		/// </summary>
		public string Value
		{
			get { return this.value; }
			set
			{
				this.value = value;
				OnPropertyChanged("Value");
			}
		}

		public override string ToString()
		{
			return String.Format("{0}: '{1}'", Name, Value);
		}

		public override string GetHtml()
		{
			return String.Format("<meta itemprop=\"{0}\" content=\"{1}\">", InternalName, Value);
		}
	}
}
