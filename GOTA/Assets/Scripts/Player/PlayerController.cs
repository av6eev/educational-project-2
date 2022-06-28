using Utilities;

namespace Player
{
    public class PlayerController : IController
    {
        private readonly GameContext _context;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public PlayerController(GameContext context, PlayerModel model, PlayerView view)
        {
            _context = context;
            _model = model;
            _view = view;
        }

        public void Deactivate()
        {
            _model.OnWalk -= Walk;
            _model.OnIdle -= Idle;
        }

        public void Activate()
        {
            _model.OnWalk += Walk;
            _model.OnIdle += Idle;
        }

        private void Idle(float speed)
        {
            _view.Idle(speed);
        }

        private void Walk(bool isEnable)
        {
            _view.Walk(isEnable);
        }
    }
}