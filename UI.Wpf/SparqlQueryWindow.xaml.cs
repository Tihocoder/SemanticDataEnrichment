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
using System.Windows.Shapes;
using SemanticDataEnrichment.Core;

namespace SemanticDataEnrichment.UI.Wpf
{
	/// <summary>
	/// Interaction logic for SparqlQueryWindow.xaml
	/// </summary>
	public partial class SparqlQueryWindow : Window
	{
		public SparqlQueryWindow()
		{
			InitializeComponent();
			this.DataContext = new RdfQueryViewModel();
			SetDefaultFiles();
		}

		public RdfQueryViewModel CurrentContext
		{
			get
			{
				return this.DataContext as RdfQueryViewModel;
			}
		}

		private void SetDefaultFiles()
		{
			CurrentContext.AddRdfFile("FdoDS.rdf");
			CurrentContext.AddRdfFile("output.rdf");
			CurrentContext.AddRdfFile("schema.owl");
		}

		private void AddFileButton_Click(object sender, RoutedEventArgs e)
		{
			CurrentContext.AddRdfFile(String.Empty);
		}

		private void SelectFileButton_Click(object sender, RoutedEventArgs e)
		{
			FrameworkElement element = sender as FrameworkElement;

			if (element == null)
				return;

			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog() { Filter = "Rdf, Owl and Xml|*.rdf,*.owl,*.xml|Все фалы|*.*", DefaultExt = "rdf" };
			bool? result = dlg.ShowDialog();
			if (result.GetValueOrDefault() != true)
				return;

			FilePath fp = element.DataContext as FilePath;
			fp.Value = dlg.FileName;
		}

		private void UnSelectFileButton_Click(object sender, RoutedEventArgs e)
		{
			FrameworkElement element = sender as FrameworkElement;

			if (element == null)
				return;

			CurrentContext.FilesToQuery.Remove(element.DataContext as FilePath);
		}

		private void ExecuteButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				using (new WaitCursor())
				{
					CurrentContext.ExecuteQuery();
				}
			}
			catch (Exception ex)
			{
				App.ShowError(ex);
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (CurrentContext != null
				&& (!String.IsNullOrWhiteSpace(CurrentContext.QueryResult) || !String.IsNullOrWhiteSpace(CurrentContext.SparqlQueryText))
				&& MessageBox.Show("Закрыть окно запросов?", "Закрыть", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
				e.Cancel = true;
		}
	}
}
