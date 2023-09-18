using CameraManager;
using Player.Data.ScriptableObjects;
using Player.StateMachine;
using Player.StateMachine.Utilities;
using UnityEngine;
using Utilities;

public class StartController : MonoBehaviour
{
    [SerializeField] private GlobalContainer _container;
    [SerializeField] private CameraData _cameraData;
    [SerializeField] private PlayerSO _playerSO;
    
    private readonly GameContext _context = new GameContext();
    private readonly ControllerEngine _controllerEngine = new ControllerEngine();
    private readonly SystemEngine _systemEngine = new SystemEngine();
    private readonly StepEngine _stepEngine = new StepEngine();
    private StateMachineEngine _stateMachineEngine;
    
    private void Start()
    {
        _context.GlobalContainer = _container;
        _context.CameraData = _cameraData;
        _context.PlayerSO = _playerSO;
        
        _stepEngine.Execute(_context, _controllerEngine, _context.GlobalContainer);

        _stateMachineEngine = new StateMachineEngine(_context);
        _context.StateMachineEngine = _stateMachineEngine;
        
        _controllerEngine.Activate();
    }

    private void Update()
    {
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).HandleInput();
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).LogicUpdate();
    }

    private void FixedUpdate()
    {
        _systemEngine.Update(Time.deltaTime);
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).PhysicsUpdate();
    }
    
    public void OnAnimationEnterEvent()
    {
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).OnAnimationEnter();
    }
    
    public void OnAnimationExitEvent()
    {
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).OnAnimationExit();
    }
    
    public void OnAnimationTransitionEvent()
    {
        _stateMachineEngine.GetStateMachine(StateMachineType.Movement).OnAnimationTransition();
    }
}
