﻿using Sulakore.Habbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RetroFun.Utils.HostFinder.BobbaItalia
{
    class StaffRaresWallItems
    {
        public static readonly List<int> Bobba_RariMaggioHabbo = new List<int> { 16136, 16135, 16134, 16133, 16132, 16138, 16137 };
        public static readonly List<int> Bobba_RariLPTPOK = new List<int> { 4331 };
        public static readonly List<int> Bobba_RariSTAFF = new List<int> { 4058 };
        public static readonly List<int> Bobba_LimitedScaduti = new List<int> { 25724, 25596 };
        public static readonly List<int> Bobba_QuadriLimited = new List<int> { 25720, 25664, 25663, 25662, 25661, 25660, 25659, 25658, 25657, 25656, 25655, 25654, 25719, 25718, 25727, 25726, 25725, 25724, 25723, 25722, 25597, 25596, 25595, 25594, 25593, 25592, 25711, 25710, 25709, 25708, 25707, 25706, 25705, 25704, 25703, 25702, 25701, 25700, 25721 };
        public static readonly List<int> Bobba_TestBug = new List<int> { 25499, 25202, 25145, 25146, 25142, 25140, 25143, 25141, 25571, 25139, 25588, 25628, 25572, 25570 };
        public static readonly List<int> Bobba_FurniCalippo = new List<int> { 4020 };
        public static readonly List<int> Bobba_Promozioni = new List<int> { 25402, 25389, 25498, 25574, 4030, 4249, 4247, 4243, 4240, 4239, 4122, 4114, 4113, 4112, 25454, 4108, 4107, 4106, 4103, 4101, 4100, 4099, 4098, 4096, 4090, 4089, 4092, 4091, 4088, 25383, 4083, 4082, 4081, 4080, 4079, 4066, 4053, 4052, 4049, 4045, 4330, 25430, 4256, 4325, 4327, 4328, 25418, 25384, 4258, 4338, 25382, 4347, 25493, 4344, 25404, 25391, 4345, 4341, 4425, 4018, 4019, 4051, 25449, 4259, 25403, 25575, 25401 };
        public static readonly List<int> Bobba_Rari = new List<int> { 4260, 4017, 4055, 2, 4021 };
        public static readonly List<int> Bobba_System = new List<int> { 4271, 4105, 4104, 4102 };
        public static readonly List<int> Bobba_Utility = new List<int> { 4339, 4111, 4397 };



        public static bool isRareFurni(HWallItem item)
        {
            if (Bobba_RariMaggioHabbo.Contains(item.TypeId)) { return true; }
            else if (Bobba_RariLPTPOK.Contains(item.TypeId)) { return true; }
            else if (Bobba_RariSTAFF.Contains(item.TypeId)) { return true; }
            else if (Bobba_LimitedScaduti.Contains(item.TypeId)) { return true; }
            else if (Bobba_QuadriLimited.Contains(item.TypeId)) { return true; }
            else if (Bobba_TestBug.Contains(item.TypeId)) { return true; }
            else if (Bobba_FurniCalippo.Contains(item.TypeId)) { return true; }
            else if (Bobba_Promozioni.Contains(item.TypeId)) { return true; }
            else if (Bobba_Rari.Contains(item.TypeId)) { return true; }
            else if (Bobba_System.Contains(item.TypeId)) { return true; }
            else if (Bobba_Utility.Contains(item.TypeId)) { return true; }
            else { return false; }
        }

    }





}
