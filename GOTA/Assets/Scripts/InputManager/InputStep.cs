using Utilities;

namespace InputManager
{
    public class InputStep : IStep
    {
        public void Execute(GameContext context, ControllerEngine engine, GlobalContainer container)
        {
            var model = new InputModel();
            context.InputModel = model;
            
            var controller = new InputController(context, model, container.PlayerInput, container.InputComponent);
            engine.Add(controller);
            controller.Activate();
        }
    }
}