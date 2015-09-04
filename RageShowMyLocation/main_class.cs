using Rage;
using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Rage.Attributes.Plugin("RageShowMyLocation", Description = "Shows current location.", Author = "waski35")]

namespace RageShowMyLocation
{
    public class RageShowMyLocationClass 
    {
        public static Rage.Vector3 pl_pos = new Vector3(0,0,0);
        public static String Loc = "";
        static int option_pos_x = 500;
        static int option_pos_y = 0;
        static string option_font_name = "Arial";
        static int option_font_size = 14;
        static Rage.Graphics graf;

       
        public static void Main()
        {
                      
            Game.FrameRender += new EventHandler<GraphicsEventArgs>((obj,graf_ev) => DisplayPos(obj,graf_ev));
            ReadSettings();
            while (true)
            {
              
                //DisplayPos();
                GameFiber.Yield();
            }   
            
            

                
        }
        public static void ReadSettings()
        {
            string line = "";
            string path = Directory.GetCurrentDirectory();
            path = path + "\\Plugins\\RageShowMyLocation.ini";
            if (File.Exists(path))
            {
                Game.Log("RageShowMyLocation : found settings file, adjusting settings.");
                Game.Log("RageShowMyLocation : Settings File path : " + path);
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                int index_start = 0;
                int index_stop = 0;
                char[] usun_zn = { ';', ',', '.', '#', '/', '\\',' '};
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Trim();
                    line = line.Trim(usun_zn);
                    if (line.Contains("pos_x="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_x = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("pos_y="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_y = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("font_name="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_font_name = Convert.ToString(line.Substring(index_start + 1));
                    }
                    if (line.Contains("font_size="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_font_size = Convert.ToInt32(line.Substring(index_start + 1));
                    }

                }

                file.Close();
            }

        }
        public static String GetColor(String str)
        {
            string ret = "White";
            if (str.Contains("Interstate") || str.Contains("Międzystan") || string.Equals(str, "Fwy", StringComparison.OrdinalIgnoreCase))
            {
                ret = "Red";
            }
            else if (str.Contains("Route") || str.Contains("Droga") || str.Contains("Great Ocean") || string.Equals(str, "Hwy", StringComparison.OrdinalIgnoreCase))
            {
                ret = "Yellow";
            }
            else
            {
                ret = "White";
            }
            return ret;
        }

        public static void DisplayPos(System.Object obj, GraphicsEventArgs eva)
        {
           
            Ped ped = Game.LocalPlayer.Character;
            pl_pos = ped.Position;
            String street = Rage.World.GetStreetName(pl_pos);

            //street = street + ", " + GetDistrict(street);
            street = street + ", " + GetCounty(street);
            //street = street + " - Time - " + GetCurTime();
            
            
            Point pt = new Point(option_pos_x, option_pos_y);
            Color text_col = Color.FromName(GetColor(street));
            eva.Graphics.DrawText(street, option_font_name, option_font_size, pt, text_col);

            
        }
        public static String GetCurTime()
        {
            String ret = "";
            uint time_mili = Game.GameTime;
            DateTime date_time = new DateTime();
            date_time.AddMilliseconds(Convert.ToDouble(time_mili));
            String godz = Convert.ToString(date_time.Hour);
            String minuta = Convert.ToString(date_time.Minute);
            ret = godz + ":" + minuta;
            return ret;
            
        }
               
        public static String GetDistrict(String street)
        {
            String dist = "";

            return dist;
        }
        
        public static String GetCounty(string street)
        {
            String county = "";
            if (street.Contains("Abattoir"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Abe Milton"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Ace Jones"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Adam's Apple"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Aguja"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Alta"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Amarillo"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Americano"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Atlee"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Autopia"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Banham Canyon"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Barbareno"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Bay City Ave"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Bay City Inc"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Baytree Canyon"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Del Perro"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Bridge"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Brouge"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Buccaneer"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Buen Vino"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Caesar"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Calais"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Capital"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Carcer"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Carson"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Chum"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Chupacabra"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Clinton"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Cockingend"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Conquistador"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Cortes"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Cougar"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Covenant"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Cox"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Crusade"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Davis"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Decker"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Didion"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Dorset Dr"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Dorset Pl"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Dry Dock"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Dunstable Dr"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Dutch London"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Eastbourne"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("East Galileo"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("East Mirror"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Eclipse"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Edwood"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Elgin"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("El Burro"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("El Rancho"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Equality"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Exceptionalists"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Fantastic"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Fenwell"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Forum"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Fudge"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Galileo"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Gentry"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Ginger"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Glory"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Goma"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Greenwich"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Grove"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Hanger"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Hangman"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Hardy"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Hawick"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Heritage"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Hillcrest"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Imagination"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Industry"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Ineseno"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Integrity"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Invention"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Innocence"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Jamestown"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Kimble Hill"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Kortz"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Labor"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Laguna"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Lake Vinewood"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Las Lagunas"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Liberty"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Lindsay"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Little Bighorn"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Low Power"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Macdonald"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Mad Wayne Thunder"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Magellan"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Marathon"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Marlowe"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Melanoma"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Meteor"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Milton"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Mirror Park"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Mirror"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Morningwood"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Mount Haan"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Mount Vinewood"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Movie Star"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Mutiny"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("New Empire"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Normandy"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Nikola"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("North Archer"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("North Conker"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("North Sheldon"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("North Rockford"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Occupation"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Orchardville"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Palomino"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Peaceful"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Perth"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Picture Perfect"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Plaice"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Playa"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Popular"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Portola"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Power"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Prosperity"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Red Desert"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Richman"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Rockford"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Roy Lowenstein"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Rub"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Sam Austin"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("San Andreas"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Sandcastle"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("San Vitus"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Senora"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Shank"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Signal"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Sinner"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Sinners"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("South Arsenal"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("South Boulevard Del Perro"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("South Mo Milton"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("South Rockford"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("South Shambles"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Spanish"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Steele"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Strangeways"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Strawberry"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Supply"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Sustancia"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Swiss"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Tackle"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Tangerine"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Tongva"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Tower"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Tug"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Utopia"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Vespucci"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Vinewood"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Vitus"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Voodoo"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("West Eclipse"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("West Galileo"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("West Mirror"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Whispymound"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Wild Oats"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("York"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Zancudo Barranca"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Interstate") || street.Contains("Route") || street.Contains("Droga") || street.Contains("Międzystan") || street.Contains("Great Ocean") || string.Equals(street,"Hwy",StringComparison.OrdinalIgnoreCase) || string.Equals(street,"Fwy",StringComparison.OrdinalIgnoreCase))
            {
                county = "San Andreas Highway System";
            }
            else
            {
                county = "Blaine County";
            }

            return county;
        }
       

    }
}
