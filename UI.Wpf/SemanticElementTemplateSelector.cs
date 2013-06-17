using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SemanticDataEnrichment.UI.Wpf
{
	internal class SemanticElementTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
		{
			return base.SelectTemplate(item, container);
		}
	}
}
