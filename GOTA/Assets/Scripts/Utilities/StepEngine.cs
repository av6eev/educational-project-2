using System.Collections.Generic;
using InputManager;
using Player;

namespace Utilities
{
    public class StepEngine : IStep
    {
        private readonly List<IStep> _steps = new List<IStep>();

        public StepEngine()
        {
            Add(new InputStep());
            Add(new PlayerStep());
        }
        
        public void Execute(GameContext context, ControllerEngine engine, GlobalContainer container)
        {
            foreach (var step in _steps)
            {
                step.Execute(context, engine, container);
            }
        }

        private void Add(IStep step)
        {
            _steps.Add(step);
        }

        public void Clear()
        {
            _steps.Clear();
        }
    }
}