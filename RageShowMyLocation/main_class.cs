﻿using Rage;
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
        public static void Main()
        {
            GameFiber.StartNew(delegate
            {
                for (; ; )
                {
                    foreach (Rage.Ped ped in Rage.World.GetAllPeds())
                    {
                        if (ped.IsPlayer)
                        {
                            pl_pos = ped.Position;
                            Loc = GetLocation(pl_pos);
                            break;
                        }
                    }
                    Update_pos_onScreen(Loc);
                }
            });
        }

        public static String GetLocation(Rage.Vector3 pos)
        {
            String street = Rage.World.GetStreetName(pos);
            return street;
        }
        public static void Update_pos_onScreen(String street)
        {
            Point pt = new Point(200,20);
            Color white = Color.FromName("White");
            Game.DisplayNotification(street);
        }
       

    }
}
