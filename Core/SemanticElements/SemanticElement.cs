using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SemanticDataEnrichment.Core.ModelComponents;

namespace SemanticDataEnrichment.Core.SemanticElements
{
	/// <summary>
	/// Абстрактный элемент онтологии
	/// </summary>
	public abstract class SemanticElement : NotifyPropertyChangedBase
	{
		protected SemanticElement()
		{ }

		protected string internalName;
		/// <summary>
		/// Внутреннее имя элемента в схеме онтологии
		/// </summary>
		public string InternalName
		{
			get { return this.internalName; }
		}

		protected string friendlyName;
		/// <summary>
		/// Значение внутреннего имени элемента онтологии
		/// </summary>
		public string FriendlyName
		{
			get { return this.friendlyName; }
			set
			{
				this.friendlyName = value;
				OnPropertyChanged("FriendlyName");
				OnPropertyChanged("Name");
			}
		}

		/// <summary>
		/// Имя элемента онтологии - FriendlyName, если не заполнено, то InternalName
		/// </summary>
		public string Name
		{
			get
			{
				return String.IsNullOrWhiteSpace(FriendlyName) ? InternalName : FriendlyName;
			}
			set
			{
				FriendlyName = value;
			}
		}

		protected string rdfNamespace;
		public string RdfNamespace
		{
			get { return this.rdfNamespace; }
			set
			{
				this.rdfNamespace = value;
				OnPropertyChanged("RdfNamespace");
			}
		}

		public abstract string GetHtml();

		//public ObservableCollection<SemanticElement> ParentEnumerable { get; set; }

		//public void RemoveParentEnumerable()
		//{
		//	if (ParentEnumerable == null || !ParentEnumerable.Contains(this))
		//		return;

		//	ParentEnumerable.Remove(this);
		//}
	}
}
