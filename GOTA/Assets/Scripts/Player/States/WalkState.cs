using Player.States.Base;
using UnityEngine;
using Utilities;

namespace Player.States
{
    public class WalkState : State
    {
        private PlayerData _data;
        private PlayerModel _model;

        public WalkState(GameContext context, StatesEngine statesEngine) : base(context, statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;

            _model = _context.PlayerModel;
            _data = _context.PlayerData;
            Type = StatesType.Walk;
        }

        public override void Disable()
        {
            _model.Walk(false);
        }

        public override void Enable()
        {
            Debug.Log("Walk state");

            _model.IsSprint = false;
            
            _input = Vector2.zero;
            _velocity = Vector3.zero;
            _currentVelocity = Vector3.zero;
            _gravityVelocity.y = 0;

            _playerSpeed = _data.DefaultSpeed;
            _model.IsGrounded = _context.GlobalContainer.PlayerView.CharacterController.isGrounded;
        }

        public override void DoLogic()
        {
            _model.Walk(_model.IsWalk);

            if (_model.IsJump)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Jump));
            }
            
            if (_model.IsSprint)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Sprint));
            }

            _context.GlobalContainer.PlayerView.SetAnimationSpeed(_input.magnitude, _data.SpeedDampTime, Time.deltaTime);
        }
    }
}