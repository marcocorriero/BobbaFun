﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sulakore.Protocol;
using Sulakore.Communication;
using Sulakore.Components;
using System.Threading;
using RetroFun.Subscribers;
using Sulakore.Habbo;
using RetroFun.Helpers;
using System.Runtime.CompilerServices;
using RetroFun.Globals;

namespace RetroFun.Pages
{
    [ToolboxItem(true)]
    [DesignerCategory("UserControl")]
    public partial class PersonalPage : ObservablePage
    {
        HMessage Permissions;

        private bool HasUserPermissionsMessage;
        private bool _HasModToolsUnlocked;
        private bool IsInterceptTradeUserOn;
        private bool TradeSpammerActivated;
        private bool hasReceivedBanListResponse;
        private int _selectedUserId;
        private readonly Handitems[] _Handitems = new[]
        {
            #region handitemlist
           new Handitems(1, "Tè Verde"),
           new Handitems(2, "Succo di Frutta"),
           new Handitems(3, "Carota"),
           new Handitems(4, "Gelato alla Vaniglia"),
           new Handitems(5, "Latte"),
           new Handitems(6, "Spremuta di Arancia"),
           new Handitems(7, "Acqua"),
           new Handitems(8, "Caffè"),
           new Handitems(9, "Decaffeinato"),
           new Handitems(10, "Tè"),
           new Handitems(11, "Cioccolata"),
           new Handitems(12, "Caffè Macchiato"),
           new Handitems(13, "Espresso"),
           new Handitems(14, "Cola"),
           new Handitems(15, "Cioccolata Calda"),
           new Handitems(16, "Gassosa"),
           new Handitems(17, "Chinotto"),
           new Handitems(18, "Bicchiere d'acqua"),
           new Handitems(19, "Acqua Gassata"),
           new Handitems(20, "Banana Soda"),
           new Handitems(21, "Hamburger"),
           new Handitems(22, "Succo di Lime"),
           new Handitems(23, "Infuso di Radice"),
           new Handitems(24, "Deliziose Bollicine"),
           new Handitems(25, "Pozione d'Amore"),
           new Handitems(26, "Ghiacciolo"),
           new Handitems(27, "Té"),
           new Handitems(28, "Sake"),
           new Handitems(29, "Succo di pomodoro"),
           new Handitems(30, "Liquido radioattivo"),
           new Handitems(31, "FrizzLime"),
           new Handitems(34, "Pesce"),
           new Handitems(35, "FrizzLime"),
           new Handitems(36, "Pera"),
           new Handitems(37, "Pesca deliziosa"),
           new Handitems(38, "Arancia"),
           new Handitems(39, "Fetta di Formaggio"),
           new Handitems(40, "Succo d'arancia"),
           new Handitems(41, "Sumppi-kuppi"),
           new Handitems(42, "Succo d'arancia"),
           new Handitems(43, "Limonata"),
           new Handitems(44, "Drink da Fine del Mondo!"),
           new Handitems(45, "Sfera misteriosa gialla"),
           new Handitems(46, "Sfera misteriosa verde"),
           new Handitems(47, "Sfera misteriosa rossa"),
           new Handitems(48, "Lecca-lecca"),
           new Handitems(49, "Estathè"),
           new Handitems(50, "Bottiglia di Succo Spumeggiante"),
           new Handitems(51, "patatine"),
           new Handitems(52, "Cheetos"),
           new Handitems(53, "Caffè Espresso"),
           new Handitems(54, "Nesquik"),
           new Handitems(55, "Pepsi"),
           new Handitems(56, "Cheetos Piccanti"),
           new Handitems(57, "Bibita alla Ciliegia"),
           new Handitems(58, "Tazza di Sangue"),
           new Handitems(59, "Sacchetto"),
           new Handitems(60, "Castagne"),
           new Handitems(61, "Fanta"),
           new Handitems(62, "Acqua Avvelenata"),
           new Handitems(63, "Pop Corn"),
           new Handitems(64, "Cedrata"),
           new Handitems(66, "Banana Drink"),
           new Handitems(67, "Skittle blu"),
           new Handitems(68, "Skittle rossa"),
           new Handitems(69, "Skittle verde"),
           new Handitems(70, "Coscia di pollo"),
           new Handitems(71, "Toast"),
           new Handitems(72, "Succo di mirtillo"),
           new Handitems(73, "Zabaione"),
           new Handitems(74, "Calice"),
           new Handitems(75, "Gelato alla fragola"),
           new Handitems(76, "Gelato alla menta"),
           new Handitems(77, "Gelato al Cioccolato"),
           new Handitems(79, "Zucchero Filato Rosa"),
           new Handitems(80, "Zucchero Filato Azzurro"),
           new Handitems(81, "Hot Dog"),
           new Handitems(82, "Cannocchiale"),
           new Handitems(83, "Mela succosa"),
           new Handitems(84, "Omino di Pan di Zenzero"),
           new Handitems(85, "Americano"),
           new Handitems(86, "Frappuccino"),
           new Handitems(87, "Acqua frizzantina"),
           new Handitems(88, "Gin"),
           new Handitems(89, "Cupcake"),
           new Handitems(90, "Rosè"),
           new Handitems(91, "Tazzina blu"),
           new Handitems(92, "Gomma Blu"),
           new Handitems(93, "Gomma Rossa"),
           new Handitems(94, "Gomma Pink"),
           new Handitems(95, "Lecca Lecca verde"),
           new Handitems(96, "Fetta di Torta"),
           new Handitems(97, "Croissant"),
           new Handitems(98, "Pomodoro"),
           new Handitems(99, "Melanzana"),
           new Handitems(100, "Cavolo"),
           new Handitems(101, "Bottiglia di Succo Frizzante"),
           new Handitems(102, "Energy Drink"),
           new Handitems(103, "Banana"),
           new Handitems(104, "Avocado"),
           new Handitems(105, "Uva"),
           new Handitems(106, "Frullato"),
           new Handitems(107, "Succo di verdura"),
           new Handitems(108, "Pesi"),
           new Handitems(109, "Burger"),
           new Handitems(110, "Lettera"),
           new Handitems(111, "granchio"),
           new Handitems(112, "Peperoncino Rosso"),
           new Handitems(113, "Frullato di agrumi"),
           new Handitems(114, "Frullato verde"),
           new Handitems(115, "Frullato di Frutti di bosco"),
           new Handitems(116, "Limone"),
           new Handitems(117, "Cookie"),
           new Handitems(118, "Ramune Rosa"),
           new Handitems(119, "Ramune Blu"),
           new Handitems(120, "Granita di Mirtilli"),
           new Handitems(121, "Granita di Fragole"),
           new Handitems(122, "Takoyaki"),
           new Handitems(123, "Ramen"),
           new Handitems(124, "Bubble tea Viola"),
           new Handitems(125, "Bubble tea Verde"),
           new Handitems(126, "Bubble tea Rosa"),
           new Handitems(127, "Cono Gelato"),
           new Handitems(128, "Gelato al Carbone"),
           new Handitems(129, "Yogurt"),
           new Handitems(130, "Formaggio"),
           new Handitems(131, "Pane"),
           new Handitems(132, "Gamberetto"),
           new Handitems(133, "Broccoli"),
           new Handitems(134, "Anguria"),
           new Handitems(135, "Donut"),
           new Handitems(136, "Salsiccia"),
           new Handitems(137, "Ghiacciolo"),
           new Handitems(138, "Patatine (aperte)"),
           new Handitems(142, "Bevanda Ghiacciata"),
           new Handitems(143, "Spiedino di Marshmallow Ricoperto di Cioccolato"),
           new Handitems(144, "Spiedino di Fragola Ricoperta di Cioccolato"),
           new Handitems(1000, "Rosa"),
           new Handitems(1001, "Rosa Nera"),
           new Handitems(1002, "Girasole"),
           new Handitems(1003, "Libro Rosso"),
           new Handitems(1004, "Libro Blu"),
           new Handitems(1005, "Libro Verde"),
           new Handitems(1006, "Fiore Giallo"),
           new Handitems(1007, "Margherita Azzurra"),
           new Handitems(1008, "Margherita Gialla"),
           new Handitems(1009, "Margherita Rosa"),
           new Handitems(1011, "Cartellina"),
           new Handitems(1013, "Pillole"),
           new Handitems(1014, "Siringa"),
           new Handitems(1015, "Rifiuti Tossici"),
           new Handitems(1019, "Fiore Bolly"),
           new Handitems(1021, "Giacinto Rosa"),
           new Handitems(1022, "Giacinto Azzurro"),
           new Handitems(1023, "Stella di Natale"),
           new Handitems(1024, "Budino"),
           new Handitems(1025, "Caramella"),
           new Handitems(1026, "Regalo"),
           new Handitems(1027, "Candela"),
           new Handitems(1028, "Cereali"),
           new Handitems(1029, "Palloncino"),
           new Handitems(1030, "HiPad"),
           new Handitems(1031, "Torcia Habbo-lympix"),
           new Handitems(1032, "Sindaco Tom"),
           new Handitems(1033, "UFO"),
           new Handitems(1034, "Oggetto alieno"),
           new Handitems(1035, "Chiave inglese"),
           new Handitems(1036, "Papera di gomma"),
           new Handitems(1037, "Serpente"),
           new Handitems(1038, "Bastone"),
           new Handitems(1039, "Mano Mozzata"),
           new Handitems(1040, "Cuore"),
           new Handitems(1041, "Calamaro"),
           new Handitems(1042, "Pupù di Pipistrello"),
           new Handitems(1043, "Verme"),
           new Handitems(1044, "Ratto Morto"),
           new Handitems(1045, "Dentiera"),
           new Handitems(1046, "Scatola Durex"),
           new Handitems(1047, "Palla di cannone"),
           new Handitems(1048, "Bandiera nera"),
           new Handitems(1049, "Martello"),
           new Handitems(1050, "Uovo Pasquale"),
           new Handitems(1051, "Pennello"),
           new Handitems(1052, "Bandiera bianca"),
           new Handitems(1053, "Anatra"),
           new Handitems(1054, "Palloncino Arancione"),
           new Handitems(1055, "Palloncino Verde"),
           new Handitems(1056, "Palloncino Blu"),
           new Handitems(1057, "Palloncino Rosa"),
           new Handitems(1058, "Lanterna Antica"),
           new Handitems(1059, "Carta igienica"),
           new Handitems(1060, "Spray"),
           new Handitems(1061, "Margherita gialla"),
           new Handitems(1062, "Caramella Teschio Rosa"),
           new Handitems(1063, "Caramella Teschio Verde"),
           new Handitems(1064, "Caramella Teschio Blu"),
           new Handitems(1065, "Bambola Giocattolo"),
           new Handitems(1066, "Orsetto Giocattolo"),
           new Handitems(1067, "Soldato Giocattolo"),
           new Handitems(1068, "Manga"),
           new Handitems(1069, "Fumetto"),
           new Handitems(1070, "Biglietto Giallo"),
           new Handitems(1071, "HiPad Gold"),
           new Handitems(1072, "Bussola"),
           new Handitems(1073, "Uovo di Dinosauro"),
           new Handitems(1074, "Allosaurus Verde"),
           new Handitems(1075, "Triceratopi Gialli"),
           new Handitems(1076, "Saurolophus Viola"),
           new Handitems(1077, "Asciugamano"),
           new Handitems(1078, "Lucertola"),
           new Handitems(1079, "Cervo Volante"),
           new Handitems(1080, "Scarabeo Rinoceronte"),
           new Handitems(1081, "Annaffiatoio"),
           new Handitems(1082, "Bandiera Pride"),
           new Handitems(1083, "Zucca Spaventosa"),
           new Handitems(1084, "Borsa della spesa"),
           new Handitems(1085, "DVD d'Azione"),
           new Handitems(1086, "DVD Thriller"),
           new Handitems(1087, "Quaderno"),
           new Handitems(1088, "Matita"),
           new Handitems(1089, "Patatine (chiuse)"),
           new Handitems(1090, "Canna da Pesca - Pesce catturato"),
           new Handitems(1091, "Canna da Pesca - Stivale catturato"),
           new Handitems(1092, "Canna da Pesca - Messaggio in Bottiglia catturato"),
           #endregion
        };

        #region crap

        private string _badgecode;
        public string badgecode
        {
            get => _badgecode;
            set
            {
                _badgecode = value;
                RaiseOnPropertyChanged();
            }
        }


        private bool _giveHanditemToselecteduser;
        public bool giveHanditemToselecteduser
        {
            get => _giveHanditemToselecteduser;
            set
            {
                _giveHanditemToselecteduser = value;
                RaiseOnPropertyChanged();
            }


        }
        public bool HasModToolsUnlocked
        {
            get => _HasModToolsUnlocked;
            set
            {
                _HasModToolsUnlocked = value;
                RaiseOnPropertyChanged();
            }
        }

        private bool _HasStaffPermissions;

        public bool HasStaffPermissions
        {
            get => _HasStaffPermissions;
            set
            {
                _HasStaffPermissions = value;
                RaiseOnPropertyChanged();
            }
        }

        private bool _CreditsChecked = true;

        private bool _CrystalsChecked = true;
        public bool CrystalsChecked
        {
            get => _CrystalsChecked;
            set
            {
                _CrystalsChecked = value;
                RaiseOnPropertyChanged();
            }
        }

        public bool CreditsChecked
        {
            get => _CreditsChecked;
            set
            {
                _CreditsChecked = value;
                RaiseOnPropertyChanged();
            }
        }


        private bool _DucketsChecked = true;

        public bool DucketsChecked
        {
            get => _DucketsChecked;
            set
            {
                _DucketsChecked = value;
                RaiseOnPropertyChanged();
            }
        }


        private int _CreditsValue = int.MaxValue;

        private int _CrystalsValue = int.MaxValue;
        public int CrystalsValue
        {
            get => _CrystalsValue;
            set
            {
                _CrystalsValue = value;
                RaiseOnPropertyChanged();
            }
        }



        private int _HanditemHunter;
        public int HanditemHunter
        {
            get => _HanditemHunter;
            set
            {
                _HanditemHunter = value;
                RaiseOnPropertyChanged();
            }
        }

        private int _TradeSpammerUserID;
        public int TradeSpammerUserID
        {
            get => _TradeSpammerUserID;
            set
            {
                _TradeSpammerUserID = value;
                RaiseOnPropertyChanged();
            }
        }

        private int _TradeSpammerCooldown;
        public int TradeSpammerCooldown
        {
            get => _TradeSpammerCooldown;
            set
            {
                _TradeSpammerCooldown = value;
                RaiseOnPropertyChanged();
            }
        }

        private int _HanditemID;
        public int HanditemID
        {
            get => _HanditemID;
            set
            {
                _HanditemID = value;
                RaiseOnPropertyChanged();
            }
        }

        public int CreditsValue
        {
            get => _CreditsValue;
            set
            {
                _CreditsValue = value;
                RaiseOnPropertyChanged();
            }
        }


        private int _DucketsValue = int.MaxValue;

        public int DucketsValue
        {
            get => _DucketsValue;
            set
            {
                _DucketsValue = value;
                RaiseOnPropertyChanged();
            }
        }


        private bool _BlockBypassers;

        public bool BlockBypassers
        {
            get => _BlockBypassers;
            set
            {
                _BlockBypassers = value;
                RaiseOnPropertyChanged();
            }
        }

        private bool _AutomaticAttempt;

        public bool AutomaticAttempt
        {
            get => _AutomaticAttempt;
            set
            {
                _AutomaticAttempt = value;
                RaiseOnPropertyChanged();
            }
        }

        private bool isTalkAvailable = false;
        private bool HasEffectBeenRemoved = false;
        #endregion
        public PersonalPage()
        {
            InitializeComponent();

            HanditemCmbx.Items.AddRange(_Handitems);
            HanditemCmbx.SelectedIndex = 0;


            Bind(CreditsChbx, "Checked", nameof(CreditsChecked));
            Bind(CrystalsChbx, "Checked", nameof(CrystalsChecked));
            Bind(DucketsChbx, "Checked", nameof(DucketsChecked));

            Bind(CreditsNbx, "Value", nameof(CreditsValue));
            Bind(CrystalsNbx, "Value", nameof(CrystalsValue));
            Bind(DucketsNbx, "Value", nameof(DucketsValue));
            Bind(UserIntUpDwn, "Value", nameof(TradeSpammerUserID));
            Bind(TradeSpammerCooldownNbx, "Value", nameof(TradeSpammerCooldown));
            Bind(BadgeTxbx, "Text", nameof(badgecode));

        }

        public override void In_GenericErrorMessages(DataInterceptedEventArgs e)
        {
            e.IsBlocked = BlockBypassers;
        }

        public override void In_RoomAccessDenied(DataInterceptedEventArgs e)
        {
            e.IsBlocked = BlockBypassers;
        }

        public override void In_HotelView(DataInterceptedEventArgs e)
        {
            e.IsBlocked = BlockBypassers;
        }


        public override void In_RoomOpen(DataInterceptedEventArgs e)
        {
            if (AutomaticAttempt)
            {
                RoomBypass();
            }
        }

        public override void In_UserPermissions(DataInterceptedEventArgs e)
        {
            if (!HasUserPermissionsMessage)
            {
                Permissions = e.Packet;
                HasUserPermissionsMessage = true;
            }
        }

        public override void Out_TradeStart(DataInterceptedEventArgs e)
        {
            if (IsInterceptTradeUserOn)
            {
                TradeSpammerUserID = e.Packet.ReadInteger();
                TradeSpammerspeak("Victim ID Found!");
                IsInterceptTradeUserOn = false;
                e.IsBlocked = true;
            }
        }


        public override void In_TradeStopped(DataInterceptedEventArgs e)
        {
            e.IsBlocked = TradeSpammerActivated;
        }

        private void EnableModToolsBtn_Click(object sender, EventArgs e)
        {
            ManageModTools();
        }

        private void AcquireMODPermissionsBtn_Click(object sender, EventArgs e)
        {
            ManageModPermissions();
        }

        private void ManageModPermissions()
        {
            if (HasStaffPermissions)
            {
                if (HasUserPermissionsMessage)
                {
                    Speak("Your Normal permissions have been restored.");
                    _ = SendToClient(Permissions);
                }
                else
                {
                    Speak("Default permissions have been restored.");
                    _ = SendToClient(In.UserPermissions, 2, 4, false);
                    WriteToButton(AcquireMODPermissionsBtn, "Acquire MOD Permissions (CS)");
                }
                HasStaffPermissions = false;
            }
            else
            {
                Speak("Permissions set to Staff.");
                _ = SendToClient(In.UserPermissions, int.MaxValue, int.MaxValue, false);
                WriteToButton(AcquireMODPermissionsBtn, "Remove MOD Permissions (CS)");
                HasStaffPermissions = true;

            }
        }

        private void Speak(string text)
        {
            _ = SendToClient(In.RoomUserWhisper, 0, "[Personal]: " + text, 0, 34, 0, -1);
        }


        private void WriteToButton(SKoreButton button, string text)
        {
            Invoke((MethodInvoker)delegate
            {
                button.Text = text;
            });
        }


        private void ManageModTools()
        {
            if (HasModToolsUnlocked)
            {
                SendPacketModTools(false);
                HasModToolsUnlocked = false;
                WriteToButton(EnableModToolsBtn, "Show Mod Tools (CS)");
            }
            else
            {
                SendPacketModTools(true);
                HasModToolsUnlocked = true;
                WriteToButton(EnableModToolsBtn, "Hide Mod Tools (CS)");
            }
        }


        private void SendPacketModTools(bool value)
        {
            _ = SendToClient(In.ModTool, new object[]
                {
                0,
                0,
                0,
                value,
                value,
                value,
                value,
                value,
                value,
                value,
                0
});
        }

        private void SetCurrencyBtn_Click(object sender, EventArgs e)
        {
            SetCurrencyOnClient();
        }

        private void SetCurrencyOnClient()
        {

            if (CreditsChecked)
            {
                SetCredits(CreditsValue);
            }
            if (CrystalsChecked)
            {
                SetCrystals(CrystalsValue, DucketsValue);

                if (DucketsChecked)
                {
                    SetDuckets(DucketsValue);
                }
            }
        }

        private void SetCrystals(int crystals, int duckets)
        {
            _ = SendToClient(In.UserCurrency, new object[]
       {
                        11,
                        0,
                        duckets,
                        1,
                        0,
                        2,
                        0,
                        3,
                        0,
                        4,
                        0,
                        5,
                        crystals,
                        101,
                        0,
                        102,
                        0,
                        103,
                        0,
                        104,
                        0,
                        105,
                        0
       });
        }

        private void SetDuckets(int Duckets)
        {
            _ = SendToClient(In.UserPoints, Duckets + ".0");
        }

        private void SetCredits(int Credits)
        {
            _ = SendToClient(In.UserCredits, CreditsValue + ".0");
        }



        private void StartTradeSpammer()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                do
                {
                    if (TradeSpammerActivated)
                    {
                        _ = SendToServer(Out.TradeStart, TradeSpammerUserID);
                        Thread.Sleep(TradeSpammerCooldown);
                        _ = SendToServer(Out.TradeClose);
                        Thread.Sleep(TradeSpammerCooldown);
                    }
                } while (TradeSpammerActivated);
            }).Start();
        }




        private void RequestRoomHeightmap()
        {
            _ = SendToServer(Out.RequestRoomHeightmap);
        }

        private void CrashUserBtn_Click(object sender, EventArgs e)
        {
            if (TradeSpammerActivated)
            {
                WriteToButton(CrashUserBtn, "Spam Trade : OFF");
                TradeSpammerActivated = false;
            }
            else
            {
                WriteToButton(CrashUserBtn, "Spam Trade : ON");
                TradeSpammerActivated = true;
                StartTradeSpammer();
            }
        }

        private void CaptureTradeUserBtn_Click(object sender, EventArgs e)
        {
            TradeSpammerspeak("Please trade once with the victim to intercept his Trade user ID.");
            IsInterceptTradeUserOn = true;
        }

        private void TradeSpammerspeak(string text)
        {
            _ = SendToClient(In.RoomUserWhisper, 0, "[Trade Spammer]: " + text, 0, 34, 0, -1);
        }

        private void EnterRoomBtn_Click(object sender, EventArgs e)
        {
            RequestRoomHeightmap();

        }

        private void AutomaticBypassBtn_Click(object sender, EventArgs e)
        {
            if (AutomaticAttempt)
            {
                WriteToButton(AutomaticBypassBtn, "Automatic: OFF");
                AutomaticAttempt = false;
            }
            else
            {
                WriteToButton(AutomaticBypassBtn, "Automatic: ON");
                AutomaticAttempt = true;
            }
        }

        public override void In_RoomUserEffect(DataInterceptedEventArgs e)
        {
            int UserIndex = e.Packet.ReadInteger();
            if (UserIndex == GlobalInts.OwnUser_index && !HasEffectBeenRemoved)
            {
                HasEffectBeenRemoved = true;
            }
        }
        public override void Out_RequestWearingBadges(DataInterceptedEventArgs e)
        {
            _selectedUserId = e.Packet.ReadInteger();
            if (giveHanditemToselecteduser)
            {
                _ = SendToServer(Out.RoomUserTalk, ":handitem " + HanditemID, GlobalInts.Selected_bubble_ID);
                _ = SendToServer(Out.RoomUserGiveHandItem, _selectedUserId);
            }
        }

        private async void RemoveEnableOnlyBobba()
        {
            if (KnownDomains.isBobbaHotel)
            {
                if (isTalkAvailable && !HasEffectBeenRemoved)
                {
                    await Task.Delay(500);
                    await SendToServer(Out.RoomUserTalk, ":enable 0", GlobalInts.Selected_bubble_ID);
                    isTalkAvailable = false;
                }
            }
        }

        public override void Out_RequestRoomLoad(DataInterceptedEventArgs e)
        {
            isTalkAvailable = true;
            HasEffectBeenRemoved = false;
            hasReceivedBanListResponse = false;

        }

        public override void Out_RequestRoomHeightmap(DataInterceptedEventArgs e)
        {
            isTalkAvailable = true;
            HasEffectBeenRemoved = false;
            hasReceivedBanListResponse = false;
        }

        public override void In_RoomUserStatus(DataInterceptedEventArgs e)
        {
            RemoveEnableOnlyBobba();
        }

        private async void RoomBypass()
        {
            await Task.Delay(200);
            await SendToServer(Out.RequestRoomHeightmap);
            WriteToButton(AutomaticBypassBtn, "Automatic: OFF");
            AutomaticAttempt = false;
        }







        private void BlockRestrictionsBtn_Click(object sender, EventArgs e)
        {
            if (BlockBypassers)
            {
                WriteToButton(BlockRestrictionsBtn, "Block Restrictions : OFF");
                BlockBypassers = false;

            }
            else
            {
                WriteToButton(BlockRestrictionsBtn, "Block Restrictions : ON");
                BlockBypassers = true;
            }
        }

        private void GiveHanditemToMyself_Click(object sender, EventArgs e)
        {
            _ = SendToServer(Out.RoomUserTalk, ":handitem " + HanditemID, 18);
        }


        private void GiveallUserHanditemBtn_Click(object sender, EventArgs e)
        {
            GiveAllUserHanditem(HanditemID);
        }

        private async void GiveAllUserHanditem(int handitem)
        {
            try
            {
                if (GlobalLists.UsersInRoom.Count != 0 && GlobalLists.UsersInRoom != null)
                {
                    foreach (HEntity user in GlobalLists.UsersInRoom)
                    {
                        Thread.Sleep(50);
                        await SendToServer(Out.RoomUserTalk, ":handitem " + handitem.ToString(), 18);
                        await SendToServer(Out.RoomUserGiveHandItem, user.Id);
                    }
                    await SendToServer(Out.RoomUserTalk, ":handitem " + handitem.ToString(), 18);

                }
            }
            catch (Exception)
            {

            }
        }

        private class Handitems
        {
            public string Name { get; }

            public int ID { get; }

            public Handitems(int id, string name)
            {
                ID = id;
                Name = name;
            }

            public override string ToString() => $"{Name} [ID: {ID}]";
        }

        private void GiveHanditemToClickedUserbtn_Click(object sender, EventArgs e)
        {
            if (giveHanditemToselecteduser)
            {
                WriteToButton(GiveHanditemToClickedUserbtn, "Give Handitem to clicked user : OFF");
                giveHanditemToselecteduser = false;
            }
            else
            {
                WriteToButton(GiveHanditemToClickedUserbtn, "Give Handitem to clicked user : ON");
                giveHanditemToselecteduser = true;
            }
        }

        private void HanditemSubBtn_Click(object sender, EventArgs e)
        {
            HanditemHunter--;
            _ = SendToServer(Out.RoomUserTalk, ":handitem " + HanditemHunter, 18);

        }

        private void HanditemAddBtn_Click(object sender, EventArgs e)
        {
            HanditemHunter++;
            _ = SendToServer(Out.RoomUserTalk, ":handitem " + HanditemHunter, 18);

        }

        private void Handitemnbx_ValueChanged(object sender, EventArgs e)
        {
            _ = SendToServer(Out.RoomUserTalk, ":handitem " + HanditemHunter, 18);

        }

        private void GiveBadgeToYourselfBtn_Click(object sender, EventArgs e)
        {
            _ = SendToServer(Out.RoomUserTalk, ":givebadge " + GlobalStrings.UserDetails_Username + " " + badgecode, 18);
        }

        private void HanditemCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            HanditemID = ((Handitems)HanditemCmbx.SelectedItem).ID;
        }

        private void TeleportBtn_Click(object sender, EventArgs e)
        {
            _ = SendToServer(Out.RoomUserTalk, ":tp", GlobalInts.Selected_bubble_ID);
        }

        private void OverrideBtn_Click(object sender, EventArgs e)
        {
            _ = SendToServer(Out.RoomUserTalk, ":override", GlobalInts.Selected_bubble_ID);
        }

        private void RequestRoomBannedUsersBtn_Click(object sender, EventArgs e)
        {
            _ = SendToServer(Out.RoomRequestBannedUsers, GlobalInts.ROOM_ID);
        }


        public override void In_RoomBannedUsers(DataInterceptedEventArgs e)
        {
            if (KnownDomains.isBobbaHotel)
            {
                e.IsBlocked = true;
                BobbaTempFix(e.Packet);
            }
        }


        private async void BobbaTempFix(HMessage e)
        {
            
            await Task.Delay(2500);
            if(!hasReceivedBanListResponse)
            {
                await SendToClient(e);
                hasReceivedBanListResponse = true;
                await Task.Delay(2500);
                DisableBanListResponse();
            }
        }

        private async void DisableBanListResponse()
        {
            await Task.Delay(3500);
            hasReceivedBanListResponse = true;
        }



        public override void Out_RoomRequestBannedUsers(DataInterceptedEventArgs e)
        {
            if (KnownDomains.isBobbaHotel)
            {
                e.IsBlocked = true;
                _ = SendToServer(e.Packet);
            }
        }
    }
}

