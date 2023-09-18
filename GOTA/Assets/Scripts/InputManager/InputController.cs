using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace InputManager
{
    public class InputController : IController
    {
        private readonly PlayerInput _playerInput;
        private readonly GameContext _context;
        private readonly InputModel _model;
        private readonly InputComponent _component;

        private readonly List<InputAction> _actions = new List<InputAction>();

        public InputController(GameContext context, InputModel model, PlayerInput playerInput, InputComponent component)
        {
            _playerInput = playerInput;
            _context = context;
            _model = model;
            _component = component;

            foreach (var action in _playerInput.actions)
            {
                _actions.Add(action);
            }
        }

        public void Deactivate()
        {
            Detach();

            _model.OnDisableAction -= DisableAction;
        }

        public void Activate()
        {
            Attach();

            _model.OnDisableAction += DisableAction;
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

        private void DisableAction(InputAction action, float seconds)
        {
            _component.OnDisableAction(Disable(action, seconds));
        }

        private IEnumerator Disable(InputAction action, float seconds)
        {
            action.Disable();

            yield return new WaitForSeconds(seconds);
            
            action.Enable();
        }
    }
}