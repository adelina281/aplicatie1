using AutoLotModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pusca_Adelina
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
		CollectionViewSource productViewSource;

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			System.Windows.Data.CollectionViewSource productViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productViewSource")));
			this.productViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productViewSource")));
			this.productViewSource.Source = ctx.Products.Local;
			ctx.Products.Load();
		}

		private void productDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//instantiem Customer entity
				Product product = new Product()
				{
					Size = sizeTextBox.Text.Trim(),
					Name = nameTextBox.Text.Trim(),
					Price = float.Parse(priceTextBox.Text.Trim()),
				};
				//adaugam entitatea nou creata in context
				ctx.Products.Add(product);
				productViewSource.View.Refresh();
				//salvam modificarile
				ctx.SaveChanges();
			}
			//using System.Data;
			catch (Exception)
			{
				MessageBox.Show("The price must be a number!");
			}


		}

		private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			HomeWindow homeWindow = new HomeWindow();
			this.Hide();
			homeWindow.Show();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Product product = (Product)productDataGrid.SelectedItem;
				ctx.Products.Remove(product);
				ctx.SaveChanges();
			}
			catch (Exception)
			{
				MessageBox.Show("A product must be selected!");
			}
			productViewSource.View.Refresh();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			Product product = new Product();
			try
			{
				product = (Product)productDataGrid.SelectedItem;
				product.Size = sizeTextBox.Text.Trim();
				product.Name = nameTextBox.Text.Trim();
				product.Price = float.Parse(priceTextBox.Text.Trim());
				//salvam modificarile
				ctx.SaveChanges();
			}
			catch (Exception)
			{
				MessageBox.Show("The price must be a number!");
			}
			productViewSource.View.Refresh();
			// pozitionarea pe item-ul curent
			productViewSource.View.MoveCurrentTo(product);
		}

		private void productDataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
		{

		}

		private void sizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}
