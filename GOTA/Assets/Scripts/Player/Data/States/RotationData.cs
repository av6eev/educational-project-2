using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class RotationData
    {
        [field: SerializeField] public Vector3 TargetRotationReachTime { get; private set; }
    }
}