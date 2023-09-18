using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class GroundedData
    {
        [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField][field: Range(0f, 5f)] public float GroundToFallRayDistance { get; private set; } = 1f;
        
        [field: SerializeField] public AnimationCurve SlopeSpeedAngles { get; private set; }
        
        [field: SerializeField] public RotationData RotationData { get; private set; }
        [field: SerializeField] public WalkData WalkData { get; private set; }
        [field: SerializeField] public RunData RunData { get; private set; }
        [field: SerializeField] public DashData DashData { get; private set; }
        [field: SerializeField] public StopData StopData { get; private set; }
        [field: SerializeField] public RollData RollData { get; private set; }
    }
}