using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SemanticDataEnrichment.Core.SemanticElements;

namespace SemanticDataEnrichment.UI.Wpf
{
	/// <summary>
	/// Interaction logic for SemanticObjectUserControl.xaml
	/// </summary>
	public partial class SemanticObjectUserControl : UserControl
	{
		public SemanticObjectUserControl()
		{
			InitializeComponent();
		}

		private SemanticObject CurrentContext
		{
			get
			{
				return this.DataContext as SemanticObject;
			}
		}

		private void SemanticPropertyUserControl_RemoveButtonClick_1(object sender, RoutedEventArgs e)
		{
			CurrentContext.Elements.Remove((sender as FrameworkElement).DataContext as SemanticElement);
		}

		public event RoutedEventHandler RemoveButtonClick;

		private void RemoveButton_Click(object sender, RoutedEventArgs e)
		{
			if (!e.Handled && RemoveButtonClick != null)
				RemoveButtonClick(this, e);
		}
	}
}
