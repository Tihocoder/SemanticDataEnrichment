using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SemanticDataEnrichment.Core.ModelComponents;

namespace SemanticDataEnrichment.Core.SemanticElements
{
	/// <summary>
	/// Объект онтологии
	/// </summary>
	public class SemanticObject : SemanticElement
	{
		public SemanticObject(string internalName)
			: this(internalName, string.Empty)
		{ }

		public SemanticObject(string internalName, string friendlyName)
		{
			this.internalName = internalName;
			this.friendlyName = friendlyName;
			Elements = new ObservableCollection<SemanticElement>();
		}

		/// <summary>
		/// Составные элементы объекта
		/// </summary>
		public ObservableCollection<SemanticElement> Elements
		{
			get;
			private set;
		}

		public override string ToString()
		{
			return Name;
		}

		public override string GetHtml()
		{
			StringBuilder div = new StringBuilder();
			div.AppendFormat("<div itemscope itemtype=\"{0}/{1}\">", RdfNamespace, InternalName);
			div.AppendLine();
			foreach (var content in Elements)
			{
				div.AppendLine(content.GetHtml());
			}
			div.AppendLine("</div>");
			return div.ToString();
		}
	}
}
