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
            _model.OnMove -= Move;
        }
        
        public void Activate()
        {
            _model.OnMove += Move;
        }

        private void Move()
        {
            _view.Move();
        }
    }
}