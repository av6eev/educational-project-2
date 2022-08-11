using UnityEngine;

namespace Player.StateMachine.Utilities
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void HandleInput();
        public void LogicUpdate();
        public void PhysicsUpdate();
        public void OnAnimationEnter();
        public void OnAnimationExit();
        public void OnAnimationTransition();
    }
}