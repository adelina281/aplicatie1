using AutoLotModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Pusca_Adelina
{
	/// <summary>
	/// Interaction logic for ShopWindow.xaml
	/// </summary>
	public partial class ShopWindow : Window
	{
		AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
		CollectionViewSource productViewSource;

		ObservableCollection<ShopItemTable> ItemsTable = new ObservableCollection<ShopItemTable>();
		List<ShopItem> Items = new List<ShopItem>();

		public ShopWindow()
		{
			InitializeComponent();
			DataContext = this;
			dataGrid.ItemsSource = ItemsTable;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			HomeWindow homeWindow = new HomeWindow();
			this.Hide();
			homeWindow.Show();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			System.Windows.Data.CollectionViewSource productViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productViewSource")));
			this.productViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productViewSource")));
			this.productViewSource.Source = ctx.Products.Local;
			ctx.Products.Load();
			System.Windows.Data.CollectionViewSource shopItemTableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			// shopItemTableViewSource.Source = [generic data source]
		}

		private void nameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void descriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void addBtn_Click(object sender, RoutedEventArgs e)
		{

			try
			{
				Product product = new Product()
				{
					Size = descriptionTextBox.Text.Trim(),
					Name = nameComboBox.Text.Trim(),
					Price = float.Parse(priceTextBox.Text.Trim())
				};
				int quantity = int.Parse(quantityTextBox.Text.Trim());
				this.Items.Add(new ShopItem(product, quantity));
				this.ItemsTable.Add(new ShopItemTable(product, quantity));
				this.computeTotalPrice();

				// resetez campurile de pentru adaugare
				nameComboBox.Text = "";
				quantityTextBox.Text = "";
				priceTextBox.Text = "";
			}
			catch (Exception)
			{
				MessageBox.Show("The quantity must be a number!");
			}
		}

		public void computeTotalPrice()
		{
			double total = 0;
			for (int i = 0; i < this.Items.Count(); i++)
			{
				total += this.Items[i].Total;
			}
			this.totalTextBox.Text = total.ToString() + " RON";
		}

		private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{

		}

		private void totalTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		// Actiuni pentru butonul de plasare comanda
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < this.Items.Count(); i++)
			{
				ctx.SaveChanges();
				productViewSource.View.Refresh();
				// pozitionarea pe item-ul curent
				productViewSource.View.MoveCurrentTo(this.Items[i].Product);
			}

			this.Items.Clear();
			this.ItemsTable.Clear();
			this.computeTotalPrice();
			MessageBox.Show("The order was completed successfully!");
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void deleteBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ShopItemTable item = (ShopItemTable)dataGrid.SelectedItem;
				int index = this.ItemsTable.IndexOf(item);
				this.ItemsTable.Remove(item);
				this.Items.Remove(this.Items[index]);
				this.computeTotalPrice();

			}
			catch (Exception)
			{
				MessageBox.Show("A product must be selected!");
			}

		}

		private void nameComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{

		}

		private void deleteBtn_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				ShopItemTable item = (ShopItemTable)dataGrid.SelectedItem;
				int index = this.ItemsTable.IndexOf(item);
				this.ItemsTable.Remove(item);
				this.Items.Remove(this.Items[index]);
				this.computeTotalPrice();

			}
			catch (Exception)
			{
				MessageBox.Show("A product must be selected!");
			}
		}
	}

	public class ShopItem
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public double Total { get; set; }

		public ShopItem(Product product, int quantity)
		{
			this.Product = product;
			this.Quantity = quantity;
			this.Total = (double)product.Price * quantity;
		}
	}

	public class ShopItemTable
	{
		public String ProductName { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public double Total { get; set; }

		public ShopItemTable(Product product, int quantity)
		{
			this.ProductName = product.Name;
			this.Price = (double)product.Price;
			this.Quantity = quantity;
			this.Total = (double)product.Price * quantity;
		}
	}
}
