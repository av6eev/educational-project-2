using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class RollData
    {
        [field: SerializeField] [field: Range(0f, 3f)] public float SpeedModifier { get; private set; } = 1f;
    }
}