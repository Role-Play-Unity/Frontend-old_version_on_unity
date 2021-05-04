using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIW.Tools.Common.Build
{
    [CreateAssetMenu(fileName = "Build Settings", menuName = "LIFE IS WOLF/Tools/Common/Build/Settings", order = 51)]
    public class BuildSettings : ScriptableObject
    {
        public GlobalConfig Main = new GlobalConfig();
        public BuildConfig Build = new BuildConfig();
        public WindowsConfig Windows = new WindowsConfig();
        public LinuxConfig Linux = new LinuxConfig();

        [System.Serializable]
        public class GlobalConfig
        {
            public bool WINDOWS;
            public bool LINUX;

            public bool MASTER_SERVER;
            public bool GAME_SERVER;
            public bool GAME_CLIENT;

            public string[] SCENE_MASTER_SERVER_PATH = new string[] { @"Assets\Backend\Scene\MasterServer.unity" };
            public string[] SCENE_GAME_SERVER_PATH = new string[] { @"Assets\Backend\Scene\GameServer.unity" };
            public string[] SCENE_GAME_CLIENT_PATH = new string[] { @"Assets\Frontend\Scene\Loading.unity", @"Assets\Frontend\Scene\General Menu.unity", @"Assets\Frontend\Scene\NewPlayer.unity", @"Assets\Frontend\Scene\Game.unity" };

        }
        [System.Serializable]
        public class BuildConfig
        {
            public UnityEditor.BuildOptions BuildOptions;

            public string NAME_BUILD_MASTER_SERVER = "MASTER_SERVER";
            public string NAME_BUILD_GAME_SERVER = "ROOM_SERVER";
            public string NAME_BUILD_GAME_CLIENT = "ROLE_PLAY";

            public string VERSION_MASTER_SERVER = "0.0.0.000001A";
            public string VERSION_GAME_SERVER = "0.0.0.000001A";
            public string VERSION_GAME_CLIENT = "0.0.0.000001A";

            public string COMPANY_NAME = "LIFE IS WOLF";
        }
        [System.Serializable]
        public class WindowsConfig
        {
            public ArchitectureType ARCHITECTURE;
            public string BUILD_MASTER_SERVER_PATH = @"C:\Server\Role_Play_Unity\Windows\Master\";
            public string BUILD_GAME_SERVER_PATH = @"C:\Server\Role_Play_Unity\Windows\Game\";
            public string BUILD_GAME_CLIENT_PATH = @"G:\Develop\BUILD\Role_Play_Unity\Windows\Game\";
            public enum ArchitectureType { x86_64, x86, All };
        }

        [System.Serializable]
        public class LinuxConfig
        {
            public string BUILD_MASTER_SERVER_PATH = @"\tmp\Role_Play_Unity\Linux\Master\";
            public string BUILD_GAME_SERVER_PATH = @"\tmp\Role_Play_Unity\Linux\Game\";
            public string BUILD_GAME_CLIENT_PATH = @"\tmp\BUILD\Role_Play_Unity\Linux\Game\";
        }
    }
}