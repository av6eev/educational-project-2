using UnityEngine;
using Utilities;

namespace Player.Systems
{
    public class CameraRotationSystem : ISystem
    {
        private readonly GameContext _context;
        private readonly PlayerView _view;
        private readonly PlayerModel _model;

        public CameraRotationSystem(GameContext context)
        {
            _context = context;

            _view = _context.GlobalContainer.PlayerView;
            _model = _context.PlayerModel;
        }

        public void Update(float deltaTime)
        {
            // if (_model.IsRotationButtonEnable)
            // {
                // var targetRotation = Quaternion.Euler(0, _view.CameraTransform.eulerAngles.y, 0);
                // _view.transform.rotation = Quaternion.Lerp(_view.transform.rotation, targetRotation, _context.PlayerData.RotationSpeed * deltaTime);
            // }
        }
    }
}