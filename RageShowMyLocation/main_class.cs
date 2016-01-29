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
        public static int option_pos_x = 500;
        public static int option_pos_y = 0;
        private static string option_font_name = "Arial";
        public static int option_font_size = 14;
        private static string option_interstate_color = "Red";
        private static string option_route_color = "Yellow";
        private static string option_city_color = "White";
        private static int option_developer = 0;
        private static int option_metric = 0;
        private static int option_12hourclock = 0;
        private static int option_rect_aroud_text = 0;
        private static int option_box_opacity = 100;
        private static int option_show_cords = 0;
        private static int option_show_zone = 0;
        private static int option_show_player_speed = 0;
        private static int option_show_time = 0;
        private static int option_show_heading = 9;
        private static Rage.Graphics graf;
        public static string plug_ver = "RageShowMyLocation " + typeof(RageShowMyLocationClass).Assembly.GetName().Version;
        private static Color text_col_all;
        private static string street = "";
        private static string heading = "";
        private static string time = "";
        private static string speed_lim = "";
        private static string player_speed = "";
        private static string coords = "";
        private static Color text_col_street;

        private static SizeF rect_size_street;
        private static PointF pt_rect_street;
        private static RectangleF rect_street;
        private static PointF pt_street;
        private static SizeF text_rect_street;

        private static SizeF rect_size_heading;
        private static PointF pt_rect_heading;
        private static RectangleF rect_heading;
        private static PointF pt_heading;
        private static Color text_col_heading;
        private static SizeF text_rect_heading;

        private static SizeF rect_size_time;
        private static PointF pt_rect_time;
        private static RectangleF rect_time;
        private static PointF pt_time;
        private static Color text_col_time;
        private static SizeF text_rect_time;

        private static SizeF rect_size_sl;
        private static PointF pt_rect_sl;
        private static RectangleF rect_sl;
        private static PointF pt_sl;
        private static Color text_col_sl;
        private static SizeF text_rect_sl;

        private static SizeF rect_size_cs;
        private static PointF pt_rect_cs;
        private static RectangleF rect_cs;
        private static PointF pt_cs;
        private static Color text_col_cs;
        private static SizeF text_rect_cs;

        private static SizeF rect_size_coords;
        private static PointF pt_rect_coords;
        private static RectangleF rect_coords;
        private static PointF pt_coords;
        private static Color text_col_coords;
        private static SizeF text_rect_coords;


        
        public static int option_pos_x_heading = 300;
        public static int option_pos_y_heading = 0;

        public static int option_pos_x_time = 350;
        public static int option_pos_y_time = 0;

        public static int option_pos_x_sl = 400;
        public static int option_pos_y_sl = 0;

        public static int option_pos_x_cs = 500;
        public static int option_pos_y_cs = 0;

        public static int option_pos_x_coords = 550;
        public static int option_pos_y_coords = 0;

        public static int option_font_size_heading = 14;
        public static int option_font_size_time = 14;
        public static int option_font_size_sl = 14;
        public static int option_font_size_cs = 14;
        public static int option_font_size_coords = 14;
    

       
        public static void Main()
        {

            Game.RawFrameRender += new EventHandler<GraphicsEventArgs>((obj, graf_ev) => DisplayPos(obj, graf_ev));
            Game.FrameRender += PositionControl.Process;
            Game.LogTrivial(plug_ver + " : Added event handler for FrameRender");
            ReadSettings();
            Game.LogTrivial(plug_ver + " : Plugin loaded !");

            PositionControl.InitMenus(); 

            while (true)
            {
                PrepareStreet();
                GameFiber.Yield();
            }
                            
        }
        public static void Reload()
        {
            ReadSettings();
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

                    if (line.Contains("pos_x_heading="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_x_heading = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("pos_y_heading="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_y_heading = Convert.ToInt32(line.Substring(index_start + 1));
                    }

                    if (line.Contains("pos_x_time="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_x_time = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("pos_y_time="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_y_time = Convert.ToInt32(line.Substring(index_start + 1));
                    }

                    if (line.Contains("pos_x_playerspeed="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_x_cs = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("pos_y_playerspeed="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_y_cs = Convert.ToInt32(line.Substring(index_start + 1));
                    }

                    if (line.Contains("pos_x_speedlimit="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_x_sl = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("pos_y_speedlimit="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_y_sl = Convert.ToInt32(line.Substring(index_start + 1));
                    }

                    if (line.Contains("pos_x_coords="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_x_coords = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("pos_y_coords="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_pos_y_coords = Convert.ToInt32(line.Substring(index_start + 1));
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

                    if (line.Contains("font_size_heading="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_font_size_heading = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("font_size_time="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_font_size_time = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("font_size_player_speed="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_font_size_cs = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("font_size_speed_limit="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_font_size_sl = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("font_size_coords="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_font_size_coords = Convert.ToInt32(line.Substring(index_start + 1));
                    }


                    if (line.Contains("metric_units="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_metric = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("do_not_touch_this="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_developer = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("12_hour_clock="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_12hourclock = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("display_box_around_text="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_rect_aroud_text = Convert.ToInt32(line.Substring(index_start + 1));
                    }
                    if (line.Contains("box_around_text_opacity="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_box_opacity = Convert.ToInt32(line.Substring(index_start + 1));
                        if (option_box_opacity < 0 || option_box_opacity > 255)
                        {
                            option_box_opacity = 100;
                        }
                    }
                    if (line.Contains("show_coords="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_show_cords = Convert.ToInt32(line.Substring(index_start + 1));
                       
                    }
                    if (line.Contains("show_zone="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_show_zone = Convert.ToInt32(line.Substring(index_start + 1));

                    }
                    if (line.Contains("show_time="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_show_time = Convert.ToInt32(line.Substring(index_start + 1));

                    }
                    if (line.Contains("show_player_speed="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_show_player_speed = Convert.ToInt32(line.Substring(index_start + 1));

                    }
                    if (line.Contains("show_heading="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        option_show_heading = Convert.ToInt32(line.Substring(index_start + 1));

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
                    ret = 60;
                }
                if (str.Contains("68"))
                {
                    if (pl_pos.X > 1566.0)
                    {
                        ret = 50;
                    }
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
                if ((pl_pos.X <= 426.0 && pl_pos.Y <= 2123.0) && pl_pos.Y >= 1698.0)
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
           
            

            if (option_rect_aroud_text > 0)
            {
                eva.Graphics.DrawRectangle(rect_street, Color.FromArgb(option_box_opacity, Color.Black));
                eva.Graphics.DrawText(street, option_font_name, option_font_size, pt_rect_street, text_col_street);

                eva.Graphics.DrawRectangle(rect_heading, Color.FromArgb(option_box_opacity, Color.Black));
                eva.Graphics.DrawText(heading, option_font_name, option_font_size, pt_rect_heading, text_col_all);
                
                eva.Graphics.DrawRectangle(rect_time, Color.FromArgb(option_box_opacity, Color.Black));
                eva.Graphics.DrawText(time, option_font_name, option_font_size, pt_rect_time, text_col_all);
                
                eva.Graphics.DrawRectangle(rect_sl, Color.FromArgb(option_box_opacity, Color.Black));
                eva.Graphics.DrawText(speed_lim, option_font_name, option_font_size, pt_rect_sl, text_col_all);
                
                eva.Graphics.DrawRectangle(rect_cs, Color.FromArgb(option_box_opacity, Color.Black));
                eva.Graphics.DrawText(player_speed, option_font_name, option_font_size, pt_rect_cs, text_col_all);
                
                eva.Graphics.DrawRectangle(rect_coords, Color.FromArgb(option_box_opacity, Color.Black));
                eva.Graphics.DrawText(coords, option_font_name, option_font_size, pt_rect_coords, text_col_all);
            }
            else
            {
                eva.Graphics.DrawText(street, option_font_name, option_font_size, pt_street, text_col_street);
                eva.Graphics.DrawText(heading, option_font_name, option_font_size, pt_heading, text_col_all);
                eva.Graphics.DrawText(speed_lim, option_font_name, option_font_size, pt_sl, text_col_all);
                eva.Graphics.DrawText(time, option_font_name, option_font_size, pt_time, text_col_all);
                eva.Graphics.DrawText(player_speed, option_font_name, option_font_size, pt_cs, text_col_all);
                eva.Graphics.DrawText(coords, option_font_name, option_font_size, pt_coords, text_col_all);
            }

            
        }
        private static void PrepareStreet()
        {
            Ped ped = Game.LocalPlayer.Character;
            pl_pos = ped.Position;
            street = Rage.World.GetStreetName(pl_pos);

            //street = street + ", " + GetDistrict(street);
            street = street + ", " + GetCounty(street);
            if (option_show_zone > 0)
            {
                street = street + ", " + GetPlayerZone();
            }
            
            speed_lim = " " + GetSpeedLimit(street);
            
            if (option_show_time > 0)
            {
                time = " - Time - " + GetCurTime();
            }
            if (option_show_heading > 0)
            {
                heading = " | " + GetDirection() + " | ";
            }
            if (option_show_player_speed > 0)
            {
                player_speed = " Speed : " + GetPlayerSpeed();
            }
            if (option_developer == 35 || option_show_cords > 0)
            {
                coords = " POS X : " + Convert.ToString(pl_pos.X) + ", Y : " + Convert.ToString(pl_pos.Y) + ", Z : " + Convert.ToString(pl_pos.Z);
            }
            
            


            

            text_col_street = Color.FromName(GetColor(street));
            text_col_all = Color.White;

            pt_street = new PointF(option_pos_x, option_pos_y);
            text_rect_street = new SizeF();
            text_rect_street = Rage.Graphics.MeasureText(street, option_font_name, option_font_size);
                       
            rect_size_street = new SizeF((text_rect_street.Width + ((text_rect_street.Width / street.Length) * 2)), text_rect_street.Height + (text_rect_street.Height / 2));
            pt_rect_street = new PointF(pt_street.X + ((rect_size_street.Width / street.Length) / 2), pt_street.Y + (text_rect_street.Height / 8));
            rect_street = new RectangleF(pt_street,rect_size_street);

            pt_heading = new PointF(option_pos_x_heading, option_pos_y_heading);
            text_rect_heading = new SizeF();
            text_rect_heading = Rage.Graphics.MeasureText(heading, option_font_name, option_font_size);

            rect_size_heading = new SizeF((text_rect_heading.Width + ((text_rect_heading.Width / heading.Length) * 2)), text_rect_heading.Height + (text_rect_heading.Height / 2));
            pt_rect_heading = new PointF(pt_heading.X + ((rect_size_heading.Width / heading.Length) / 2), pt_heading.Y + (text_rect_heading.Height / 8));
            rect_heading = new RectangleF(pt_heading, rect_size_heading);

            pt_time = new PointF(option_pos_x_time, option_pos_y_time);
            text_rect_time = new SizeF();
            text_rect_time = Rage.Graphics.MeasureText(time, option_font_name, option_font_size);

            rect_size_time = new SizeF((text_rect_time.Width + ((text_rect_time.Width / time.Length) * 2)), text_rect_time.Height + (text_rect_time.Height / 2));
            pt_rect_time = new PointF(pt_time.X + ((rect_size_time.Width / time.Length) / 2), pt_time.Y + (text_rect_time.Height / 8));
            rect_time = new RectangleF(pt_time, rect_size_time);

            pt_sl = new PointF(option_pos_x_sl, option_pos_y_sl);
            text_rect_sl = new SizeF();
            text_rect_sl = Rage.Graphics.MeasureText(street, option_font_name, option_font_size);

            rect_size_sl = new SizeF((text_rect_sl.Width + ((text_rect_sl.Width / speed_lim.Length) * 2)), text_rect_sl.Height + (text_rect_sl.Height / 2));
            pt_rect_sl = new PointF(pt_sl.X + ((rect_size_sl.Width / speed_lim.Length) / 2), pt_sl.Y + (text_rect_sl.Height / 8));
            rect_sl = new RectangleF(pt_sl, rect_size_sl);

            pt_cs = new PointF(option_pos_x_cs, option_pos_y_cs);
            text_rect_cs = new SizeF();
            text_rect_cs = Rage.Graphics.MeasureText(street, option_font_name, option_font_size);

            rect_size_cs = new SizeF((text_rect_cs.Width + ((text_rect_cs.Width / player_speed.Length) * 2)), text_rect_cs.Height + (text_rect_cs.Height / 2));
            pt_rect_cs = new PointF(pt_cs.X + ((rect_size_cs.Width / player_speed.Length) / 2), pt_cs.Y + (text_rect_cs.Height / 8));
            rect_cs = new RectangleF(pt_cs, rect_size_cs);

            pt_coords = new PointF(option_pos_x_coords, option_pos_y_coords);
            text_rect_coords = new SizeF();
            text_rect_coords = Rage.Graphics.MeasureText(street, option_font_name, option_font_size);

            rect_size_coords = new SizeF((text_rect_coords.Width + ((text_rect_coords.Width / coords.Length) * 2)), text_rect_coords.Height + (text_rect_coords.Height / 2));
            pt_rect_coords = new PointF(pt_coords.X + ((rect_size_coords.Width / coords.Length) / 2), pt_coords.Y + (text_rect_coords.Height / 8));
            rect_coords = new RectangleF(pt_coords, rect_size_coords);


        }
        private static String GetCurTime()
        {
            String ret = "";
            TimeSpan time_mili = World.TimeOfDay;
            int hours = time_mili.Hours;
            int minutes = time_mili.Minutes;
            String amPm = "AM";
            if (option_12hourclock == 1)
            {
                if (hours == 0)
                    hours = 12;
                else if (hours == 12)
                    amPm = "PM";
                else if (hours > 12)
                {
                    hours -= 12;
                    amPm = "PM";
                }
            }
            String godz = Convert.ToString(hours);
            String minuta = Convert.ToString(minutes);
            if (godz.Length < 2)
            {
                godz = "0" + godz;
            }
            if (minuta.Length < 2)
            {
                minuta = "0" + minuta;
            }
            ret = godz + ":" + minuta;
            if (option_12hourclock == 1)
            {
                ret = ret + amPm;
            }
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
        private static String GetPlayerZone()
        {
            String zone = "";
            
            Rage.Native.NativeArgument[] func_args = new Rage.Native.NativeArgument[3];
            func_args[0] = pl_pos.X;
            func_args[1] = pl_pos.Y;
            func_args[2] = pl_pos.Z;

            Encoding enc = Encoding.Unicode;



            string func_ret = (string)Rage.Native.NativeFunction.CallByHash(0xCD90657D4C30E1CA,typeof(string), func_args);
            //zone = enc.GetString(func_ret);

            zone = Translate_zone(func_ret);


            return zone;
        }

        private static void Calculate_pos_text_box(int pos_x, int pos_y)
        {
            pt_street = new PointF(pos_x, pos_y);
            text_rect_street = new SizeF();
            text_rect_street = Rage.Graphics.MeasureText(street, option_font_name, option_font_size);

            rect_size_street = new SizeF((text_rect_street.Width + ((text_rect_street.Width / street.Length) * 2)), text_rect_street.Height + (text_rect_street.Height / 2));
            pt_rect_street = new PointF(pt_street.X + ((rect_size_street.Width / street.Length) / 2), pt_street.Y + (text_rect_street.Height / 8));
            rect_street = new RectangleF(pt_street, rect_size_street);

        }

        private static String Translate_zone(string zone_op)
        {
            switch (zone_op)
            {
                case "AIRP":
                    return "Los Santos International Airport";
                case "ALAMO":
                    return "The Alamo Sea";
                case "ALTA":
                    return "Alta";
                case "ARMYB":
                    return "Fort Zancudo";
                case "BANHAMC":
                    return "Banham Canyon";
                case "BANNING":
                    return "Banning";
                case "BEACH":
                    return "Vespucci Beach";
                case "BHAMCA":
                    return "Banham Canyon Drive";
                case "BRADP":
                    return "Braddock Pass";
                case "BRADT":
                    return "Braddock Tunnel";
                case "BURTON":
                    return "Burton";
                case "CALAFB":
                    return "Calafia Bridge";
                case "CANNY":
                    return "Raton Canyon";
                case "CCREAK":
                    return "Cassidy Creek";
                case "CHAMH":
                    return "Chamberlain Hills";
                case "CHIL":
                    return "Vinewood Hills";
                case "CHU":
                    return "Chumash";
                case "CMSW":
                    return "Chiliad Mountain State Wilderness";
                case "CYPRE":
                    return "Cypress Flats";
                case "DAVIS":
                    return "Davis";
                case "DELBE":
                    return "Del Perro Beach";
                case "DELPE":
                    return "Del Perro";
                case "DELSOL":
                    return "Puerto Del Sol";
                case "DESRT":
                    return "Grand Senora Desert";
                case "DOWNT":
                    return "Downtown";
                case "DTVINE":
                    return "Downtown Vinewood";
                case "EAST_V":
                    return "East Vinewood";
                case "EBURO":
                    return "El Burro Heights";
                case "ELGORL":
                    return "El Gordo Lighthouse";
                case "ELYSIAN":
                    return "Elysian Island";
                case "GALFISH":
                    return "Galilee";
                case "golf":
                    return "GWC and Golfing Society";
                case "GRAPES":
                    return "Grapeseed";
                case "GREATC":
                    return "Great Chaparral";
                case "HARMO":
                    return "Harmony";
                case "HAWICK":
                    return "Hawick";
                case "HORS":
                    return "Vinewood Racetrack";
                case "HUMLAB":
                    return "Humane Labs and Research";
                case "JAIL":
                    return "Bolingbroke Penitentiary";
                case "KOREAT":
                    return "Little Seoul";
                case "LACT":
                    return "Land Act Reservoir";
                case "LAGO":
                    return "Lago Zancudo";
                case "LDAM":
                    return "Land Act Dam";
                case "LEGSQU":
                    return "Legion Square";
                case "LMESA":
                    return "La Mesa";
                case "LOSPUER":
                    return "La Puerta";
                case "MIRR":
                    return "Mirror Park";
                case "MORN":
                    return "Morningwood";
                case "MOVIE":
                    return "Richards Majestic";
                case "MTCHIL":
                    return "Mount Chiliad";
                case "MTGORDO":
                    return "Mount Gordo";
                case "MTJOSE":
                    return "Mount Josiah";
                case "MURRI":
                    return "Murrieta Heights";
                case "NCHU":
                    return "North Chumash";
                case "NOOSE":
                    return "NOOSE HQ";
                case "OCEANA":
                    return "Pacific Ocean";
                case "PALCOV":
                    return "Paleto Cove";
                case "PALETO":
                    return "Paleto Bay";
                case "PALFOR":
                    return "Paleto Forest";
                case "PALHIGH":
                    return "Palomino Highlands";
                case "PALMPOW":
                    return "Palmer-Taylor Power Station";
                case "PBLUFF":
                    return "Pacific Bluffs";
                case "PBOX":
                    return "Pillbox Hill";
                case "PROCOB":
                    return "Procopio Beach";
                case "RANCHO":
                    return "Rancho";
                case "RGLEN":
                    return "Richman Glen";
                case "RICHM":
                    return "Richman";
                case "ROCKF":
                    return "Rockford Hills";
                case "RTRAK":
                    return "Redwood Lights Track";
                case "SanAnd":
                    return "San Andreas";
                case "SANCHIA":
                    return "San Chianski Mountain Range";
                case "SANDY":
                    return "Sandy Shores";
                case "SKID":
                    return "Mission Row";
                case "SLAB":
                    return "Stab City";
                case "STAD":
                    return "Maze Bank Arena";
                case "STRAW":
                    return "Strawberry";
                case "TATAMO":
                    return "Tataviam Mountains";
                case "TERMINA":
                    return "Terminal";
                case "TEXTI":
                    return "Textile City";
                case "TONGVAH":
                    return "Tongva Hills";
                case "TONGVAV":
                    return "Tongva Valley";
                case "VCANA":
                    return "Vespucci Canals";
                case "VESP":
                    return "Vespucci";
                case "VINE":
                    return "Vinewood";
                case "WINDF":
                    return "RON Alternates Wind Farm";
                case "WVINE":
                    return "West Vinewood";
                case "ZANCUDO":
                    return "Zancudo River";
                case "ZP_ORT":
                    return "Port of South Los Santos";
                case "ZQ_UAR":
                    return "Davis Quartz";
                default:
                    return String.Empty;
            }
        }
        public static void SaveSettings()
        {

            string line = "";
            string path = Directory.GetCurrentDirectory();
            path = path + "\\Plugins\\RageShowMyLocation.ini";
            if (File.Exists(path))
            {
                Game.LogTrivial(RageShowMyLocationClass.plug_ver + " : found settings file, saving settings.");
                Game.LogTrivial(RageShowMyLocationClass.plug_ver + " : Settings File path : " + path);
                System.IO.FileStream settingsfile = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                System.IO.StreamWriter file_w = new StreamWriter(settingsfile);
                
                    
                file_w.WriteLine("// This is settings file for RageShowMyLocation Plugin");
                file_w.WriteLine("//");
                file_w.WriteLine("// Settings Explanation :");
                file_w.WriteLine("// pos_x, pos _y - screen position in pixels where text will be displayed, put here an integer, for each axis, x = 0 and y = 0 means upper left corner o screen,");
                file_w.WriteLine("// font_name - any installed in Windows system font_name,");
                file_w.WriteLine("// font_size - size of text in points,");
                file_w.WriteLine("// metric_units - set 0 to use imperial units, set 1 to use metric units,");
                file_w.WriteLine("// 12_hour_clock - set to 0 to use 24 hour clock, set to 1 to use 12 hour clock,");
                file_w.WriteLine("// display_box_around_text - set to 0 to disable, set to 1 to enable box around plugin's text,");
                file_w.WriteLine("// box_around_text_opacity - correct values are from 0 to 255. 0 means fully transparent, 255 means fully opaque. Placing incorrect value will cause plugin to use default (100),");
                file_w.WriteLine("// show_coords - set to 1 to display current (x,y,z) coordinates of player in game world, set to 0 to do not display this information,");
                file_w.WriteLine("// show_heading - set to 0 to not display heading, set to 1 to display it,");
                file_w.WriteLine("// show_player_speed - set to 0 to not display player current speed, or to 1 to display it,");
                file_w.WriteLine("// show_time - set to 0 to not display curent time in game, or to 1 to display it,");
                file_w.WriteLine("// show_zone - set to 0 to not display curent zone, or to 1 to display it (option shows raw data, a the moment)");
                file_w.WriteLine("//");
                file_w.WriteLine("//");
                file_w.WriteLine("pos_x=" + Convert.ToString(RageShowMyLocationClass.option_pos_x));
                file_w.WriteLine("pos_y=" + Convert.ToString(RageShowMyLocationClass.option_pos_y));
                file_w.WriteLine("pos_x_heading=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_heading));
                file_w.WriteLine("pos_y_heading=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_heading));
                file_w.WriteLine("pos_x_time=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_time));
                file_w.WriteLine("pos_y_time=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_time));
                file_w.WriteLine("pos_x_playerspeed=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_cs));
                file_w.WriteLine("pos_y_playerspeed=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_cs));
                file_w.WriteLine("pos_x_speedlimit=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_sl));
                file_w.WriteLine("pos_y_speedlimit=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_sl));
                file_w.WriteLine("pos_x_coords=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_coords));
                file_w.WriteLine("pos_y_coords=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_coords));
                file_w.WriteLine("font_size=" + Convert.ToString(RageShowMyLocationClass.option_font_size));
                file_w.WriteLine("font_size_heading=" + Convert.ToString(RageShowMyLocationClass.option_font_size_heading));
                file_w.WriteLine("font_size_time=" + Convert.ToString(RageShowMyLocationClass.option_font_size_time));
                file_w.WriteLine("font_size_player_speed=" + Convert.ToString(RageShowMyLocationClass.option_font_size_cs));
                file_w.WriteLine("font_size_speed_limit=" + Convert.ToString(RageShowMyLocationClass.option_font_size_sl));
                file_w.WriteLine("font_size_coords=" + Convert.ToString(RageShowMyLocationClass.option_font_size_coords));
                file_w.WriteLine("font_name=" + Convert.ToString(RageShowMyLocationClass.option_font_name));
                file_w.WriteLine("metric_units=" + Convert.ToString(RageShowMyLocationClass.option_metric));
                file_w.WriteLine("12_hour_clock=" + Convert.ToString(RageShowMyLocationClass.option_12hourclock));
                file_w.WriteLine("display_box_around_text=" + Convert.ToString(RageShowMyLocationClass.option_rect_aroud_text));
                file_w.WriteLine("box_around_text_opacity=" + Convert.ToString(RageShowMyLocationClass.option_box_opacity));
                file_w.WriteLine("show_coords=" + Convert.ToString(RageShowMyLocationClass.option_show_cords));
                file_w.WriteLine("show_heading=" + Convert.ToString(RageShowMyLocationClass.option_show_heading));
                file_w.WriteLine("show_player_speed=" + Convert.ToString(RageShowMyLocationClass.option_show_player_speed));
                file_w.WriteLine("show_time=" + Convert.ToString(RageShowMyLocationClass.option_show_time));
                file_w.WriteLine("show_zone=" + Convert.ToString(RageShowMyLocationClass.option_show_zone));
                file_w.WriteLine("//////  this should allways be 0 - its for testing purposes, setting it to other values may cause strange things to happen");
                file_w.WriteLine("do_not_touch_this=" + Convert.ToString(RageShowMyLocationClass.option_developer));

                file_w.Close();
            }


            RageShowMyLocationClass.Reload();
        }

    }
}
