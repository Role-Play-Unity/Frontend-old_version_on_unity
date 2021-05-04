using UnityEngine;
using Mirror;
using System;

public class WeaponShoot : NetworkBehaviour
{
    public Models.Game.Weapon.Weapon weapon;

    [SerializeField]
    private Transform spawnBulletPosition;

    [SerializeField]
    private LayerMask mask;
    void Start()
    {
        if(spawnBulletPosition == null)
        { 
            Debug.LogError("Ты наверное так сильно заебался, что забыл поставить место спавна пули в инспекторе на скрипт 'WeaponShoot'.");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        } 
    }
    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(spawnBulletPosition.position, spawnBulletPosition.forward, out hit, weapon.range, mask))
        {
            if (hit.collider.tag == "Player") { CmdPlayerShot(hit.transform.name); }
        }
    }

    [Command]
    private void CmdPlayerShot(string playerName)
    {
        Debug.Log("Server: " + playerName + " получил пуль в тело)");
    }
}
