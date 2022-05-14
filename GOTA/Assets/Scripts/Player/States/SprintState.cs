using Player.States.Base;
using UnityEngine;
using Utilities;

namespace Player.States
{
    public class SprintState : State
    {
        private readonly PlayerModel _model;
        private readonly PlayerData _data;
        
        public SprintState(GameContext context, StatesEngine statesEngine) : base(context, statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;

            _model = _context.PlayerModel;
            _data = _context.PlayerData;
            
            Type = StatesType.Sprint;
        }

        public override void Disable()
        {
            _model.Sprint(_input, _data, false);
        }

        public override void Enable()
        {
            Debug.Log("Sprint state");

            _input = Vector2.zero;
            _velocity = Vector3.zero;
            _currentVelocity = Vector3.zero;
            _gravityVelocity.y = 0;

            _playerSpeed = _data.DefaultSpeed;
            _model.IsGrounded = _context.GlobalContainer.PlayerView.CharacterController.isGrounded;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            
            _model.IsWalk = _moveAction.IsPressed();
        }

        public override void DoLogic()
        {
            if (_model.IsSprint)
            {
                _model.Sprint(_input, _data, true);
            }
            else if (_model.IsWalk)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Walk));
            }
            else
            {
                _model.Sprint(_input, _data, false);
                StatesEngine.Change(StatesEngine.Get(StatesType.Idle));
            }

            if (_model.IsJump)
            {
                _model.Jump(true);
                StatesEngine.Change(StatesEngine.Get(StatesType.Jump));
            }
        }
    }
}