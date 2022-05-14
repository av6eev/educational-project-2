using System;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerController : IController
    {
        private readonly GameContext _context;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public PlayerController(GameContext context, PlayerModel model, PlayerView view)
        {
            _context = context;
            _model = model;
            _view = view;
        }

        public void Deactivate()
        {
            _model.OnWalk -= Walk;
            _model.OnJump -= Jump;
            _model.OnLand -= Land;
            _model.OnSprint -= Sprint;
            _model.OnIdle -= Idle;
        }

        public void Activate()
        {
            _model.OnWalk += Walk;
            _model.OnJump += Jump;
            _model.OnLand += Land;
            _model.OnSprint += Sprint;
            _model.OnIdle += Idle;
        }

        private void Idle(float speed)
        {
            _view.Idle(speed);
        }

        private void Land(bool isEnable)
        {
            _view.Land(isEnable);
        }

        private void Sprint(Vector2 input, PlayerData data, bool isEnable)
        {
            _view.Sprint(input, data, isEnable);
        }

        private void Jump(bool isEnable)
        {
            _view.Jump(isEnable);
        }

        private void Walk(bool isEnable)
        {
            _view.Walk(isEnable);
        }
    }
}