namespace Utilities
{
    public interface IStep
    {
        void Execute(GameContext context, ControllerEngine engine, GlobalContainer container);
    }
}