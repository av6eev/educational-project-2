using Player.StateMachine.Movement.States;
using Player.StateMachine.Utilities;
using Utilities;

namespace Player.StateMachine.Movement
{
    public class MovementStateMachine : StateMachine
    {
        public StateMachineEngine StateMachineEngine { get; }
        public IdlingState IdlingState { get; }
        public RunningState RunningState { get; }
        public WalkingState WalkingState { get; }
        public SprintingState SprintingState { get; }

        public MovementStateMachine(StateMachineEngine stateMachineEngine, GameContext context)
        {
            Type = StateMachineType.Movement;
            StateMachineEngine = stateMachineEngine;
            
            IdlingState = new IdlingState(this, context);
            RunningState = new RunningState(this, context);
            WalkingState = new WalkingState(this, context);
            SprintingState = new SprintingState(this, context);
        }
    }
}