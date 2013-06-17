using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SemanticDataEnrichment.Core.SemanticElements;

namespace SemanticDataEnrichment.UI.Wpf
{
	/// <summary>
	/// Interaction logic for SemanticElementsWindow.xaml
	/// </summary>
	public partial class SemanticElementsWindow : Window
	{
		public SemanticElementsWindow(ISemanticModel dataContext)
		{
			InitializeComponent();
			DataContext = dataContext;
		}

		public ISemanticModel CurrentContext
		{
			get
			{
 				return this.DataContext as ISemanticModel;
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void SemanticObjectUserControl_RemoveButtonClick_1(object sender, RoutedEventArgs e)
		{
			(CurrentContext.SemanticElements as ObservableCollection<SemanticElement>).Remove((sender as FrameworkElement).DataContext as SemanticElement);
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CurrentContext.CreateSemanticHTML();
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
