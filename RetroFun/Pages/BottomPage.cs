﻿using Microsoft.Office.Interop.Excel;
using RetroFun.Globals;
using RetroFun.Helpers;
using RetroFun.Subscribers;
using RetroFun.Utils.Globals;
using Sulakore.Communication;
using Sulakore.Habbo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace RetroFun.Pages
{
    [ToolboxItem(true)]
    [DesignerCategory("UserControl")]
    public partial class BottomPage:  ObservablePage
    {

        private Dictionary<string, HEntity> removedEntities { get => GlobalDictionaries.removedEntities; set { GlobalDictionaries.removedEntities = value; } }

        private Dictionary<int, HEntity> Dictionaryusers { get => GlobalDictionaries.Dictionary_UsersPresentInRoom; set { GlobalDictionaries.Dictionary_UsersPresentInRoom = value; } }




        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<HEntity> CurrentRoomUsers { get => GlobalLists.UsersInRoom; set { GlobalLists.UsersInRoom = value; RaiseOnPropertyChanged(); } }
        public List<GlobalLists.EntityWhisper> EntityWhisperFix { get => GlobalLists.whisperfix; set { GlobalLists.whisperfix = value; RaiseOnPropertyChanged(); } }
        public List<HEntity> UserLeftRoom { get => GlobalLists.UserLeftRoom; set { GlobalLists.UserLeftRoom = value; RaiseOnPropertyChanged(); } }


        public string Own_look { get => GlobalStrings.UserDetails_Look; set { GlobalStrings.UserDetails_Look = value; RaiseOnPropertyChanged(); } }

        public int Own_index { get => GlobalInts.OwnUser_index; set { GlobalInts.OwnUser_index = value; RaiseOnPropertyChanged(); } }


        public bool FreezeUser
        {
            get => Global_bools.FreezeUserMovement;
            set
            {
                Global_bools.FreezeUserMovement = value;
                RaiseOnPropertyChanged();
            }
        }

        public string OwnUsername { get => GlobalStrings.UserDetails_Username; set { GlobalStrings.UserDetails_Username = value; RaiseOnPropertyChanged(); } }


        public BottomPage()
        {
            InitializeComponent();

            Bind(FreezeMovementCheck, "Checked", nameof(FreezeUser));
            Bind(UsernameTextBox, "Text", nameof(OwnUsername));
        }

        private void Speak(string text)
        {
                _ = SendToClient(In.RoomUserWhisper, 0, "[RetroFun Init]: " + text, 0, 34, 0, -1);
        }


        public override async void Out_LatencyTest(DataInterceptedEventArgs obj)
        {
            if (OwnUsername == null)
            {
                   await SendToServer(Out.RequestUserData);
            }
            if (!KnownDomains.isDomainRecognized)
            {
                KnownDomains.RecognizeHostBool(Connection.Host);
                if (KnownDomains.isAKnownHost(Connection.Host))
                {
                    Speak("This is a Known host : " + KnownDomains.GetHostName(Connection.Host));
                    Speak("Setting RetroFun for this known host!");
                }
                else
                {
                    Speak("Setting RetroFun for Unknown Host!");
                    Speak("Some features won't work here Server-side, please contact the developer on discord in case there's a problem with this host : " + GlobalStrings.DeveloperDiscord);
                }
         }
            UpdateMainFrmTitle();
        }

        public override void Out_Username(DataInterceptedEventArgs obj)
        {
            string username = obj.Packet.ReadString();

            if (OwnUsername == null)
            {
                OwnUsername = username;
                UpdateMainFrmTitle();
            }
        }


        public override void In_RoomUsers(DataInterceptedEventArgs obj)
        {
            HEntity[] array = HEntity.Parse(obj.Packet);
            if (array.Length != 0)
            {
                foreach (HEntity hentity in array)
                {
                    var whisperfixers = new GlobalLists.EntityWhisper(hentity, 18);
                    if (UserLeftRoom.Contains(hentity))
                    {
                        UserLeftRoom.Remove(hentity);
                    }
                    if (!Dictionaryusers.ContainsKey(hentity.Id))
                    {
                        Dictionaryusers.Add(hentity.Id, hentity);
                    }
                    if (!EntityWhisperFix.Contains(whisperfixers))
                    {
                        EntityWhisperFix.Add(whisperfixers);
                    }
                    if (!CurrentRoomUsers.Contains(hentity))
                    {
                        CurrentRoomUsers.Add(hentity);
                    }
                    if (OwnUsername != null)
                    {
                        if (hentity.Name == OwnUsername)
                        {
                            Own_index = hentity.Index;
                            Own_look = hentity.FigureId;
                        }
                    }
                }
            }


            UpdateUsersInRoom();
        }


        public override void In_RoomUserShout(DataInterceptedEventArgs e)
        {
            int index = e.Packet.ReadInteger();
            string msg = e.Packet.ReadString();
            e.Packet.ReadInteger();
            int bubbleid = e.Packet.ReadInteger();
            var entity = HentityUtils.FindEntityByIndex(index);
            if (entity != null)
            {
                if (EntityWhisperFix.First(ent => ent.entity == entity).bubbleid != bubbleid)
                {
                    EntityWhisperFix.First(ent => ent.entity == entity).bubbleid = bubbleid;
                }
            }
        }

        public override void In_RoomUserTalk(DataInterceptedEventArgs e)
        {
            int index = e.Packet.ReadInteger();
            string msg = e.Packet.ReadString();
            e.Packet.ReadInteger();
            int bubbleid = e.Packet.ReadInteger();
            var entity = HentityUtils.FindEntityByIndex(index);
            if (entity != null)
            {
                if (EntityWhisperFix.First(ent => ent.entity == entity).bubbleid != bubbleid)
                {
                    EntityWhisperFix.First(ent => ent.entity == entity).bubbleid = bubbleid;
                }
            }
        }

        private void UpdateUsersInRoom()
        {
            Invoke((MethodInvoker)delegate
            {
                UsersInRoomLbl.Text = "Users In Room : " + CurrentRoomUsers.Count;
            });
        }


        private void UpdateMainFrmTitle()
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    if (MainFrm.ActiveForm != null)
                    {
                        MainFrm.ActiveForm.Text = "RetroFun  [Host : " + KnownDomains.GetHostName(Connection.Host) + " ]" +
                        "  " +
                        "[IsKnownHost : " + KnownDomains.isAKnownHost(Connection.Host).ToString() + " ]" +
                        "  " +
                        "[Username : " + OwnUsername + " ]";
                    }
                });
            }
            catch (Exception) { }
        }


        public  override void In_RoomUserRemove(DataInterceptedEventArgs e)
        {
            int index = int.Parse(e.Packet.ReadString());
            var entity1 = HentityUtils.FindEntityByIndex(index);
            var entity2 = HentityUtils.FindEntityByIndexDictionary(index);
            var entity3 = HentityUtils.WhisperEntityFix(index);
            if (entity1 != null)
            {
                CurrentRoomUsers.Remove(entity1);
                UserLeftRoom.Add(entity1);
            }
            if (entity2 != null)
            {
                Dictionaryusers.Remove(entity2.Id);
            }
            if (entity3 != null)
            {
                EntityWhisperFix.Remove(entity3);
            }

            UpdateUsersInRoom();
        }


        public override void Out_RequestRoomLoad(DataInterceptedEventArgs e)
        {
            Dictionaryusers.Clear();
            removedEntities.Clear();
            CurrentRoomUsers.Clear();
            UpdateUsersInRoom();
            EntityWhisperFix.Clear();
            UserLeftRoom.Clear();
        }

        public override void Out_RequestRoomHeightmap(DataInterceptedEventArgs e)
        {
            Dictionaryusers.Clear();
            removedEntities.Clear();
            CurrentRoomUsers.Clear();
            UpdateUsersInRoom();
            EntityWhisperFix.Clear();
            UserLeftRoom.Clear();
        }


        public override void Out_RoomUserWalk(DataInterceptedEventArgs e)
        {
            if(FreezeUser)
            {
                e.IsBlocked = true;
            }
        }
    }
}
