﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheHabourProject
{
    public class WharfPlace
    {
        public int Number { get; set; }
        public bool HalfPlaceFree { get; set; }
        public bool WharfPlaceFree { get; set; }
        public int DaysLeftForBoatInWharf { get; set; }


        public WharfPlace(int number)
        {
            Number = number;
            HalfPlaceFree = true;
            WharfPlaceFree = true;
            DaysLeftForBoatInWharf = 0;
        }
        public WharfPlace(string[] info)
        {
            Number = int.Parse(info[0]);
            HalfPlaceFree = bool.Parse(info[1]);
            WharfPlaceFree = bool.Parse(info[2]);
            DaysLeftForBoatInWharf = int.Parse(info[3]);
        }
    }
}
