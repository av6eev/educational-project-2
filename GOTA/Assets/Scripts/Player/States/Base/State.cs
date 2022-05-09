using InputManager;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.States.Base
{
    public abstract class State
    {
        protected StatesEngine StatesEngine;
        protected GameContext _context;

        protected Vector3 _velocity;
        protected Vector3 _currentVelocity;
        protected Vector3 _gravityVelocity;
        protected Vector2 _input;
        protected Vector3 _playerVelocity;
        protected float _playerSpeed;
        
        protected InputAction _moveAction;
        protected InputAction _lookAction;
        protected InputAction _jumpAction;

        public StatesType Type;

        protected State(GameContext context, StatesEngine statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;

            _moveAction = _context.InputModel.InputActions[InputActions.Movement];
            _jumpAction = _context.InputModel.InputActions[InputActions.Jump];
            _lookAction = _context.InputModel.InputActions[InputActions.Look];
        }
        
        public abstract void Disable();

        public abstract void Enable();

        public abstract void HandleInput();

        public abstract void DoLogic();

        public abstract void DoPhysics(float deltaTime);
    }
}