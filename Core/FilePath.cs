using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SemanticDataEnrichment.Core.ModelComponents;

namespace SemanticDataEnrichment.Core
{
	public class FilePath : NotifyPropertyChangedBase
	{
		public FilePath()
		{ }

		public FilePath(string filePath)
		{
			Value = filePath;
		}

		private string _value;
		public string Value 
		{ 
			get { return this._value; }
			set 
			{ 
				this._value = value;
				OnPropertyChanged("Value");
			} 
		}

		public static explicit operator String(FilePath obj)
		{
			return obj.Value;
		}

		public static explicit operator FilePath(String path)
		{
			return new FilePath(path);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
