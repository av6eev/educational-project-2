using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class RunData
    {
        [field: SerializeField][field: Range(1f, 2f)] public float SpeedModifier { get; private set; } = 1f;
    }
}