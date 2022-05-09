using Utilities;

namespace Player
{
    public class PlayerStep : IStep
    {
        public void Execute(GameContext context, ControllerEngine engine, GlobalContainer container)
        {
            var model = new PlayerModel();
            context.PlayerModel = model;

            var controller = new PlayerController(context, model, container.PlayerView);
            engine.Add(controller);
        }
    }
}