using Rage;
using System;
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
        public static uint notif_handle = 0;
        static string street_bckp = "";
        static bool option_subtitle = false;
        static bool option_notify = false;
        static Rage.Graphics graf;
       
        public static void Main()
        {
                      
            Game.FrameRender += new EventHandler<GraphicsEventArgs>((obj,graf_ev) => DisplayPos(obj,graf_ev));
            while (true)
            {
              
                //DisplayPos();
                GameFiber.Yield();
            }   
            
            

                
        }


        public static void DisplayPos(System.Object obj, GraphicsEventArgs eva)
        {
           
            Ped ped = Game.LocalPlayer.Character;
            pl_pos = ped.Position;
            String street = Rage.World.GetStreetName(pl_pos);
            Update_pos_onScreen(Loc, eva);
            Point pt = new Point(500, 0);
            Color white = Color.FromName("White");
            eva.Graphics.DrawText(street, "Arial", 21, pt, white);

            
        }
               
        public static void Update_pos_onScreen(String street, GraphicsEventArgs g_ev)
        {
            Point pt = new Point(500,0);
            Color white = Color.FromName("White");

            //if (option_notify)
            //{
            //    if (street_bckp != street && street_bckp != "")
            //    {

            //        Game.RemoveNotification(notif_handle);

           //     }
           //     notif_handle = Game.DisplayNotification(street);

            //    street_bckp = street;
            //}
            //else if (option_subtitle)
            //{
             //   Game.DisplaySubtitle(street, 1000);
            //}
            //else
            //{
                //do nothing
            //}
            


                
            
                
        }
       

    }
}
