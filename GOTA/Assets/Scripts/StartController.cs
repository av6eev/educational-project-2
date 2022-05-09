using Player;
using Player.States.Base;
using UnityEngine;
using Utilities;

public class StartController : MonoBehaviour
{
    [SerializeField] private GlobalContainer _container;
    [SerializeField] private PlayerData _playerData;
    private readonly GameContext _context = new GameContext();

    private readonly ControllerEngine _controllerEngine = new ControllerEngine();
    private readonly SystemEngine _systemEngine = new SystemEngine();
    private readonly StepEngine _stepEngine = new StepEngine();
    private readonly StatesEngine _statesEngine = new StatesEngine();
    
    void Start()
    {
        _context.GlobalContainer = _container;
        _context.PlayerData = _playerData;
        _context.StatesEngine = _statesEngine;

        _stepEngine.Execute(_context, _controllerEngine, _context.GlobalContainer);
        _controllerEngine.Activate();
    }

    void Update()
    {
        _statesEngine.CurrentState.DoLogic();
    }

    private void FixedUpdate()
    {
        _statesEngine.CurrentState.HandleInput();
        _statesEngine.CurrentState.DoPhysics(Time.deltaTime);
    }
}
