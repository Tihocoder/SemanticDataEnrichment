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

namespace SemanticDataEnrichment.UI.Wpf
{
	/// <summary>
	/// Interaction logic for SemanticPropertyUserControl.xaml
	/// </summary>
	public partial class SemanticPropertyUserControl : UserControl
	{
		public SemanticPropertyUserControl()
		{
			InitializeComponent();
		}

		public event RoutedEventHandler RemoveButtonClick;

		private void RemoveButton_Click(object sender, RoutedEventArgs e)
		{
			if (!e.Handled && RemoveButtonClick != null)
				RemoveButtonClick(this, e);
		}
	}
}
