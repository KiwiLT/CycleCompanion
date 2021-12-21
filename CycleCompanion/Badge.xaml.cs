using System;
using System.Collections.Generic;
using System.Net.Mail;
using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Badge : ContentPage
    {
        public static bool hasBadgeOne { get; set; }

        public string BadgeOne
        {
            get
            {
                if (Statistieken.begintijd != default(DateTime))
                {
                    return "1.0";
                } else
                {
                    return "0.5";
                }
            }
        }

        public string BadgeTwo
        {
            get
            {
                if (Statistieken.maxsnelheid >= 40.0)
                {
                    return "1.0";
                } else
                {
                    return "0.5";
                }
            }
        }
        public string BadgeThree
        {
            get
            {
                if (Statistieken.afstand >= 50.0)
                {
                    return "1.0";
                } else
                {
                    return "0.5";
                }
            }
        }

        public string BadgeFour
        {
            get
            {
                if (Statistieken.afstand >= 100.0)
                {
                    return "1.0";
                } else
                {
                    return "0.5";
                }
            }
        }
        public string BadgeFive
        {
            get
            {
                if (Statistieken.begintijd - Statistieken.eindtijd > TimeSpan.FromHours(1.0))
                {
                    return "1.0";
                } else
                {
                    return "0.5";
                }
            }
        }

        public string BadgeSix
        {
            get
            {
                if (Statistieken.eindtijd != default(DateTime))
                {
                    return "1.0";
                } else
                {
                    return "0.5";
                }
            }
        }

        public Badge()
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
        public async void Navigate_Profiel(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Profiel());
        }
        void btnSend_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.dewielrenners.com");

                mail.From = new MailAddress("info@dewielrenners.com");
                string mailTo = "nisatutucu@gmail.com";
                mail.To.Add(mailTo);
                mail.Subject = "De Wielrenners Certificaat";
                mail.Body = "Gefeliciteerd!";

                SmtpServer.Port = 587;
                SmtpServer.Host = "mail.dewielrenners.com";
                SmtpServer.EnableSsl = false;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("info@dewielrenners.com", "mB0ZNSBvN");

                SmtpServer.Send(mail);

                DisplayAlert("Gelukt!", $"Certificaat is gemaild naar uw e-mailadres, {mailTo}!", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Faild", ex.Message, "OK");
            }
        }
    }
}
