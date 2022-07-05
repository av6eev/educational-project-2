using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States
{
    public class RunningState : MovementState
    {
        public RunningState(MovementStateMachine movementStateMachine, GameContext context) : base(movementStateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Context.PlayerData.SpeedModifier = 1f;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (Context.PlayerModel.IsButtonToggled)
            {
                case false when !Context.PlayerModel.IsRunEnable:
                    MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);
                    break;
                case true when !Context.PlayerModel.IsRunEnable:
                    MovementStateMachine.ChangeState(MovementStateMachine.WalkingState);
                    break;
                case false:
                    MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);
                    Debug.Log("test");
                    break;
                default: return;
            }
        }
    }
}