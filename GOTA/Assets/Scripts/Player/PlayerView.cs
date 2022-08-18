using Player.Data.States;
using Player.Utilities;
using Player.Utilities.Data;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public AnimationsData AnimationsData { get; private set; }
        public Rigidbody Rigidbody;
        public Transform CameraTransform;
        public Animator Animator;
        public ColliderUtility ColliderUtility;
        public LayerData LayerData;
        
        private void Awake()
        {
            if (Camera.main != null) CameraTransform = Camera.main.transform;

            AnimationsData.Initialize();
        }
        
        private void OnValidate()
        {
            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateColliderDimensions();
        }
    }
}