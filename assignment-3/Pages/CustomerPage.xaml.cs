using assignment_3.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
        }

        private async void btn_Addcustomer_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://localhost:7072/api/Customer";
            using var client = new HttpClient();

            await client.PostAsJsonAsync(url, new CustomerRequest
            {
                FirstName = tb_Firstname.Text,
                LastName = tb_Lastname.Text,
                Email = tb_Email.Text,
            });


        }

        private void btn_Updatecustomer_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://localhost:7072/api/Customer";
            using var client = new HttpClient();

            //var customer = (ProductModel)cb_Products.SelectedItem;
            //product.Name = tb_Productname.Text;
            //product.Description = tb_Description.Text;
            //product.Price = decimal.Parse(tb_Productprice.Text);

            //await client.PutAsJsonAsync($"{productURL}?id={product.Id}", product);
        }
    }
}
