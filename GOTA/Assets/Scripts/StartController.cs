using Player.StateMachine;
using Player.StateMachine.Utilities;
using ScriptableObjects;
using UnityEngine;
using Utilities;

public class StartController : MonoBehaviour
{
    [SerializeField] private GlobalContainer _container;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private CameraData _cameraData;
    private readonly GameContext _context = new GameContext();

    private readonly ControllerEngine _controllerEngine = new ControllerEngine();
    private readonly SystemEngine _systemEngine = new SystemEngine();
    private readonly StepEngine _stepEngine = new StepEngine();
    private StateMachineEngine _stateMachineEngine;
    
    void Start()
    {
        _context.GlobalContainer = _container;
        _context.PlayerData = _playerData;
        _context.CameraData = _cameraData;
        
        _stepEngine.Execute(_context, _controllerEngine, _context.GlobalContainer);
        _controllerEngine.Activate();
        
        _stateMachineEngine = new StateMachineEngine(_context);
        _context.StateMachineEngine = _stateMachineEngine;
    }

    void Update()
    {
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).HandleInput();
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).LogicUpdate();
    }

    private void FixedUpdate()
    {
        _systemEngine.Update(Time.deltaTime);
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).PhysicsUpdate();
    }
}
