using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using MySqlConnector;

namespace CycleCompanion
{
    public partial class Inchecken : ContentPage
    {
        public bool ingechecked { get; set; }

        public string userName
        {
            get
            {
                return "Kees";
            }
        }
        public Inchecken()
        {
            BindingContext = this;
            InitializeComponent();
        }

        public async void Navigate_Main(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
        public async void CheckInButton(object sender, EventArgs e)
        {
            ingechecked = true;
            string connectionString = Configuration.getConnectionString();
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            while (ingechecked)
            {
                Location myLocation = null;
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location != null)
                    {
                        Console.WriteLine($"Latitude: {location.Latitude}\n" +
                                          $"Longitude: {location.Longitude}\n" +
                                          $"Altitude: {location.Altitude}\n" +
                                          $"Speed: {location.Speed}\n");
                        myLocation = location;

                    }
                }
                catch (FeatureNotSupportedException fnsEx) { Console.WriteLine("Feature not supported."); }
                catch (FeatureNotEnabledException fneEx) { Console.WriteLine("Feature not enabled."); }
                catch (PermissionException pEx) { Console.WriteLine("Please enable permission to get location."); }
                catch (Exception ex) { Console.WriteLine("Unable to get location"); }
                if (myLocation != null)
                {
                    string deelnemerid = "" + Profiel.deelnemerId;
                    string tijd = DateTime.Now.ToString("HH:mm:ss");
                    string query = "INSERT INTO " +
                        "`Locaties`(`DeelnemerID`, `LocatieID`, `Tijd`, `YCoordinaat`, `XCoordinaat`) " +
                        $"VALUES({deelnemerid}, NULL, '{tijd}', {myLocation.Latitude}, {myLocation.Longitude});";
                    Console.WriteLine(query);
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    var reader = await command.ExecuteNonQueryAsync();
                    Thread.Sleep(5000);
                } 
            }
            connection.Close();

        }
        public void CheckOutButton(object sender, EventArgs e)
        {
            ingechecked = false;
        }
    }
}
