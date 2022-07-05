using Player.Data.States;
using UnityEngine;

namespace Player.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSO", order = 53)]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public GroundedData GroundedData { get; private set; }
    }
}