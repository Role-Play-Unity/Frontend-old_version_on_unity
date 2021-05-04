using UnityEngine;

namespace Models.Game.Weapon
{
    [System.Serializable]
    public class Weapon
    {
        public string name = "AR-15";
        public float springForce = 3f;
        public float shotDelay = 0.01f;
        public float range = 100f;
    }
}