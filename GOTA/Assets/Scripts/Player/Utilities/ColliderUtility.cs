using System;
using Player.Utilities.Data;
using UnityEngine;

namespace Player.Utilities
{
    [Serializable]
    public class ColliderUtility
    {
        public CapsuleColliderData CapsuleColliderData { get; private set; }
        [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }
        [field: SerializeField] public SlopeData SlopeData { get; private set; }

        public void Initialize(GameObject go)
        {
            if (CapsuleColliderData != null) return;
            
            CapsuleColliderData = new CapsuleColliderData();
            CapsuleColliderData.Initialize(go);
        }
        
        public void CalculateColliderDimensions()
        {
            SetColliderRadius(DefaultColliderData.Radius);
            SetColliderHeight(DefaultColliderData.Height * (1f - SlopeData.StepHeightPercentage));
            RecalculateColliderCenter();

            var halfColliderHeight = CapsuleColliderData.Collider.height; 
            
            if (halfColliderHeight / 2f < CapsuleColliderData.Collider.radius)
            {
                SetColliderRadius(halfColliderHeight);
            }
            
            CapsuleColliderData.UpdateData();
        }

        public void RecalculateColliderCenter()
        {
            var heightDifference = DefaultColliderData.Height - CapsuleColliderData.Collider.height;
            var newColliderCenter = new Vector3(0f, DefaultColliderData.CenterY + (heightDifference / 2f), 0f);

            CapsuleColliderData.Collider.center = newColliderCenter;
        }

        public void SetColliderRadius(float radius)
        {
            CapsuleColliderData.Collider.radius = radius;
        }
        
        public void SetColliderHeight(float height)
        {
            CapsuleColliderData.Collider.height = height;
        }
    }
}