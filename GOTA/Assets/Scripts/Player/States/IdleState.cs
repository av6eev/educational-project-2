using Player.States.Base;
using UnityEngine;
using Utilities;

namespace Player.States
{
    public class IdleState : State
    {
        private readonly PlayerModel _model;
        private readonly PlayerData _data;

        public IdleState(GameContext context, StatesEngine statesEngine) : base(context, statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;
            
            _model = _context.PlayerModel;
            _data = _context.PlayerData;

            Type = StatesType.Idle;
        }

        public override void Disable()
        {
            _gravityVelocity.y = 0;
        }
        
        public override void Enable()
        {
            Debug.Log("Idle state");
            _model.Idle(0);

            _model.IsJump = false;
            _model.IsSprint = false;
            _model.IsWalk = false;
            
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
            if (_model.IsJump)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Jump));
            }

            if (_model.IsWalk)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Walk));
            }
        }

        public override void DoPhysics(float deltaTime) {}
    }
}