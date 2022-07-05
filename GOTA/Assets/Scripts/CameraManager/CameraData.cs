using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Camera Data", menuName = "Camera Data", order = 52)]
    public class CameraData : ScriptableObject
    {
        [Header("Distance")] 
        [Range(0f,10f)] public float DefaultDistance = 6f;
        [Range(0f,10f)] public float MinDistance = 1f;
        [Range(0f,10f)] public float MaxDistance = 8f; 
        
        [Header("Zooming")]
        [Range(0f,10f)] public float ZoomSensitivity = 5f; 
        [Range(0f,10f)] public float Smoothing = 3f; 
    }
}