using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class AirborneData
    {
        [field: SerializeField] public JumpData JumpData { get; private set; }
    }
}