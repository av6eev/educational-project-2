using Player.Data.States;
using Player.StateMachine.Movement.States.Grounded;
using Player.StateMachine.Movement.States.Moving;
using Player.StateMachine.Movement.States.Stopping;
using Player.StateMachine.Utilities;
using Utilities;

namespace Player.StateMachine.Movement
{
    public class MovementStateMachine : StateMachine
    {
        public StateMachineEngine StateMachineEngine { get; }
        public StateReusableData ReusableData { get; }
        
        public IdlingState IdlingState { get; }
        public RunningState RunningState { get; }
        public WalkingState WalkingState { get; }
        public DashingState DashingState { get; }
        
        public LightStoppingState LightStoppingState { get; }
        public MediumStoppingState MediumStoppingState { get; }
        public HardStoppingState HardStoppingState { get; }

        public MovementStateMachine(StateMachineEngine stateMachineEngine, GameContext context)
        {
            Type = StateMachineType.Movement;
            StateMachineEngine = stateMachineEngine;
            
            ReusableData = new StateReusableData();
            
            IdlingState = new IdlingState(this, context);
            RunningState = new RunningState(this, context);
            WalkingState = new WalkingState(this, context);
            DashingState = new DashingState(this, context);

            LightStoppingState = new LightStoppingState(this, context);
            MediumStoppingState = new MediumStoppingState(this, context);
            HardStoppingState = new HardStoppingState(this, context);
        }
    }
}