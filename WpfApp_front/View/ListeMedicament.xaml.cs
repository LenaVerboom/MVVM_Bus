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
using System.Windows.Shapes;
using WpfApp_front.Model;

namespace WpfApp_front.View
{
    /// <summary>
    /// Logique d'interaction pour ListeMedicalement.xaml
    /// </summary>
    public partial class ListeMedicalement : Window
    {
        HttpClient client;
        public ListeMedicalement()
        {
            client = new HttpClient();
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = await client.GetAsync("https://localhost:7020/api/Medicament/1");
            response.EnsureSuccessStatusCode(); 
            if (response.IsSuccessStatusCode) {
                Message.Content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                Message.Content = "Server error";
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(txtNom.Text) || String.IsNullOrEmpty(txtDesc.Text))
            {
                MessageBox.Show(this, "Le champ est vide", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var Medicament = new Medicament()
                {
                    Name = txtNom.Text,
                    Description = txtDesc.Text,
                };
                string message = string.Empty;

                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7020/api/Medicament/",Medicament);
                    
                    response.EnsureSuccessStatusCode();

                    message = await response.Content.ReadAsStringAsync();  
                } catch(Exception ex) 
                {
                    message = ex.Message;
                }

                MessageBox.Show(this, message, "Message", MessageBoxButton.OK);
            }
        }
    }
}
