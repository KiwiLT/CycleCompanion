using System;
using System.Collections.Generic;
using MySqlConnector;
using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Geschiedenis : ContentPage
    {
        public static (double, string)[] speedhis { get; set; }



        public string speed1 { get { return speedhis[0].Item1 + " km/u"; } }
        public string time1 { get { return speedhis[0].Item2 + ""; } }
        public string speed2 { get { return speedhis[1].Item1 + " km/u"; } }
        public string time2 { get { return speedhis[1].Item2 + ""; } }
        public string speed3 { get { return speedhis[2].Item1 + " km/u"; } }
        public string time3 { get { return speedhis[2].Item2 + ""; } }
        public string speed4 { get { return speedhis[3].Item1 + " km/u"; } }
        public string time4 { get { return speedhis[3].Item2 + ""; } }
        public string speed5 { get { return speedhis[4].Item1 + " km/u"; } }
        public string time5 { get { return speedhis[4].Item2 + ""; } }
        public string speed6 { get { return speedhis[5].Item1 + " km/u"; } }
        public string time6 { get { return speedhis[5].Item2 + ""; } }
        public string speed7 { get { return speedhis[6].Item1 + " km/u"; } }
        public string time7 { get { return speedhis[6].Item2 + ""; } }
        public string speed8 { get { return speedhis[7].Item1 + " km/u"; } }
        public string time8 { get { return speedhis[7].Item2 + ""; } }
        public string speed9 { get { return speedhis[8].Item1 + " km/u"; } }
        public string time9 { get { return speedhis[8].Item2 + ""; } }
        public string speed10 { get { return speedhis[9].Item1 + " km/u"; } }
        public string time10 { get { return speedhis[9].Item2 + ""; } }
        public string speed11 { get { return speedhis[10].Item1 + " km/u"; } }
        public string time11 { get { return speedhis[10].Item2 + ""; } }
        public string speed12 { get { return speedhis[11].Item1 + " km/u"; } }
        public string time12 { get { return speedhis[11].Item2 + ""; } }
        public string speed13 { get { return speedhis[12].Item1 + " km/u"; } }
        public string time13 { get { return speedhis[12].Item2 + ""; } }
        public string speed14 { get { return speedhis[13].Item1 + " km/u"; } }
        public string time14 { get { return speedhis[13].Item2 + ""; } }
        public string speed15 { get { return speedhis[14].Item1 + " km/u"; } }
        public string time15 { get { return speedhis[14].Item2 + ""; } }
        public string speed16 { get { return speedhis[15].Item1 + " km/u"; } }
        public string time16 { get { return speedhis[15].Item2 + ""; } }


        public Geschiedenis()
        {
            speedhis = new (double, string)[16];
            
            
            
            string connectionString = Configuration.getConnectionString();
            var connection = new MySqlConnection(connectionString);
            string geschiedenisquery = "SELECT `Snelheid`, `Tijd` FROM `Locaties` WHERE `DeelnemerID`=" + Profiel.deelnemerId + " AND MINUTE(`Tijd`) IN (0, 15, 30, 45) AND SECOND(`TIJD`) IN (0, 1,2,3,4) ORDER BY `LocatieID` DESC LIMIT 16;";
            
            connection.Open();
            var cmd = new MySqlCommand(geschiedenisquery, connection);
            MySqlDataReader geschiedenisreader = cmd.ExecuteReader();

            int i = 0;
            while (geschiedenisreader.Read())
            {
                (double, string) spdata = (geschiedenisreader.GetDouble(0), geschiedenisreader.GetTimeSpan(1).ToString().Substring(0,5));
                speedhis[i] = spdata;
                i++;
            }
            geschiedenisreader.Close();
            connection.Close();

            BindingContext = this;
            InitializeComponent();
            while (i < 16)
            {
                    speedhis[i] = (0, "00:00:00");
                    i++;
            }

        }






        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
        public async void Navigate_Profiel(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Profiel());
        }
        public async void Navigate_Badge(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Badge());
        }
    }
}

