using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class DashData
    {
        [field: SerializeField] [field: Range(1f, 3f)] public float SpeedModifier { get; private set; } = 2f;
        [field: SerializeField] [field: Range(0f, 2f)] public float ConsideredConsecutiveTime { get; private set; } = 1f;
        [field: SerializeField] [field: Range(1, 10)] public int DashesLimitAmount { get; private set; } = 2;
        [field: SerializeField] [field: Range(0f, 5f)] public float DashLimitCooldown { get; private set; } = 1.8f;
        
        [field: SerializeField] public RotationData RotationData { get; private set; }
    }
}