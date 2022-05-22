using Utilities;

namespace Player.States.Base
{
    public class StatesStep : IStep
    {
        private GameContext _context;
        private StatesEngine _statesEngine;

        public void Execute(GameContext context, ControllerEngine engine, GlobalContainer container)
        {
            _context = context;
            _statesEngine = _context.StatesEngine;

            var idleState = new IdleState(_context, _statesEngine);
            _statesEngine.Init(idleState);
            
            _statesEngine.Add(idleState);
            _statesEngine.Add(new WalkState(_context, _statesEngine));
        }
    }
}