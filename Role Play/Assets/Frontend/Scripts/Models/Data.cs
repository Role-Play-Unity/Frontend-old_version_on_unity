using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DATA : MonoBehaviour
{
    public static class VERSION {
        public static string MASTER_LOADING_UI  = "0.3";
        public static string MASTER_LOADING_SCRIPT  = "0.1";
        public static string MASTER_GAME_MENU_UI = "0.1";
        public static string MASTER_GAME_MENU_SCRIPT = "0.1";

        public static string MASTER_SERVER      = "0.0.0.000008A";
        public static string MASTER_GAME_SERVER = "0.0.0.000008A";
        public static string MASTER_GAME_CLIENT { 
            get { 
                return "0.0." + (Convert.ToDouble(MASTER_LOADING_UI) + Convert.ToDouble(MASTER_GAME_MENU_UI)).ToString()+ "A"; 
            } 
        } //"0.0.*.**A";

    }
}
