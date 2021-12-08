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
            if (ingechecked == false)
            {
                DisplayAlert("Ingechecked!", "U bent nu ingechecked. Uw tijd loopt en uw locatie is zichtbaar.", "Ok");
            }
            
            Thread tOne = new Thread(backgroundLocation);
            tOne.Start();


        }

        public async void backgroundLocation()
        {


            if (ingechecked == false)
            {
                ingechecked = true;
                startLocation = null;
                Statistieken.begintijd = DateTime.Now;
                Statistieken.Update_Data();
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
                            Console.WriteLine($"Latitude: {location.Latitude}\n" +
                                              $"Longitude: {location.Longitude}\n" +
                                              $"Altitude: {location.Altitude}\n" +
                                              $"Speed: {location.Speed}\n");
                            myLocation = location;
                            if (previousLocation == null)
                            {
                                previousLocation = location;
                            }
                            if (startLocation == null)
                            {
                                startLocation = location;
                            }

                        }
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        Console.WriteLine("Feature not supported.");
                        ingechecked = false;
                        connection.Close();
                        await DisplayAlert("Feature not supported error", "Gps feature is not supported on current platform", "Ok");
                        break;
                    }
                    catch (FeatureNotEnabledException fneEx)
                    {
                        Console.WriteLine("Feature not enabled.");
                        ingechecked = false;
                        connection.Close();
                        await DisplayAlert("Feature not enabled error", "Please make sure the gps feature on your phone works", "Ok");
                        break;
                    }
                    catch (PermissionException pEx)
                    {
                        Console.WriteLine("Please enable permission to get location.");
                        await DisplayAlert("Permission error", "Please enable permission to access location", "Ok");
                        ingechecked = false;
                        connection.Close();
                        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to get location");
                        ingechecked = false;
                        connection.Close();
                        await DisplayAlert("Unable to get location", "Sorry, we can't get your current location", "Ok");
                    }
                    if (myLocation != null)
                    {
                        string deelnemerid = "" + Profiel.deelnemerId;
                        currentTime = DateTime.Now;
                        string tijd = currentTime.ToString("HH:mm:ss");
                        string query = "INSERT INTO " +
                            "`Locaties`(`DeelnemerID`, `LocatieID`, `Tijd`, `YCoordinaat`, `XCoordinaat`) " +
                            $"VALUES({deelnemerid}, NULL, '{tijd}', {myLocation.Latitude}, {myLocation.Longitude});";
                        Console.WriteLine(query);
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = query;
                        var reader = command.ExecuteNonQuery();
                        calculateSpeed(currentTime, previousTime, previousLocation, myLocation);
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

        public void calculateSpeed(DateTime currentTime, DateTime previousTime, Location previousLocation, Location CurrentLocation)
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
        }

        public void CheckOutButton(object sender, EventArgs e)
        {
            if (ingechecked == true)
            {
                Statistieken.eindtijd = DateTime.Now;
                ingechecked = false;
                DisplayAlert("Uitgechecked", "U bent nu uitgechecked.", "Ok");
            }
            
        }
    }
}
