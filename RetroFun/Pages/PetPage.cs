﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RetroFun.Subscribers;
using Sulakore.Communication;
using Sulakore.Components;

namespace RetroFun.Pages
{
    [ToolboxItem(true)]
    [DesignerCategory("UserControl")]
    public partial class PetPage:  ObservablePage
    {


        public PetPage()
        {
            InitializeComponent();

            Bind(PageIDNbx, "Value", nameof(PageID));
            Bind(PetRaceNbx, "Value", nameof(PetRace));
            Bind(PetNameTbx, "Text", nameof(PetName));
            Bind(PetColorHTMLTbx, "Text", nameof(PetHTMLColor));
            Bind(PetIdNbx, "Value", nameof(PetID));


        }


        public bool isInterceptEnabled;



        private int _PageID;

        public int PageID
        {
            get => _PageID;
            set
            {
                _PageID = value;
                RaiseOnPropertyChanged();
            }
        }


        private int _PetID;

        public int PetID
        {
            get => _PetID;
            set
            {
                _PetID = value;
                RaiseOnPropertyChanged();
            }
        }


        private string _PetName;

        public string PetName
        {
            get => _PetName;
            set
            {
                _PetName = value;
                RaiseOnPropertyChanged();
            }
        }

        private int _PetRace;

        public int PetRace
        {
            get => _PetRace;
            set
            {
                _PetRace = value;
                RaiseOnPropertyChanged();
            }
        }


        private string _PetHTMLColor;

        public string PetHTMLColor
        {
            get => _PetHTMLColor;
            set
            {
                _PetHTMLColor = value;
                RaiseOnPropertyChanged();
            }
        }


        public override void Out_CatalogBuyItem(DataInterceptedEventArgs e)
        {
            int PetRaceType;
            bool HasParsed;
            if(isInterceptEnabled)
            {
                PageID = e.Packet.ReadInteger();
                PetID = e.Packet.ReadInteger();
                string petData = e.Packet.ReadString();
                string[] splitted = petData.Split('\n');
                if (splitted.Length < 2)
                    throw new Exception("");
                PetName = splitted[0];
                HasParsed = int.TryParse(splitted[1], out PetRaceType);
                if (HasParsed)
                {
                    PetRace = PetRaceType;
                }
                else
                {
                    PetRace = 0;
                }
                PetHTMLColor = splitted[2];
                e.IsBlocked = true;
                _ = SendToClient(In.RoomUserWhisper, 0, "[Pet Editor]: Values intercepted, see Retrofun.", 0, 34, 0, -1);
                WriteToButton(InterceptPetPurchaseBtn, "Intercept Pet purchase : OFF");
                isInterceptEnabled = false;
            }

        }

        public async void SendPetPurchase()
        {
          await  SendToServer(Out.CatalogBuyItem, PageID, PetID, PetName + '\n' +PetRace.ToString() + '\n' +PetHTMLColor , 1);
        }



        private void InterceptPetPurchaseBtn_Click(object sender, EventArgs e)
        {
            if(isInterceptEnabled)
            {
                WriteToButton(InterceptPetPurchaseBtn, "Intercept Pet purchase : OFF");
                isInterceptEnabled = false;
            }
            else
            {
                WriteToButton(InterceptPetPurchaseBtn, "Intercept Pet purchase : ON");
                _ = SendToClient(In.RoomUserWhisper, 0, "[Pet Editor]: Please purchase The Pet to load the values to edit with Retrofun", 0, 34, 0, -1);
                isInterceptEnabled = true;
            }
        }

        private void BuyPetBtn_Click(object sender, EventArgs e)
        {
            SendPetPurchase();
        }


        private void WriteToButton(SKoreButton Button, string text)
        {
            Invoke((MethodInvoker)delegate
            {
                Button.Text = text;
            });


        }
        public string ToHexString(Color c) => $"{c.R:X2}{c.G:X2}{c.B:X2}";


        private void ChoosePetColorBtn_Click(object sender, EventArgs e)
        {
            if(PetColor.ShowDialog()==DialogResult.OK)
            {
                PetHTMLColor = ToHexString(PetColor.Color);
            }
        }

    }
}
