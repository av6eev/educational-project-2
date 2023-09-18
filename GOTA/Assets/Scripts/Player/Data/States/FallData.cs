using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class FallData
    {
        [field: SerializeField] [field: Range(1f, 15f)] public float SpeedLimit { get; private set; } = 15f;
        [field: SerializeField] [field: Range(0f, 100f)] public float MinDistanceToHardFall { get; private set; } = 3f;
    }
}