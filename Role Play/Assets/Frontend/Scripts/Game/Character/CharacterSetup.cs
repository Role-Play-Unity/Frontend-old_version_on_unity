using UnityEngine;
using Mirror;

public class CharacterSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] conponentsToDisable;

    [SerializeField]
    private string ramoteLayerName = "RemotePlayer";
    Camera sceneCamera;
    private void Start()
    {
        Debug.Log("Discord RPC - Initialize Start");

        if (isLocalPlayer)
        {
           // DiscordRPCSend();
            //Camera.main?.gameObject.SetActive(false);
        }
        else
        {
            DisableComponents();
            AssignRemoteLayer();
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        MasterCharacter character = GetComponent<MasterCharacter>();

        GameMaster.RegisterPlayer(netID, character);

    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(ramoteLayerName);
    }
    private void DisableComponents()
    {
        for (int i = 0; i < conponentsToDisable.Length; i++)
        {
            conponentsToDisable[i].enabled = false;
        }
    }
    /*public DiscordRpcMaster.EventHandlers DiscordRPCHandlers;
    private void DiscordRPCSend()
    {
        Debug.Log("Discord RPC - Initialize Start");
        DiscordRpcMaster.Initialize("678044314860781578", ref DiscordRPCHandlers, true, "");

        Debug.Log("Discord RPC - Initialize");

        Debug.Log("Discord RPC - Update Presence");
        DiscordRpcMaster.UpdatePresence(new DiscordRpcMaster.RichPresence {
            state = "Game in Role Play",
            details = "Role Play on Unity",
            startTimestamp = (long)Time.realtimeSinceStartup,

            //largeImageKey = "ff",
            //largeImageText = "V0.0.0.1 | Testing RPC",
        });
        Debug.Log("Discord RPC - UpdatePresence");
    }*/


    private void OnDisable()
    {
        //Camera.main?.gameObject.SetActive(true);
        GameMaster.UnregisterPlayer(transform.name);
    }
}