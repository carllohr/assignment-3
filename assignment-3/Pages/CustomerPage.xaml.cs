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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
            PopulateCombobox().ConfigureAwait(false);
        }
        public async Task PopulateCombobox() // gets list of customers from database to combobox
        {

            using var client = new HttpClient();
            var customers = await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7072/api/Customer");
            var customerCollection = new ObservableCollection<CustomerModel>();

            foreach (var customer in customers)
            {
                customerCollection.Add(customer);
            }

            cb_Customers.ItemsSource = customerCollection;
        }
        private async void btn_Createcustomer_Click(object sender, RoutedEventArgs e) // creates customer with security checks to avoid empty information
        {
            var url = "https://localhost:7072/api/Customer";
            using var client = new HttpClient();
            if (tb_Firstname.Text != "" && tb_Lastname.Text != "" && tb_Email.Text != "")
            {
                await client.PostAsJsonAsync(url, new CustomerRequest
                {
                    FirstName = tb_Firstname.Text,
                    LastName = tb_Lastname.Text,
                    Email = tb_Email.Text,
                });
                await PopulateCombobox();
                ClearText();
            }
            else
            {
                MessageBox.Show("You need to fill in all the fields to create a customer");
            }
        }

        private async void btn_Updatecustomer_Click(object sender, RoutedEventArgs e) // button method to update customer information, with some security checks
        {
            var url = "https://localhost:7072/api/Customer";
            using var client = new HttpClient();

            var customer = (CustomerModel)cb_Customers.SelectedItem;
            if (tb_Firstname.Text != "" && tb_Lastname.Text != "" && tb_Email.Text != "")
            {
                customer.FirstName = tb_Firstname.Text;
                customer.LastName = tb_Lastname.Text;
                customer.Email = tb_Email.Text;

                await client.PutAsJsonAsync($"{url}?id={customer.Id}", customer);
                await PopulateCombobox();
                ClearText();
                cb_Customers.SelectedIndex = -1;
                MessageBox.Show("Customer information successfully updated");
            }
            else
            {
                MessageBox.Show("You cannot update a customer with empty information. Fill in all the fields to proceed");
            }
        }

        public void ClearText()
        {
            tb_Firstname.Text = "";
            tb_Lastname.Text = "";
            tb_Email.Text = "";
        }

        private void cb_Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = (CustomerModel)cb_Customers.SelectedItem;

            if (customer != null)
            {
                tb_Firstname.Text = customer.FirstName;
                tb_Lastname.Text = customer.LastName;
                tb_Email.Text = customer.Email;
            }
        }

        private async void btn_Deletecustomer_Click(object sender, RoutedEventArgs e) // deletes customer
        {
            var customerURL = "https://localhost:7072/api/Customer";
            using var client = new HttpClient();
            var customer = (CustomerModel)cb_Customers.SelectedItem;

            await client.DeleteAsync($"{customerURL}?id={customer.Id}");
            await PopulateCombobox();

            ClearText();
            MessageBox.Show("Customer removed");
        }
    }
}
