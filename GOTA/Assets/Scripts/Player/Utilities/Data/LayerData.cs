using System;
using UnityEngine;

namespace Player.Utilities.Data
{
    [Serializable]
    public class LayerData
    {
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    }
}