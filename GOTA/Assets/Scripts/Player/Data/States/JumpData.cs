using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class JumpData
    {
        [field: SerializeField] public RotationData RotationData { get; private set; }
        [field: SerializeField] [field: Range(0f, 5f)] public float RayDistance { get; private set; } = 2f;
        
        [field: SerializeField] public AnimationCurve UpwardsForceMultiplier { get; private set; }
        [field: SerializeField] public AnimationCurve DownwardsForceMultiplier { get; private set; }
        
        [field: SerializeField] public Vector3 StationaryForce { get; private set; }
        [field: SerializeField] public Vector3 WeakForce { get; private set; }
        [field: SerializeField] public Vector3 MediumForce { get; private set; }
        [field: SerializeField] public Vector3 StrongForce { get; private set; }

        [field: SerializeField] [field: Range(0f, 10f)] public float DecelerationForce { get; private set; } = 1.5f;
    }
}