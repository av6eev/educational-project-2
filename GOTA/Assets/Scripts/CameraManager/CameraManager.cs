using System;
using Cinemachine;
using UnityEngine;

namespace CameraManager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraData _data;
        private CinemachineFramingTransposer _framingTransposer;
        private CinemachineInputProvider _inputProvider;

        private float _currentDistanceToTarget;
        private void Awake()
        {
            _framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            _inputProvider = GetComponent<CinemachineInputProvider>();

            _currentDistanceToTarget = _data.DefaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            var zoomValue = _inputProvider.GetAxisValue(2) * _data.ZoomSensitivity;
            var currentDistance = _framingTransposer.m_CameraDistance;

            _currentDistanceToTarget = Math.Clamp(_currentDistanceToTarget + zoomValue, _data.MinDistance, _data.MaxDistance);
            if (currentDistance.Equals(_currentDistanceToTarget)) return;

            var lerpedZoomValue = Mathf.Lerp(currentDistance, _currentDistanceToTarget, _data.Smoothing * Time.deltaTime);
            _framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}