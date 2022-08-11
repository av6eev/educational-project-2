﻿using Player.StateMachine.Utilities;
using UnityEngine;

namespace Player.StateMachine
{
    public abstract class StateMachine
    {
        private IState _currentState;
        public StateMachineType Type; 

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            
            _currentState = newState;
            _currentState.Enter();;
        }

        public void HandleInput()
        {
            _currentState?.HandleInput();
        }
        
        public void LogicUpdate()
        {
            _currentState?.LogicUpdate();
        }
        
        public void PhysicsUpdate()
        {
            _currentState?.PhysicsUpdate();
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }

        public void OnAnimationEnter()
        {
            _currentState?.OnAnimationEnter();
        }
        
        public void OnAnimationExit()
        {
            _currentState?.OnAnimationExit();
        }
        
        public void OnAnimationTransition()
        {
            _currentState?.OnAnimationTransition();
        }
    }
}