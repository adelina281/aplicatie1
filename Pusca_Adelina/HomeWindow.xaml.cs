using System;
using System.Collections.Generic;
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
	/// Interaction logic for HomeWindow.xaml
	/// </summary>
	public partial class HomeWindow : Window
	{

		public HomeWindow()
		{
			InitializeComponent();
		}

		private void adminBtn_Click(object sender, RoutedEventArgs e)
		{
			MainWindow  administratorWindow = new MainWindow();
			this.Hide();
			administratorWindow.Show();
		}

		private void shopBtn_Click(object sender, RoutedEventArgs e)
		{
			ShopWindow shopWindow = new ShopWindow();
			this.Hide();
			shopWindow.Show();

		}
	}
}
