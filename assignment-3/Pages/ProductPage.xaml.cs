using assignment_3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace assignment_3.Pages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            PopulateCombobox().ConfigureAwait(false);
            

        }
        private ObservableCollection<ProductModel> productcollection = new ObservableCollection<ProductModel>();

        private async void btn_Deleteproduct_Click(object sender, RoutedEventArgs e)
        {
            var productURL = "https://localhost:7072/api/Product";
            using var client = new HttpClient();
            var product = (ProductModel)cb_Products.SelectedItem;

            await client.DeleteAsync($"{productURL}?id={product.Id}");
            await PopulateCombobox();

            ClearText();
            MessageBox.Show("Product removed");
        }
        public async Task PopulateCombobox()
        {

            using var client = new HttpClient();
            var products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7072/api/Product");
            var productcollection = new ObservableCollection<ProductModel>();
           
            foreach (var product in products)
            {
                productcollection.Add(product);
            }
        
            cb_Products.ItemsSource = productcollection;
        }

        private void cb_Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = (ProductModel)cb_Products.SelectedItem;
            if (product != null)
            {
                tb_Productname.Text = product.Name;
                tb_Description.Text = product.Description;
                tb_Productprice.Text = product.Price.ToString();
            }
        }

        private async void btn_Updateproduct_Click(object sender, RoutedEventArgs e)
        {
            var productURL = "https://localhost:7072/api/Product";
            using var client = new HttpClient();
            var product = (ProductModel)cb_Products.SelectedItem;
            product.Name = tb_Productname.Text;
            product.Description = tb_Description.Text;
            product.Price = decimal.Parse(tb_Productprice.Text);

            await client.PutAsJsonAsync($"{productURL}?id={product.Id}", product);
            
            ClearText();

            await PopulateCombobox();

        }

        public void ClearText()
        {
            tb_Productname.Text = "";
            tb_Description.Text = "";
            tb_Productprice.Text = "";
        }

        

        private async void btn_Createproduct_Click(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();

            
            await client.PostAsJsonAsync("https://localhost:7072/api/Product", new ProductRequest
            {
                Name = tb_Productname.Text,
                Description = tb_Description.Text,
                Price = decimal.Parse(tb_Productprice.Text)
            });

            ClearText();

            await PopulateCombobox();
        }
    }
}
