using System;
using Cinemachine;
using ScriptableObjects;
using UnityEngine;

namespace CameraManager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraData _data;
        private CinemachineFramingTransposer _framingTransposer;
        private CinemachineInputProvider _inputProvider;

        private float _currentDistanceToTarger;
        private void Awake()
        {
            _framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            _inputProvider = GetComponent<CinemachineInputProvider>();

            _currentDistanceToTarger = _data.DefaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            var zoomValue = _inputProvider.GetAxisValue(2) * _data.ZoomSensitivity;
            var currentDistance = _framingTransposer.m_CameraDistance;

            _currentDistanceToTarger = Math.Clamp(_currentDistanceToTarger + zoomValue, _data.MinDistance, _data.MaxDistance);
            if (currentDistance.Equals(_currentDistanceToTarger)) return;

            var lerpedZoomValue = Mathf.Lerp(currentDistance, _currentDistanceToTarger, _data.Smoothing * Time.deltaTime);
            _framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}