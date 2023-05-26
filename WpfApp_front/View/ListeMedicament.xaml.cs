using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
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
using Newtonsoft.Json;


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

                MessageBox.Show(this, "Le médicament " + message.Split(",")[1].Split(":")[1].Replace('"', ' ') + " à bien été ajouté" , "Message", MessageBoxButton.OK);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            this.GetMedicaments();
        }

        private async void GetMedicaments()
        {
            var response = await client.GetStringAsync("https://localhost:7020/api/Medicament/GetAll");
            var medicaments = JsonConvert.DeserializeObject<List<Medicament>>(response);
            MedicamentList.DataContext = medicaments;

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var Medicament = new Medicament()
            {
                MedocId = Convert.ToInt32(txtId.Text),
                Name = txtNom.Text,
                Description = txtDesc.Text,
            };
            string message = string.Empty;
            this.UpdateMedoc(Medicament);
        }

        private async void UpdateMedoc(Medicament medoc)
        {
            await client.PutAsJsonAsync("https://localhost:7020/api/Medicament/"+medoc.MedocId, medoc);
        }

        private async void DeleteMedoc(int medocId)
        {
            await client.DeleteAsync("https://localhost:7020/api/Medicament/" + medocId);
        }


        private void BtnUpdateMedicament(object sender, RoutedEventArgs e)
        {
            Medicament medicament = ((FrameworkElement)sender).DataContext as Medicament;
            txtId.Text = medicament.MedocId.ToString();
            txtNom.Text = medicament.Name;
            txtDesc.Text = medicament.Description;
            MessageBox.Show(this, "Modifié", "Message", MessageBoxButton.OK);
            this.UpdateMedoc(medicament);
        }

        private void BtnDeleteMedicament(object sender, RoutedEventArgs e)
        {
            Medicament medoc = ((FrameworkElement)sender).DataContext as Medicament;
            this.DeleteMedoc(medoc.MedocId);
            MessageBox.Show(this, "Supprimé", "Message", MessageBoxButton.OK);
            this.GetMedicaments();

        }
    }
}
