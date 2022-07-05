using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data", order = 51)]
    public class PlayerData : ScriptableObject
    {
        [Header("Speed")] 
        public float BaseSpeed = 4f;
        public float SpeedModifier = 1f;
    }
}