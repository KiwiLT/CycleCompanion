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
                }
                else
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
                }
                else
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
                }
                else
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
                }
                else
                {
                    return "0.5";
                }
            }
        }
        public string BadgeFive
        {
            get
            {
                if ((Statistieken.begintijd - Statistieken.eindtijd > TimeSpan.FromHours(1.0) && Statistieken.eindtijd != default(DateTime))
                    || (Inchecken.ingechecked && Statistieken.begintijd - DateTime.Now > TimeSpan.FromHours(1.0)))
                {
                    return "1.0";
                }
                else
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
                }
                else
                {
                    return "0.5";
                }
            }
        }

        public Badge()
        {
            BindingContext = this;
            InitializeComponent();
            if (Statistieken.eindtijd == default(DateTime))
            {
                certbanner.Opacity = 0.5;
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
        public async void Navigate_Profiel(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Profiel());
        }
        public async void Navigate_Statistieken(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Statistieken());
        }

        void btnSend_Clicked(object sender, System.EventArgs e)
        {
            if (Statistieken.eindtijd != default(DateTime))
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("mail.dewielrenners.com");

                    mail.From = new MailAddress("info@dewielrenners.com");
                    string mailTo = Profiel.email;
                    mail.To.Add(mailTo);
                    mail.Subject = "De Wielrenners Certificaat";
                    mail.IsBodyHtml = true;
                    DateTime thisDay = DateTime.Today;
                    mail.Body = "<!DOCTYPE HTML> <HTML> <HEAD> <STYLE type='text/css'> body {margin-top: 0px;margin-left: 0px;} #page_1 {position:relative; overflow: hidden;margin: 3px 0px 0px 0px;padding: 0px;border: none;width: 1123px;height: 790px;} #page_1 #p1dimg1 {position:absolute;top:0px;left:0px;z-index:-1;width:1123px;height:790px;} #page_1 #p1dimg1 #p1img1 {width:1123px;height:790px;} #page_2 {position:relative; overflow: hidden;margin: 793px 0px 0px 0px;padding: 0px;border: none;width: 0px;height: 0px;} .dclr {clear:both;float:none;height:1px;margin:0px;padding:0px;overflow:hidden;} .ft0{font: 27px 'Century Gothic';line-height: 33px;} .ft1{font: 67px 'Century Gothic';color: #540eb1;line-height: 88px;} .ft2{font: 16px 'Century Gothic';line-height: 21px;} .ft3{font: 16px 'Century Gothic';margin-left: 5px;line-height: 21px;} .ft4{font: bold 16px 'Century Gothic';line-height: 19px;} .p0{color:#8549d9 !important;text-align: left;padding-left: 529px;margin-top: 183px;margin-bottom: 0px;} .p1{color:#8549d9 !important;text-align: left;padding-left: 529px;padding-right: 286px;margin-top: 14px;margin-bottom: 0px;} .p2{color:#8549d9 !important;text-align: left;padding-left: 529px;padding-right: 198px;margin-top: 20px;margin-bottom: 0px;} .p3{color:#8549d9 !important;text-align: left;padding-left: 529px;margin-top: 25px;margin-bottom: 0px;} .p4{color:#8549d9 !important;text-align: left;padding-left: 529px;margin-top: 2px;margin-bottom: 0px;} .p5{color:#8549d9 !important;text-align: left;padding-left: 529px;margin-top: 29px;margin-bottom: 0px;} </STYLE> </HEAD> <BODY> <DIV id='page_1'> <DIV id='p1dimg1'> <IMG src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAABGMAAAMWCAIAAADah9yvAAAgAElEQVR4nO3df4zf9X3Y8Y/vsLED5iCQJgQINYGD/ABCICHbQqibrk0cZKm3fcNWaZu09o9NUyddq6HUyrpN2typ23KrIk2VNlXb2krJvq2rXom7assOQteKzJSGhtR8+eGkgBxTfh2usbGxb38cOR+vO5/vvt/P5/N+fz6fx+Mvc3Bfv2MHyU9er8/7s2lhYaHI21x/kPoIJdjZmxz9Q/bNtOGXoiiKqekSfjWW27urJb8ye/aP9CvTm5gt6yQJ9ed3pz4CAEAxlvoAAAAA2VFKAAAAkVICAACIlBIAAECklCAXI17nAABAiZQSAABApJQAAAAipdQkpb+GCAAAWJVSAgAAiJQSAABApJQAAAAipVSTuf4g9REAAID1UkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKAAAAkVICAACIlBIAAECklAAAACKlBAAAECklAACASCkBAABESgkAACBSSgAAAJFSAgAAiJQSAABApJQAAAAipQQAABApJSAj/fndqY8AAFAUSgkAAGAlpQQt0ZuYTX0EAID2UEoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKDTM1PZn6CAAA0H5KCQAAIFJKAAAAkVICAACIlBIAAECklAAAACKlBAAAECklAACASCkBAABEDSilnT3vWgUAAGrVgFICAAComVICAACIlBIAAECklAAAACKlBAAAECklAACASCnVZ64/SH0EAABgXZQSAABApJQAAAAipQQAABApJQAAgEgpAQAAREoJAAAgUkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKAAAAkVICAACIlBIAAECklAAAACKlBAAAECklAACASCkBAABESgkAACBSSgAAAJFSAgAAiJQSZGHP/snURwAA4CylBAAAECklAACASCnReHt3DVIfAQCAtlFKAAAAkVICAACIlBIAAECklAAAACKlBAAAECklAACASCkBAABESgkAACBSSgAAAJFSAgAAiJQSAABApJQAAAAipQQAABApJQAAgEgpAQAAREoJAAAgUkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKzTM1PZn6CAAA0HJKCQAAIFJKAAAAkVICAACIlBIAAECklAAAACKlBAAAECklAACASCkBAABESgkAACBSSrWa6w9SHwEAADg/pQTkoj+/O/URAADeopQAAAAipQQAABApJQAAgEgpAQAAREoJAAAgUkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKAAAAkVICAACIlBIAAECklAAAACKlBAAAECklAACASCkBAABESgkAACBSSgAAAJFSAgAAiJQSAABApJSgDXoTs6mPAADQKkoJAAAgUkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKAAAAkVICAACIlBIAAECklAAAACKlBAAAECklAACASCkBAABESgkAACBSSgAAAJFSAgAAiJQSAABApJQAAAAipQQAABApJQAAgEgpAQAAREoJAAAgUkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKAAAAkVICAACIlBIAAECklAAAACKlBAAAECmlus31B6mPAAAAnIdSAgAAiJQSAABApJQAAAAipQQAABApJQAAgEgpAQAAREoJAAAgUkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKQEAAERKCQAAIFJKAAAAkVICAACIlBIAAECklGi2vbsGqY8AAEALKSUAAIBIKUF6e/ZPpj4CAABvo5QAAAAipQQAABApJQAAgEgpAQAAREoJAAAgUkoAAACRUgIAAIiUEgAAQKSUAAAAIqUEAAAQKSUAAIBIKTXS1PRk6iMAAECbKSUAAIBIKQEAAERKCQAAIFJKb3PJ+F+lPgIAAJCeUnqb26c+mvoIufjArQsT215JfYr0tm0fT32EXGz5obGf/607Up8CAKAmSumsG255Z1EUl114LPVBsvCBH73x0//oztSnSG/6q+8XS4v+9W/f9Ym/+d7UpwAAqIlSOuvqG68oiuIju29LfZD0towfT32ELIxfsKkoiumvvj/1QbKw45aJoigufv8FqQ8CAFAHpcQqPvITOxZ/cM1Vr6U9SVpbtvkX5C03/93LF3/wi//1b6Q9CQBAPfxB8C033fHupR9fc03X/6v51TdesviDj32+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' id='p1img1'></DIV> <DIV class='dclr'></DIV> <P class='p0 ft0'>Certificaat van deelname</P> <P class='p1 ft1'>" + Profiel.naam + " " + Profiel.achternaam + "</P> <P class='p2 ft2'>Bedankt voor je deelname aan de ‘Wielrenner’ tour!</P> <P class='p3 ft2'><SPAN class='ft2'>" + Statistieken.gemsnelheid + "</SPAN><SPAN class='ft3'>km/h</SPAN></P> <P class='p4 ft4'>Gemiddelde snelheid</P> <P class='p5 ft2'>" + Profiel.nummer + "</P> <P class='p4 ft4'>Nummer</P> <P class='p5 ft2'><NOBR>" + thisDay.ToString("d") + "</NOBR></P> <P class='p4 ft4'>Datum</P> </DIV> <DIV id='page_2'> </DIV> </BODY> </HTML>";

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
            else
            {
                DisplayAlert("U heeft nog niet gefietst!", "U kunt uw certificaat bekijken als u klaar bent met fietsen." , "OK");
            }

        }
    }
}
