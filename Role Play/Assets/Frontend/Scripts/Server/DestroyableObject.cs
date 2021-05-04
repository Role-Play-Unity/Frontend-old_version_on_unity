using UnityEngine;
using Mirror;

public class DestroyableObject : NetworkBehaviour, IDamageble
{
    [SerializeField] float HealthMax;
    [SerializeField] float Health;
    private void Start()
    {
        SetMaxHP();
    }
    [Server]
    private void SetMaxHP()
    {
        Health = HealthMax;
    }
    [Server]
    public void Damage(float amout)
    {
        Health -= amout;
        if(Health < 1)
        {
            Die();
        }
    }
    [Server]
    public void Die()
    {
        Destroy(gameObject);
    }
}
