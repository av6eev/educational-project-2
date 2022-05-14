using Player.States.Base;
using UnityEngine;
using Utilities;

namespace Player.States
{
    public class LandState : State
    {
        private PlayerModel _model;
        
        private float _passedTime;
        private float _standingTime;

        public LandState(GameContext context, StatesEngine statesEngine) : base(context, statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;

            _model = _context.PlayerModel;
            Type = StatesType.Land;
        }

        public override void Disable()
        {
            _model.Land(false);
        }

        public override void Enable()
        {
            Debug.Log("Land state");

            _passedTime = 0f;
            _standingTime = 0.3f;

           _model.Land(true);
        }

        public override void HandleInput() {}

        public override void DoLogic()
        {
            if (_passedTime > _standingTime)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Idle));
            }

            _passedTime += Time.deltaTime;
        }

        public override void DoPhysics(float deltaTime) {}
    }
}