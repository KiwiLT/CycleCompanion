using System;
using System.Collections.Generic;
using MySqlConnector;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            entAppName.Text = "";
        }

        public void checkMail(object sender, EventArgs e)
        {
            string connectionString = Configuration.getConnectionString();
            var connection = new MySqlConnection(connectionString);
            string query = "SELECT `Email` FROM `Deelnemers`;";
            connection.Open();
            var cmd = new MySqlCommand(query, connection);
            MySqlDataReader emailreader = cmd.ExecuteReader();
            List<string> emails = new List<string>();
            while (emailreader.Read())
            {
                emails.Add(emailreader.GetString(0));
            }
            emailreader.Close();
            

            if (emails.Contains(entAppName.Text))
            {
                string userinfoquery = "SELECT * FROM `Deelnemers WHERE `Email`= " + entAppName.Text + ";";
                connection.Close();
                Navigate_Menu(sender, e);
            } else
            {
                connection.Close();
                DisplayAlert("Onjuiste Email", "Voer alstublieft het email address van een deelnemer in.", "Ok");
            }

        }

        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
    }

}