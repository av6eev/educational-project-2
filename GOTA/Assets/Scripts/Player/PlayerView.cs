using Player.Utilities;
using Player.Utilities.Data;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Transform CameraTransform;
        public Animator Animator;
        public ColliderUtility ColliderUtility;
        public LayerData LayerData;

        private readonly GameContext _context = new GameContext();
        
        private void Awake()
        {
            CameraTransform = Camera.main.transform;
        }
        
        private void OnValidate()
        {
            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateColliderDimensions();
        }

    }
}