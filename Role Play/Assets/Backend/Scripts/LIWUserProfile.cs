using MasterServerToolkit.Networking;
using MasterServerToolkit.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MasterServerToolkit.MasterServer.Examples.BasicProfile
{
    public enum ObservablePropertiyCodes { DisplayName, Avatar, Username, Money }

    public class LIWUserProfile : ProfilesModule
    {
        [Header("Start Values"), SerializeField]
        private string username = "LIW USER PROFILE | PROFILES MODULE";
        [SerializeField]
        private string avatar = "https://i.imgur.com/JQ9pRoD.png";
        [SerializeField]
        private int money = 50;

        public HelpBox _header = new HelpBox()
        {
            Text = "This script is a custom module, which sets up profiles values for new users"
        };

        public override void Initialize(IServer server)
        {
            base.Initialize(server);

            // Set the new factory in ProfilesModule
            ProfileFactory = CreateProfileInServer;

            server.RegisterMessageHandler((short)MstMessageCodes.UpdateDisplayNameRequest, UpdateDisplayNameRequestHandler);

            //Update profile resources each 5 sec
            InvokeRepeating(nameof(IncreaseResources), 1f, 1f);
        }

        /// <summary>
        /// This method is just for creation of profile on server side as default for users that are logged in for the first time
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientPeer"></param>
        /// <returns></returns>
        private ObservableServerProfile CreateProfileInServer(string userId, IPeer clientPeer)
        {
            return new ObservableServerProfile(userId, clientPeer)
            {
                new ObservableString((short)ObservablePropertiyCodes.DisplayName, SimpleNameGenerator.Generate(Gender.Male)),
                new ObservableString((short)ObservablePropertiyCodes.Avatar, avatar),
                new ObservableString((short)ObservablePropertiyCodes.Username, username),
                new ObservableInt((short)ObservablePropertiyCodes.Money, money),
            };
        }

        private void IncreaseResources()
        {
            foreach (var profile in Profiles)
            {
                var moneyProperty = profile.GetProperty<ObservableInt>((short)ObservablePropertiyCodes.Money);
                moneyProperty.Add(1);
            }
        }

        private void UpdateDisplayNameRequestHandler(IIncomingMessage message)
        {
            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();

            if (userExtension == null || userExtension.Account == null)
            {
                message.Respond("Invalid session", ResponseStatus.Unauthorized);
                return;
            }

            var newProfileData = new Dictionary<string, string>().FromBytes(message.AsBytes());

            try
            {
                if (profilesList.TryGetValue(userExtension.Username, out ObservableServerProfile profile))
                {
                    profile.GetProperty<ObservableString>((short)ObservablePropertiyCodes.DisplayName).Set(newProfileData["displayName"]);
                    profile.GetProperty<ObservableString>((short)ObservablePropertiyCodes.Avatar).Set(newProfileData["avatar"]);

                    message.Respond(ResponseStatus.Success);
                }
                else
                {
                    message.Respond("Invalid session", ResponseStatus.Unauthorized);
                }
            }
            catch (Exception e)
            {
                message.Respond($"Internal Server Error: {e}", ResponseStatus.Error);
            }
        }
    }
}
