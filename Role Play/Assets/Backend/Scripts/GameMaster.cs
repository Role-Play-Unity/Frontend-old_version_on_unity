using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, MasterCharacter> players = new Dictionary<string, MasterCharacter>();

    public static void RegisterPlayer(string netID, MasterCharacter character)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, character);
        character.transform.name = playerId;
        Debug.Log("RegisterPlayer");
    }

    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    private void OnGUI()
    {
        /*
         * GUILayout.BeginArea(new Rect(200, 200, 200, 500));
         * GUILayout.BeginVertical();
         * Debug.Log("OnGUI");
         * foreach (var playerId in players.Keys)
         * {
         *    GUILayout.Label(playerId + " - " + players[playerId].transform.name);
         *    Debug.Log(playerId + " - " + players[playerId].transform.name);
         * }
         * GUILayout.EndVertical();
         * GUILayout.EndArea();
         */
    } 
}
