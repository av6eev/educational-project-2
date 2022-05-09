using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Utilities;

namespace InputManager
{
    public class InputController : IController
    {
        private readonly PlayerInput _playerInput;
        private readonly GameContext _context;
        private readonly InputModel _model;

        private readonly List<InputAction> _actions = new List<InputAction>();

        public InputController(GameContext context, InputModel model, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _context = context;
            _model = model;

            foreach (var action in _playerInput.actions)
            {
                _actions.Add(action);
            }
        }

        public void Deactivate()
        {
            Detach();
        }

        public void Activate()
        {
            Attach();
        }

        private void Detach()
        {
            foreach (var inputAction in _actions)
            {
                inputAction.Disable();
            }
            
            _model.InputActions.Clear();
        }
        
        private void Attach()
        {
            foreach (var inputAction in _actions)
            {
                if (!_model.InputActions.ContainsKey(inputAction.name))
                {
                    _model.InputActions.Add(inputAction.name, inputAction);
                    inputAction.Enable();
                }
            }
        }
    }
}