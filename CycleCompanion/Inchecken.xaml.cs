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
        public static bool ingechecked { get; set; }
        public static bool once { get; set; }

        public static Location startLocation { get; set; }

        public string userName
        {
            get
            {
                return Profiel.naam;
            }
        }
        public Inchecken()
        {
            BindingContext = this;
            InitializeComponent();
            if (ingechecked)
            {
                incheckbutton.Opacity = 0.5;
            } else
            {
                uitcheckbutton.Opacity = 0.5;
            }
            
        }

        public async void Navigate_Main(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
        public void CheckInButton(object sender, EventArgs e)
        {

            backgroundLocation();
            

        }

        public async void backgroundLocation()
        {


            if (once == false)
            {
                once = true;
                ingechecked = true;
                uitcheckbutton.Opacity = 1.0;
                incheckbutton.Opacity = 0.5;
                startLocation = null;
                Statistieken.begintijd = DateTime.Now;
                string connectionString = Configuration.getConnectionString();
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                Location previousLocation = null;
                Location myLocation;
                DateTime previousTime = DateTime.Now;
                DateTime currentTime;
                while (ingechecked)
                {
                    myLocation = null;
                    try
                    {
                        var request = new GeolocationRequest(GeolocationAccuracy.Best);
                        var location = await Geolocation.GetLocationAsync(request);
                        if (location != null)
                        {
                            myLocation = location;
                            if (previousLocation == null)
                            {
                                previousLocation = location;
                                await DisplayAlert("Ingechecked!", "U bent nu ingechecked. Uw tijd loopt en uw locatie is zichtbaar.", "Ok");
                            }
                            if (startLocation == null)
                            {
                                startLocation = location;
                            }

                        }
                    }
                    catch (FeatureNotSupportedException)
                    {
                        Console.WriteLine("Feature not supported.");
                        ingechecked = false;
                        connection.Close();
                        await DisplayAlert("Feature not supported error", "Gps feature is not supported on current platform", "Ok");
                        break;
                    }
                    catch (FeatureNotEnabledException)
                    {
                        Console.WriteLine("Feature not enabled.");
                        ingechecked = false;
                        connection.Close();
                        await DisplayAlert("Feature not enabled error", "Please make sure the gps feature on your phone works", "Ok");
                        break;
                    }
                    catch (PermissionException)
                    {
                        Console.WriteLine("Please enable permission to get location.");
                        await DisplayAlert("Permission error", "Please enable permission to access location", "Ok");
                        ingechecked = false;
                        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                        connection.Close();
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unable to get location");
                        ingechecked = false;
                        connection.Close();
                        await DisplayAlert("Unable to get location", "Sorry, we can't get your current location", "Ok");
                        break;
                    }
                    if (myLocation != null)
                    {
                        string deelnemerid = "" + Profiel.deelnemerId;
                        currentTime = DateTime.Now;
                        double myspeed = calculateSpeed(currentTime, previousTime, previousLocation, myLocation);
                        string tijd = currentTime.ToString("HH:mm:ss");
                        string query = "INSERT INTO " +
                            "`Locaties`(`DeelnemerID`, `LocatieID`, `Tijd`, `YCoordinaat`, `XCoordinaat`, `Snelheid`) " +
                            $"VALUES({deelnemerid}, NULL, '{tijd}', {myLocation.Latitude}, {myLocation.Longitude}, {myspeed});";
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = query;
                        var reader = command.ExecuteNonQuery();
                        previousLocation = myLocation;
                        previousTime = currentTime;
                        Thread.Sleep(5000);
                        Statistieken.Update_Data();
                    }
                }
                Statistieken.huidigesnelheid = 0;
                Statistieken.eindtijd = DateTime.Now;
                Statistieken.Update_Data();
                connection.Close();
            }
        }

        public double calculateSpeed(DateTime currentTime, DateTime previousTime, Location previousLocation, Location CurrentLocation)
        {
            double distance = CurrentLocation.CalculateDistance(previousLocation, DistanceUnits.Kilometers);
            double timepass = (currentTime - previousTime).TotalHours;
            double speed = distance / timepass;
            Statistieken.huidigesnelheid = speed;
            if (speed > Statistieken.maxsnelheid)
            {
                Statistieken.maxsnelheid = speed;
            }
            double gemdistance = CurrentLocation.CalculateDistance(startLocation, DistanceUnits.Kilometers);
            double gemtimepass = (currentTime - Statistieken.begintijd).TotalHours;
            Statistieken.gemsnelheid = gemdistance / gemtimepass;
            Statistieken.afstand = gemdistance;

            return speed;
        }

        public void CheckOutButton(object sender, EventArgs e)
        {
            
            if (ingechecked == true)
            {
                uitcheckbutton.Opacity = 0.5;
                Statistieken.eindtijd = DateTime.Now;
                ingechecked = false;
                DisplayAlert("Uitgechecked", "U bent nu uitgechecked.", "Ok");
                Console.WriteLine("stopped");
            }
            
        }
    }
}
