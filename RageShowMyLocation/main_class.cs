using Rage;
using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Rage.Attributes.Plugin("RageShowMyLocation", Description = "Shows current location, Speed Limit etc", Author = "waski35")]

namespace RageShowMyLocation
{
    public class RageShowMyLocationClass 
    {
        private static Rage.Vector3 pl_pos = new Vector3(0,0,0);
        private static String Loc = "";
        private static int option_pos_x = 500;
        private static int option_pos_y = 0;
        private static string option_font_name = "Arial";
        private static int option_font_size = 14;
        private static string option_interstate_color = "Red";
        private static string option_route_color = "Yellow";
        private static string option_city_color = "White";
        private static int option_developer = 0;
        private static int option_metric = 0;
        private static Rage.Graphics graf;
        private static string plug_ver = "RageShowMyLocation " + typeof(RageShowMyLocationClass).Assembly.GetName().Version;

       
        public static void Main()
        {
                      
            Game.FrameRender += new EventHandler<GraphicsEventArgs>((obj,graf_ev) => DisplayPos(obj,graf_ev));
            Game.LogTrivial(plug_ver + " : Added event handler for FrameRender");
            ReadSettings();
            Game.LogTrivial(plug_ver + " : Plugin loaded !");

            while (true)
            {
                GameFiber.Yield();
            }
                            
        }
        private static void ReadSettings()
        {
            string line = "";
            string path = Directory.GetCurrentDirectory();
            path = path + "\\Plugins\\RageShowMyLocation.ini";
            if (File.Exists(path))
            {
                Game.LogTrivial(plug_ver + " : found settings file, adjusting settings.");
                Game.LogTrivial(plug_ver + " : Settings File path : " + path);
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
                    if (line.Contains("metric_units="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_metric = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("developer_mode="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_developer = Convert.ToInt32(line.Substring(index_start + 1));
                    }

                }

                file.Close();
            }

        }
        private static string GetSpeedLimit(String str)
        {
            string ret = "";
            if (option_metric == 0)
            {
                ret = "Limit : " + Convert.ToString(GetSpeedLimitINT(str)) + " MPH";
            }
            else if (option_metric == 1)
            {
                ret = "Limit : " + Convert.ToString(Math.Round(GetSpeedLimitINT(str) * 1.6,1)) + " KMPH";
            }
            else
            {
                ret = "Limit : " + Convert.ToString(GetSpeedLimitINT(str)) + " MPH";
            }
            return ret;
        }
        public static int GetSpeedLimitINT(String str)
        {
            int ret = 0;
            Ped ped = Game.LocalPlayer.Character;
            pl_pos = ped.Position;
            if (str.Contains("Los Santos Freeway") || str.Contains("Los Santos Fwy") || str.Contains("Del Perro") || str.Contains("Olympic") || str.Contains("La Puerta"))
            {
                if (str.Contains("City"))
                {
                    ret = 45;
                }
                else
                {
                    ret = 60;
                }
            }
            else if (str.Contains("Great Ocean") || str.Contains("Tongva Dr") || str.Contains("Senora Fwy") || str.Contains("Palomino Fwy") || str.Contains("Senora Freeway") || str.Contains("Palomino Freeway") || str.Contains("Elysian Fields") || str.Contains("Route") || str.Contains("Droga") || str.Contains("68"))
            {
                if (str.Contains("City"))
                {
                    ret = 45;
                }
                else
                {
                    ret = 50;
                }
            }
            else if (str.Contains("Senora Rd"))
            {
                if (str.Contains("County"))
                {
                    ret = 60;
                }
                else
                {
                    ret = 35;
                }
            }
            else if (str.Contains("Panorama Dr"))
            {
                ret = 50;
            }
            else if (str.Contains("Joshua Rd"))
            {
                if (pl_pos.Y >= 3483.0)
                {
                    ret = 50;
                }
                else
                {
                    ret = 60;
                }
            }
            else if (str.Contains("Baytree Canyon Rd"))
            {
                if ((pl_pos.X >= -426.0 && pl_pos.Y <= 2123.0) && (pl_pos.X <= 70.0 && pl_pos.Y >= 1698.0))
                {
                    ret = 35;
                }
                else
                {
                    ret = 50;
                }
            }
            else if (str.Contains("County"))
            {
                ret = 45;
            }
            else if (str.Contains("City"))
            {
                ret = 35;
            }
            else
            {
                ret = 0;
            }
            return ret;
        }
        private static String GetColor(String str)
        {
            string ret = "White";
            if (str.Contains("Los Santos Freeway") || str.Contains("Los Santos Fwy") || str.Contains("Del Perro Fwy") || str.Contains("Olympic Fwy") || str.Contains("La Puerta Fwy"))
            {
                ret = "Red";
            }
            else if (str.Contains("Great Ocean") || str.Contains("Tongva Dr") || str.Contains("Senora Fwy") || str.Contains("Palomino Fwy") || str.Contains("Senora Freeway") || str.Contains("Palomino Freeway") || str.Contains("Elysian Fields") || str.Contains("Route") || str.Contains("Droga") || str.Contains("Hwy") || str.Contains("Highway"))
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
            street = street + ", " + GetSpeedLimit(street);
            street = street + " - Time - " + GetCurTime();
            street = street + " | " + GetDirection() + " | ";
            street = street + "Speed : " + GetPlayerSpeed();
            if (option_developer > 0)
            {
                street = street + ", POS - X : " + Convert.ToString(pl_pos.X) + ", Y : " + Convert.ToString(pl_pos.Y) + ", Z : " + Convert.ToString(pl_pos.Z);
            }
            
            
            
            PointF pt = new PointF(option_pos_x, option_pos_y);
            Color text_col = Color.FromName(GetColor(street));
            eva.Graphics.DrawText(street, option_font_name, option_font_size, pt, text_col);

            
        }
        private static String GetCurTime()
        {
            String ret = "";
            TimeSpan time_mili = World.TimeOfDay;
            String godz = Convert.ToString(time_mili.Hours);
            String minuta = Convert.ToString(time_mili.Minutes);
            if (godz.Length < 2)
            {
                godz = "0" + godz;
            }
            if (minuta.Length < 2)
            {
                minuta = "0" + minuta;
            }
            ret = godz + ":" + minuta;
            return ret;
            
        }
               
        private static String GetDistrict(String street)
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
                county = "Los Santos County";
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
                county = "Los Santos County";
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
            else if (street.Contains("Del Perro"))
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
                if (pl_pos.Y > 206.0)
                {
                    county = "Los Santos County";
                }
                else
                {
                    county = "Los Santos City";
                }
            }
            else if (street.Contains("El Burro"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("El Rancho"))
            {
                if (Math.Abs(pl_pos.X) > 1060)
                {
                    county = "Los Santos County";
                }
                else
                {
                    county = "Los Santos City";
                }
            }
            else if (street.Contains("Elysian Fields"))
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
                county = "Los Santos County";
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
            else if (street.Contains("Great Ocean"))
            {
                if (pl_pos.Y > -340.0 && pl_pos.Y < 2540)
                {
                    county = "Los Santos County";
                }
                else if (pl_pos.Y < -340)
                {
                    county = "Los Santos City";
                }
                else if (pl_pos.Y > 2540)
                {
                    county = "Blaine County";
                }
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
                county = "Los Santos County";
            }
            else if (street.Contains("Labor"))
            {
                if (Math.Abs(pl_pos.X) > 1100.0)
                {
                    county = "Los Santos County";
                }
                else
                {
                    county = "Los Santos City";
                }
            }
            else if (street.Contains("La Puerta"))
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
            else if (street.Contains("Los Santos"))
            {
                if (pl_pos.Y > -149.0)
                {
                    county = "Los Santos County";
                }
                else
                {
                    county = "Los Santos City";
                }
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
                if (pl_pos.Y > -149.0)
                {
                    county = "Los Santos County";
                }
                else
                {
                    county = "Los Santos City";
                }
            }
            else if (street.Contains("Occupation"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Olympic"))
            {
                if (pl_pos.X < -1060)
                {
                    county = "Los Santos County";
                }
                else
                {
                    county = "Los Santos City";
                }
            }
            else if (street.Contains("Orchardville"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Palomino Ave"))
            {
                county = "Los Santos City";
            }
            else if (street.Contains("Palomino Fwy"))
            {
                county = "Los Santos County";
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
            else if (street.Contains("Senora Rd"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Senora Fwy"))
            {
                if (pl_pos.Y > 2930)
                {
                    county = "Blaine County";
                }
                else
                {
                    county = "Los Santos County";
                }
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
                county = "Los Santos County";
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
            else if (street.Contains("Zancudo Grande Valley"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("Zancudo Rd"))
            {
                county = "Los Santos County";
            }
            else if (street.Contains("68"))
            {
                county = "Los Santos County";
            }
            else
            {
                county = "Blaine County";
            }
            if (street.Contains("Los Santos Freeway") || street.Contains("Los Santos Fwy") || street.Contains("Del Perro Fwy") || street.Contains("Olympic Fwy") || street.Contains("La Puerta Fwy") || street.Contains("Interstate") || street.Contains("Route") || street.Contains("Droga") || street.Contains("Międzystan") || street.Contains("Great Ocean") || street.Contains("Hwy") || street.Contains("Fwy") || street.Contains("Freeway") || street.Contains("Highway") || street.Contains("Great Ocean") || street.Contains("Tongva Dr") || street.Contains("Senora Fwy") || street.Contains("Palomino Fwy") || street.Contains("Senora Freeway") || street.Contains("Palomino Freeway") || street.Contains("Elysian Fields"))
            {
                county = county + ", SA Highway System";
            }

            return county;
        }
       private static String GetDirection ()
        {
            String direction = "";
            Ped ped = Game.LocalPlayer.Character;
            float heading_degrees = ped.Heading;
            if ((heading_degrees > 350.0  && heading_degrees <= 0.0) || (heading_degrees >= 0.0 && heading_degrees < 15.0))
            {
                direction = "N";
            }
            else if (heading_degrees > 15.0 && heading_degrees < 80.0)
            {
                direction = "NW";
            }
            else if (heading_degrees > 80.0 && heading_degrees < 105.0)
            {
                direction = "W";
            }
            else if (heading_degrees > 105.00 && heading_degrees < 165.0)
            {
                direction = "SW";
            }
            else if (heading_degrees > 165.0 && heading_degrees < 195.0)
            {
                direction = "S";
            }
            else if (heading_degrees > 195.0 && heading_degrees < 255.0)
            {
                direction = "SE";
            }
            else if (heading_degrees > 255.0 && heading_degrees < 285.0)
            {
                direction = "E";
            }
            else if (heading_degrees > 285.0 && heading_degrees < 350.0)
            {
                direction = "NE";
            }

            return direction;
        }
        private static String GetPlayerSpeed()
       {
           String speed = "";
           Ped ped = Game.LocalPlayer.Character;
           float speed_meters = ped.Speed;
           double speed_kmh = (Convert.ToDouble(speed_meters) * 3.6);
            if (option_metric == 1)
            {
                speed_kmh = Math.Round(speed_kmh, 1);
                speed = Convert.ToString(speed_kmh) + " KMPH";
            }
            else
            {
                double speed_mph = (speed_kmh * 0.621371192);
                speed_mph = Math.Round(speed_mph,1);
                speed = Convert.ToString(speed_mph) + " MPH";
            }
           return speed;
       }

    }
}
