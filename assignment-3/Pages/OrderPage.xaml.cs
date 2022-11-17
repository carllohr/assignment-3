using assignment_3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        
        public OrderPage()
        {
            InitializeComponent();
            PopulateCombobox().ConfigureAwait(false);
            lvProducts.ItemsSource = products;
        }

        private ObservableCollection<ProductModel> products = new ObservableCollection<ProductModel>();
        public async Task PopulateCombobox()
        {
            using var client = new HttpClient();

            var customers = await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7072/api/Customer");
            var products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7072/api/Product");
            var productcollection = new ObservableCollection<ProductModel>();
            var customercollection = new ObservableCollection<CustomerModel>();

            foreach (var product in products)
            {
                productcollection.Add(product);
            }
            foreach (var customer in customers)
            {
                customercollection.Add(customer);
            }

            cb_Products.ItemsSource = productcollection;
            cb_Customers.ItemsSource = customercollection;
        }
       

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            var product = (ProductModel)cb_Products.SelectedItem;

            if (product != null)
            {
                if (tb_Quantity.Text == "")
                {
                    product.Quantity = 1;
                }
                else 
                {
                    product.Quantity = int.Parse(tb_Quantity.Text);
                }
                    
                products.Add(product);
                
            }
            cb_Products.SelectedIndex = -1;

        }

        private async void btn_Createorder_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://localhost:7072/api/Order";
            using var client = new HttpClient();
            var customer = (CustomerModel)cb_Customers.SelectedItem;
            decimal rowPrice = 0;
            decimal totalPrice = 0;
            foreach (var item in products)
            {
                rowPrice = item.Price * item.Quantity;
                totalPrice += item.Price * item.Quantity;
            }
            if (customer != null && products.Count > 0)
            {
                try
                {
                    await client.PostAsJsonAsync($"{url}", new OrderRequest
                    {
                        CustomerId = customer.Id,
                        OrderDate = DateTime.Now,
                        Products = products,
                        Quantity = products.Count,
                        TotalPrice = totalPrice,
                        RowPrice = rowPrice

                    }) ;

                    MessageBox.Show($"Order successfully created for customer {customer.FullName} for {totalPrice.ToString()} sek amount");
                    products.Clear();
                }
                catch { }
            }
            else
            {
                MessageBox.Show("You need to select a customer and add products to place an order");
            }
        }

        private void btn_Clearorder_Click(object sender, RoutedEventArgs e)
        {
            products.Clear();
            lvProducts.Items.Refresh();
        }
    }
}
