using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class MasterServerSetting
{
    public static MasterServer Setting = new MasterServer();

    public static void Build(MasterServer setting) { Save(setting); }

    public static void Save(MasterServer setting)
    {
        Setting = setting;
        if (!Directory.Exists($"{Setting.MASTER_SERVER_PATH}/Config"))
        {
            Directory.CreateDirectory($"{Setting.MASTER_SERVER_PATH}/Config");
        }

        if (!File.Exists($"{Setting.MASTER_SERVER_PATH}/Config/Settings.ini"))
        {
            //File.Create($"{Setting.MASTER_SERVER_PATH}/Config/Settings.ini");
        }

        File.WriteAllText($"{Setting.MASTER_SERVER_PATH}/Config/Settings.ini", JsonUtility.ToJson(setting));
        Debug.Log(JsonUtility.ToJson(Setting));
    }
    public static void Loading()
    {
        if (File.Exists($"Config/Settings.ini"))
        {
            Setting = JsonUtility.FromJson<MasterServer>(File.ReadAllText("Config/Settings.ini"));
        }
        else { 
            //Setting = JsonUtility.FromJson<MasterServer>(File.ReadAllText("Config/Settings.ini"));
        }
    }
    /**/

    public class MasterServer
    {
        public string MASTER_SERVER_PATH;
        public string GAME_SERVER_PATH = @"C:\Project\Role_Play_Unity\Windows\Build\GameServer\ROLE_PLAY.exe";

        public bool PUBLIC_IP = false;
        public string HOST = "127.0.0.1";
        public int PORT = 20250;
    }
}