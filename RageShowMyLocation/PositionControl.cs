using Rage;
using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage.Native;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System.Windows.Forms;

namespace RageShowMyLocation
{
    public static class PositionControl
    {
        public static UIMenu mainMenu;

        public static UIMenu posMenu_street;
        public static UIMenu posMenu_heading;
        public static UIMenu posMenu_time;
        public static UIMenu posMenu_speedlimit;
        public static UIMenu posMenu_currspeed;
        public static UIMenu posMenu_coords;

        public static UIMenu font_sizeMenu;
        public static MenuPool _menuPool;

        private static NativeMenuItem set_pos_steet_county_zone;
        private static NativeMenuItem set_pos_heading;
        private static NativeMenuItem set_pos_time;
        private static NativeMenuItem set_pos_speedlimit;
        private static NativeMenuItem set_pos_currspeed;
        private static NativeMenuItem set_pos_coords;
        private static NativeMenuItem fontsizemenuCaller;
        private static MenuListItem font_size_steet_county_zone;
        private static MenuListItem font_size_heading;
        private static MenuListItem font_size_time;
        private static MenuListItem font_size_speedlimit;
        private static MenuListItem font_size_currspeed;
        private static MenuListItem font_size_coords;
        private static List<dynamic> font_sizes;


        private static NativeMenuItem set_pos_steet_county_zone_UP;
        private static NativeMenuItem set_pos_steet_county_zone_DOWN;
        private static NativeMenuItem set_pos_steet_county_zone_RIGHT;
        private static NativeMenuItem set_pos_steet_county_zone_LEFT;
        
        private static NativeMenuItem set_pos_heading_UP;
        private static NativeMenuItem set_pos_heading_DOWN;
        private static NativeMenuItem set_pos_heading_RIGHT;
        private static NativeMenuItem set_pos_heading_LEFT;
        
        private static NativeMenuItem set_pos_time_UP;
        private static NativeMenuItem set_pos_time_DOWN;
        private static NativeMenuItem set_pos_time_RIGHT;
        private static NativeMenuItem set_pos_time_LEFT;
        
        private static NativeMenuItem set_pos_speedlimit_UP;
        private static NativeMenuItem set_pos_speedlimit_DOWN;
        private static NativeMenuItem set_pos_speedlimit_RIGHT;
        private static NativeMenuItem set_pos_speedlimit_LEFT;

        private static NativeMenuItem set_pos_currspeed_UP;
        private static NativeMenuItem set_pos_currspeed_DOWN;
        private static NativeMenuItem set_pos_currspeed_RIGHT;
        private static NativeMenuItem set_pos_currspeed_LEFT;

        private static NativeMenuItem set_pos_coords_UP;
        private static NativeMenuItem set_pos_coords_DOWN;
        private static NativeMenuItem set_pos_coords_RIGHT;
        private static NativeMenuItem set_pos_coords_LEFT;
        



        public static void InitMenus()
        {
            _menuPool = new MenuPool();

            mainMenu = new UIMenu("RSML MENU", "");

            posMenu_street = new UIMenu("RSML MENU", "~b~Configure Position of street element");
            posMenu_heading = new UIMenu("RSML MENU", "~b~Configure Position of heading element");
            posMenu_time = new UIMenu("RSML MENU", "~b~Configure Position of time element");
            posMenu_currspeed = new UIMenu("RSML MENU", "~b~Configure Position of player speed element");
            posMenu_speedlimit = new UIMenu("RSML MENU", "~b~Configure Position of speed limit element");
            posMenu_coords = new UIMenu("RSML MENU", "~b~Configure Position of player coords element");


            font_sizeMenu = new UIMenu("RSML MENU", "");
            

            _menuPool.Add(mainMenu);
            _menuPool.Add(font_sizeMenu);
            _menuPool.Add(posMenu_street);
            _menuPool.Add(posMenu_time);
            _menuPool.Add(posMenu_speedlimit);
            _menuPool.Add(posMenu_heading);
            _menuPool.Add(posMenu_currspeed);
            _menuPool.Add(posMenu_coords);

            font_sizes = new List<dynamic>
            {
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                22,
                24,
                26,
                30,
                0xF00D, // Dynamic!
            }; 

            mainMenu.AddItem(set_pos_steet_county_zone = new NativeMenuItem("Set position of street/county/zone element","" ));
            mainMenu.AddItem(set_pos_heading = new NativeMenuItem("Set position of heading element", ""));
            mainMenu.AddItem(set_pos_time = new NativeMenuItem("Set position of time element", ""));
            mainMenu.AddItem(set_pos_speedlimit = new NativeMenuItem("Set position of speed limit element", ""));
            mainMenu.AddItem(set_pos_currspeed = new NativeMenuItem("Set position of current player speed element", ""));
            mainMenu.AddItem(set_pos_coords = new NativeMenuItem("Set position of player coords element", ""));
            mainMenu.AddItem(fontsizemenuCaller = new NativeMenuItem("Enter to set font size for elements"));

            
            font_sizeMenu.AddItem(font_size_steet_county_zone = new MenuListItem("Set font size of street/county/zone element", font_sizes, 0));
            font_sizeMenu.AddItem(font_size_heading = new MenuListItem("Set font size of heading element", font_sizes, 0));
            font_sizeMenu.AddItem(font_size_time = new MenuListItem("Set font size of time element", font_sizes, 0));
            font_sizeMenu.AddItem(font_size_speedlimit = new MenuListItem("Set font size of speed limit element", font_sizes, 0));
            font_sizeMenu.AddItem(font_size_currspeed = new MenuListItem("Set font size of player speed element", font_sizes, 0));
            font_sizeMenu.AddItem(font_size_coords = new MenuListItem("Set font size of player coords element", font_sizes, 0));


            posMenu_street.AddItem(set_pos_steet_county_zone_UP = new NativeMenuItem("Up", ""));
            posMenu_street.AddItem(set_pos_steet_county_zone_DOWN = new NativeMenuItem("Down", ""));
            posMenu_street.AddItem(set_pos_steet_county_zone_RIGHT = new NativeMenuItem("Right", ""));
            posMenu_street.AddItem(set_pos_steet_county_zone_LEFT = new NativeMenuItem("Left", ""));

            posMenu_heading.AddItem(set_pos_heading_UP = new NativeMenuItem("Up", ""));
            posMenu_heading.AddItem(set_pos_heading_DOWN = new NativeMenuItem("Down", ""));
            posMenu_heading.AddItem(set_pos_heading_RIGHT = new NativeMenuItem("Right", ""));
            posMenu_heading.AddItem(set_pos_heading_LEFT = new NativeMenuItem("Left", ""));

            posMenu_time.AddItem(set_pos_time_UP = new NativeMenuItem("Up", ""));
            posMenu_time.AddItem(set_pos_time_DOWN = new NativeMenuItem("Down", ""));
            posMenu_time.AddItem(set_pos_time_RIGHT = new NativeMenuItem("Right", ""));
            posMenu_time.AddItem(set_pos_time_LEFT = new NativeMenuItem("Left", ""));

            posMenu_speedlimit.AddItem(set_pos_speedlimit_UP = new NativeMenuItem("Up", ""));
            posMenu_speedlimit.AddItem(set_pos_speedlimit_DOWN = new NativeMenuItem("Down", ""));
            posMenu_speedlimit.AddItem(set_pos_speedlimit_RIGHT = new NativeMenuItem("Right", ""));
            posMenu_speedlimit.AddItem(set_pos_speedlimit_LEFT = new NativeMenuItem("Left", ""));

            posMenu_currspeed.AddItem(set_pos_currspeed_UP = new NativeMenuItem("Up", ""));
            posMenu_currspeed.AddItem(set_pos_currspeed_DOWN = new NativeMenuItem("Down", ""));
            posMenu_currspeed.AddItem(set_pos_currspeed_RIGHT = new NativeMenuItem("Right", ""));
            posMenu_currspeed.AddItem(set_pos_currspeed_LEFT = new NativeMenuItem("Left", ""));

            posMenu_coords.AddItem(set_pos_coords_UP = new NativeMenuItem("Up", ""));
            posMenu_coords.AddItem(set_pos_coords_DOWN = new NativeMenuItem("Down", ""));
            posMenu_coords.AddItem(set_pos_coords_RIGHT = new NativeMenuItem("Right", ""));
            posMenu_coords.AddItem(set_pos_coords_LEFT = new NativeMenuItem("Left", ""));


            mainMenu.RefreshIndex();
            font_sizeMenu.RefreshIndex();
            posMenu_street.RefreshIndex();
            posMenu_time.RefreshIndex();
            posMenu_speedlimit.RefreshIndex();
            posMenu_heading.RefreshIndex();
            posMenu_currspeed.RefreshIndex();
            posMenu_coords.RefreshIndex();

            mainMenu.BindMenuToItem(font_sizeMenu, fontsizemenuCaller);
            mainMenu.BindMenuToItem(posMenu_street, set_pos_steet_county_zone);
            mainMenu.BindMenuToItem(posMenu_heading, set_pos_heading);
            mainMenu.BindMenuToItem(posMenu_time, set_pos_time);
            mainMenu.BindMenuToItem(posMenu_speedlimit, set_pos_speedlimit);
            mainMenu.BindMenuToItem(posMenu_currspeed, set_pos_currspeed);
            mainMenu.BindMenuToItem(posMenu_coords, set_pos_coords);
            

            mainMenu.OnItemSelect += OnItemSelect;

            posMenu_street.OnItemSelect += OnItemSelect_posMenu_street;
            posMenu_heading.OnItemSelect += OnItemSelect_posMenu_heading;
            posMenu_time.OnItemSelect += OnItemSelect_posMenu_time;
            posMenu_speedlimit.OnItemSelect += OnItemSelect_posMenu_speedlimit;
            posMenu_currspeed.OnItemSelect += OnItemSelect_posMenu_currspeed;
            posMenu_coords.OnItemSelect += OnItemSelect_posMenu_coords;

            font_sizeMenu.OnListChange += OnListChange_font_size;
        }

        private static void OnItemSelect(UIMenu sender, NativeMenuItem selectedItem, int index)
        {
            if (sender != mainMenu) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index

                        
        }

        private static void OnItemSelect_posMenu_street(UIMenu sender, NativeMenuItem selectedItem, int index)
        {
            if (sender != posMenu_street) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index

            if (selectedItem == set_pos_steet_county_zone_UP)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y -= 5;
            }
            if (selectedItem == set_pos_steet_county_zone_DOWN)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y += 5;
            }
            if (selectedItem == set_pos_steet_county_zone_RIGHT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x += 5;
            }
            if (selectedItem == set_pos_steet_county_zone_LEFT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x -= 5;
            }

            SaveSettings();

        }

        private static void OnItemSelect_posMenu_heading(UIMenu sender, NativeMenuItem selectedItem, int index)
        {
            if (sender != posMenu_heading) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index

            if (selectedItem == set_pos_heading_UP)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_heading -= 5;
            }
            if (selectedItem == set_pos_heading_DOWN)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_heading += 5;
            }
            if (selectedItem == set_pos_heading_RIGHT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_heading += 5;
            }
            if (selectedItem == set_pos_heading_LEFT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_heading -= 5;
            }
            SaveSettings();
        }

        private static void OnItemSelect_posMenu_time(UIMenu sender, NativeMenuItem selectedItem, int index)
        {
            if (sender != posMenu_time) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index

            if (selectedItem == set_pos_time_UP)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_time -= 5;
            }
            if (selectedItem == set_pos_time_DOWN)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_time += 5;
            }
            if (selectedItem == set_pos_time_RIGHT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_time += 5;
            }
            if (selectedItem == set_pos_time_LEFT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_time -= 5;
            }
            SaveSettings();
        }

        private static void OnItemSelect_posMenu_speedlimit(UIMenu sender, NativeMenuItem selectedItem, int index)
        {
            if (sender != posMenu_speedlimit) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index

            if (selectedItem == set_pos_speedlimit_UP)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_sl -= 5;
            }
            if (selectedItem == set_pos_speedlimit_DOWN)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_sl += 5;
            }
            if (selectedItem == set_pos_speedlimit_RIGHT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_sl += 5;
            }
            if (selectedItem == set_pos_speedlimit_LEFT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_sl -= 5;
            }
            SaveSettings();
        }

        private static void OnItemSelect_posMenu_currspeed(UIMenu sender, NativeMenuItem selectedItem, int index)
        {
            if (sender != posMenu_currspeed) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index

            if (selectedItem == set_pos_currspeed_UP)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_cs -= 5;
            }
            if (selectedItem == set_pos_currspeed_DOWN)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_cs += 5;
            }
            if (selectedItem == set_pos_currspeed_RIGHT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_cs += 5;
            }
            if (selectedItem == set_pos_currspeed_LEFT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_cs -= 5;
            }
            SaveSettings();
        }

        private static void OnItemSelect_posMenu_coords(UIMenu sender, NativeMenuItem selectedItem, int index)
        {
            if (sender != posMenu_coords) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index

            if (selectedItem == set_pos_coords_UP)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_coords -= 5;
            }
            if (selectedItem == set_pos_coords_DOWN)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_y_coords += 5;
            }
            if (selectedItem == set_pos_coords_RIGHT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_coords += 5;
            }
            if (selectedItem == set_pos_coords_LEFT)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_pos_x_coords -= 5;
            }
            SaveSettings();
        }

        private static void OnListChange_font_size(UIMenu sender, MenuListItem list, int index)
        {
            if (sender != font_sizeMenu) return;

            if (list == font_size_steet_county_zone)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_font_size = list.IndexToItem(index).ToInt32();
            }
            if (list == font_size_heading)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_font_size_heading = list.IndexToItem(index).ToInt32();
            }
            if (list == font_size_time)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_font_size_time = list.IndexToItem(index).ToInt32();
            }
            if (list == font_size_speedlimit)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_font_size_sl = list.IndexToItem(index).ToInt32();
            } 
            if (list == font_size_currspeed)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_font_size_cs = list.IndexToItem(index).ToInt32();
            }
            if (list == font_size_coords)
            {
                RageShowMyLocation.RageShowMyLocationClass.option_font_size_coords = list.IndexToItem(index).ToInt32();
            }


            SaveSettings();
        }

        private static void SaveSettings()
        {

            string line = "";
            string path = Directory.GetCurrentDirectory();
            path = path + "\\Plugins\\RageShowMyLocation.ini";
            if (File.Exists(path))
            {
                Game.LogTrivial(RageShowMyLocationClass.plug_ver + " : found settings file, adjusting settings.");
                Game.LogTrivial(RageShowMyLocationClass.plug_ver + " : Settings File path : " + path);
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                System.IO.StreamWriter file_w = new StreamWriter(path);
                int index_start = 0;
                int index_stop = 0;
                char[] usun_zn = { ';', ',', '.', '#', '/', '\\', ' ' };
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Trim();
                    line = line.Trim(usun_zn);
                    if (line.Contains("pos_x="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length,SeekOrigin.Begin);
                        file_w.Write("pos_x=" + Convert.ToString(RageShowMyLocationClass.option_pos_x));
                    }
                    if (line.Contains("pos_y="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_y=" + Convert.ToString(RageShowMyLocationClass.option_pos_y));
                    }

                    if (line.Contains("pos_x_heading="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_x_heading=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_heading));
                    }
                    if (line.Contains("pos_y_heading="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_y_heading=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_heading));
                    }

                    if (line.Contains("pos_x_time="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_x_time=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_time));
                    }
                    if (line.Contains("pos_y_time="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_y_time=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_time));
                    }

                    if (line.Contains("pos_x_playerspeed="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_x_playerspeed=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_cs));
                    }
                    if (line.Contains("pos_y_playerspeed="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_y_playerspeed=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_cs));
                    }

                    if (line.Contains("pos_x_speedlimit="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_x_speedlimit=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_sl));
                    }
                    if (line.Contains("pos_y_speedlimit="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_y_speedlimit=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_sl));
                    }

                    if (line.Contains("pos_x_coords="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_x_coords=" + Convert.ToString(RageShowMyLocationClass.option_pos_x_coords));
                    }
                    if (line.Contains("pos_y_coords="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("pos_y_coords=" + Convert.ToString(RageShowMyLocationClass.option_pos_y_coords));
                    }

                   if (line.Contains("font_size="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("font_size=" + Convert.ToString(RageShowMyLocationClass.option_font_size));
                    }

                    if (line.Contains("font_size_heading="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("font_size_heading=" + Convert.ToString(RageShowMyLocationClass.option_font_size_heading));
                    }
                    if (line.Contains("font_size_time="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("font_size_time=" + Convert.ToString(RageShowMyLocationClass.option_font_size_time));
                    }
                    if (line.Contains("font_size_player_speed="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("font_size_player_speed=" + Convert.ToString(RageShowMyLocationClass.option_font_size_cs));
                    }
                    if (line.Contains("font_size_speed_limit="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("font_size_speed_limit=" + Convert.ToString(RageShowMyLocationClass.option_font_size_sl));
                    }
                    if (line.Contains("font_size_coords="))
                    {
                        index_start = line.IndexOf('=');
                        index_stop = line.Length - line.IndexOf('=');
                        file_w.BaseStream.Seek(file.BaseStream.Position - line.Length, SeekOrigin.Begin);
                        file_w.Write("font_size_coords=" + Convert.ToString(RageShowMyLocationClass.option_font_size_coords));
                    }


                    

                }

                file.Close();
            }


            RageShowMyLocationClass.Reload();
        }

        public static void Process(System.Object obj, GraphicsEventArgs eva)
        {
            if (Game.IsKeyDown(Keys.F5) && !_menuPool.IsAnyMenuOpen()) // Our menu on/off switch.
                mainMenu.Visible = !mainMenu.Visible;

            _menuPool.ProcessMenus();       // Procces all our menus: draw the menu and procces the key strokes and the mouse.  
        }



    }// class
} // namespace
