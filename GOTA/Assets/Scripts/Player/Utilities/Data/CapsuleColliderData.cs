using UnityEngine;

namespace Player.Utilities.Data
{
    public class CapsuleColliderData
    {
        public CapsuleCollider Collider { get; private set; }
        public Vector3 LocalColliderCenter { get; private set; }

        public void Initialize(GameObject go)
        {
            if (Collider != null) return;

            Collider = go.GetComponent<CapsuleCollider>();

            UpdateData();
        }

        public void UpdateData()
        {
            LocalColliderCenter = Collider.center;
        }
    }
}