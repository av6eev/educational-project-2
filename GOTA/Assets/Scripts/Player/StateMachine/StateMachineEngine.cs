using System.Collections.Generic;
using Player.StateMachine.Movement;
using Player.StateMachine.Utilities;
using Utilities;

namespace Player.StateMachine
{
    public class StateMachineEngine
    {
        private readonly GameContext _context;
        private readonly Dictionary<StateMachineType, StateMachine> _stateMachines = new Dictionary<StateMachineType, StateMachine>();

        private MovementStateMachine _movementStateMachine;

        public StateMachineEngine(GameContext context)
        {
            _context = context;
            
            Add(_movementStateMachine = new MovementStateMachine(this, _context));
            
            _movementStateMachine.ChangeState(_movementStateMachine.IdlingState);
        }

        private void Add(StateMachine stateMachine)
        {
            _stateMachines.Add(stateMachine.Type, stateMachine);       
        }
        
        public StateMachine GetStateMachine(StateMachineType type)
        {
            return _stateMachines[type];
        }
    }
}