﻿using RetroFun.Subscribers;
using Sulakore.Communication;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace RetroFun.Pages
{
    [ToolboxItem(true)]
    [DesignerCategory("UserControl")]
    public partial class StalkingPage : ObservablePage
    {
        private readonly Victim[] _victims = new[]
        {
            new Victim("Bot-giochi", 1442790),

        };


        private bool isUserManualWalking;

        private bool _isBotGiochiStalked;

        public bool isSpectatorModeActive
        {
            get => _isBotGiochiStalked;
            set
            {
                _isBotGiochiStalked = value;
                RaiseOnPropertyChanged();
            }
        }


        private int _UserIDCapture;

        public int UserIDCapture
        {
            get => _UserIDCapture;
            set
            {
                _UserIDCapture = value;
                RaiseOnPropertyChanged();
            }
        }


        private int _CooldownWalking = 11;

        public int CooldownWalking
        {
            get => _CooldownWalking;
            set
            {
                _CooldownWalking = value;
                RaiseOnPropertyChanged();
            }
        }


        private bool _ShouldCaptureIDMode;

        public bool ShouldCaptureIDMode
        {
            get => _ShouldCaptureIDMode;
            set
            {
                _ShouldCaptureIDMode = value;
                RaiseOnPropertyChanged();
            }
        }


        private bool _ShouldStalkBotGiochi = true;

        public bool ShouldStalkBotGiochi
        {
            get => _ShouldStalkBotGiochi;
            set
            {
                _ShouldStalkBotGiochi = value;
                RaiseOnPropertyChanged();
            }
        }
        private bool _ShouldExitDirectlyOnGames;

        public bool ShouldExitDirectlyOnGames
        {
            get => _ShouldExitDirectlyOnGames;
            set
            {
                _ShouldExitDirectlyOnGames = value;
                RaiseOnPropertyChanged();
            }
        }

        private bool _ShouldSpamRandomCoords;

        public bool ShouldSpamRandomCoords
        {
            get => _ShouldSpamRandomCoords;
            set
            {
                _ShouldSpamRandomCoords = value;
                RaiseOnPropertyChanged();
            }
        }


        private bool _MuteBotGames;

        public bool MuteBotGames
        {
            get => _MuteBotGames;
            set
            {
                _MuteBotGames = value;
                RaiseOnPropertyChanged();
            }
        }





        public StalkingPage()
        {
            InitializeComponent();

            VictimsCmbx.Items.AddRange(_victims);
            VictimsCmbx.SelectedIndex = 0;
            Bind(InterceptVictimIDChbx, "Checked", nameof(ShouldCaptureIDMode));
            Bind(Stalkbotgamescheckbx, "Checked", nameof(ShouldStalkBotGiochi));
            Bind(SpamWalkChb, "Checked", nameof(ShouldSpamRandomCoords));

            Bind(SpectatorChbx, "Checked", nameof(ShouldExitDirectlyOnGames));
            Bind(MuteBotGamesChbx, "Checked", nameof(MuteBotGames));

            Bind(IdOfVictimNbx, "Value", nameof(UserIDCapture));
            Bind(CooldownFloodNbx, "Value", nameof(CooldownWalking));
        }






        private async void StartRandomWalk()
        {
            try
            {
                CancellationTokenSource source = new CancellationTokenSource();

                for (int i = 0; i < 150; i++)
                {
                    for (int j = 0; j < 150; i++)
                    {
                        if (ShouldSpamRandomCoords)
                        {
                            if (!isUserManualWalking)
                            {
                                   await  SendToServer(Out.RoomUserWalk, i, j);
                                    await Task.Delay(CooldownWalking);
                            }
                            else
                            {
                                i = 150;
                                j = 150;
                                source.Cancel();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }




        public override void In_RoomData(DataInterceptedEventArgs e)
        {
            isUserRandomWalking();
        }

        public override void In_RoomUserStatus(DataInterceptedEventArgs e)
        {
            if (ShouldExitDirectlyOnGames)
            {
                if (isSpectatorModeActive)
                {
                    _ = SendToServer(Out.RoomUserTalk, "exit", 18);
                    isSpectatorModeActive = false;
                }
            }
        }




        private void isUserRandomWalking()
        {
            if (ShouldSpamRandomCoords)
            {
                isUserManualWalking = false;
                StartRandomWalk();
            }
        }

        public override void Out_RoomUserWalk(DataInterceptedEventArgs e)
        {
            isUserManualWalking = true;
        }

        public override void Out_RequestWearingBadges(DataInterceptedEventArgs e)
        {
            if (ShouldCaptureIDMode)
            {
                int UserID = e.Packet.ReadInteger();
                UserIDCapture = UserID;
                ShouldCaptureIDMode = false;
            }
        }

        public override void In_ReceivePrivateMessage(DataInterceptedEventArgs e)
        {

            int UserID = e.Packet.ReadInteger();
            if (UserID == 1442790)
            {
                if (ShouldStalkBotGiochi)
                {
                        _ = SendToServer(Out.StalkFriend, 1442790);
                   
                }
                if (MuteBotGames)
                {
                    e.IsBlocked = true;
                }
                if (ShouldExitDirectlyOnGames)
                {
                    isSpectatorModeActive = true;
                }

            }
        }
    
        

        private void StalkVictimBtn_Click(object sender, EventArgs e)
        {
                _ = SendToServer(Out.StalkFriend, ((Victim)VictimsCmbx.SelectedItem).ID);
            
        }

        private class Victim
        {
            public int ID { get; }
            public string Name { get; }

            public Victim(string name, int id)
            {
                ID = id;
                Name = name;
            }

            public override string ToString() => $"{Name} [#{ID}]";
        }

        private void StalkVictimID_Click(object sender, EventArgs e)
        {
                _ = SendToServer(Out.StalkFriend, UserIDCapture);
        }

    }
}