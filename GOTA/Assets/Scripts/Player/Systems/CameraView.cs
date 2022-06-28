using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.Systems
{
    public class CameraView : MonoBehaviour
    {
        public Camera MainCamera;
        public CinemachineVirtualCamera VirtualCamera;
        private void Awake()
        {
            MainCamera.TryGetComponent<CinemachineBrain>(out var brain);
            
            if (brain == null)
            {
                brain = MainCamera.AddComponent<CinemachineBrain>();
            }
        }
    }
}