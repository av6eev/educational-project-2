using Player.Data.States;
using Player.StateMachine.Movement.States.Airborne;
using Player.StateMachine.Movement.States.Grounded;
using Player.StateMachine.Movement.States.Grounded.Landing;
using Player.StateMachine.Movement.States.Grounded.Moving;
using Player.StateMachine.Movement.States.Grounded.Stopping;
using Player.StateMachine.Utilities;
using Utilities;

namespace Player.StateMachine.Movement
{
    public class MovementStateMachine : StateMachine
    {
        public StateReusableData ReusableData { get; }
        
        public IdlingState IdlingState { get; }
        public RunningState RunningState { get; }
        public WalkingState WalkingState { get; }
        public DashingState DashingState { get; }
        
        public LightStoppingState LightStoppingState { get; }
        public MediumStoppingState MediumStoppingState { get; }
        public HardStoppingState HardStoppingState { get; }
        
        public JumpingState JumpingState { get; }
        public FallingState FallingState { get; }
        
        public LightLandingState LightLandingState { get; }
        public HardLandingState HardLandingState { get; }
        public RollingState RollingState { get; }

        public MovementStateMachine(StateMachineEngine stateMachineEngine, GameContext context)
        {
            Type = StateMachineType.Movement;

            ReusableData = new StateReusableData();
            
            IdlingState = new IdlingState(this, context);
            RunningState = new RunningState(this, context);
            WalkingState = new WalkingState(this, context);
            DashingState = new DashingState(this, context);

            LightStoppingState = new LightStoppingState(this, context);
            MediumStoppingState = new MediumStoppingState(this, context);
            HardStoppingState = new HardStoppingState(this, context);

            JumpingState = new JumpingState(this, context);
            FallingState = new FallingState(this, context);
            
            LightLandingState = new LightLandingState(this, context);
            HardLandingState = new HardLandingState(this, context);
            RollingState = new RollingState(this, context);
        }
    }
}