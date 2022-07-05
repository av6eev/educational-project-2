using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class WalkData
    {
        [field: SerializeField][field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 0.225f;
    }
}