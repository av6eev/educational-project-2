using System.Collections.Generic;

namespace Player.States.Base
{
    public class StatesEngine
    {
        public State CurrentState { get; private set; }
        private readonly Dictionary<StatesType, State> _states = new Dictionary<StatesType, State>();

        public void Init(State defaultState)
        {
            SetState(defaultState);
            defaultState.Enable();
        }

        public void Change(State newState)
        {
            CurrentState.Disable();
            SetState(newState);
            newState.Enable();
        }

        private void SetState(State newState)
        {
            CurrentState = newState;
        }

        public void Add(State state)
        {
            _states.Add(state.Type, state);
        }

        public State Get(StatesType type)
        {
            return _states[type];
        }
    }
}