using CameraManager;
using InputManager;
using Player;
using Player.StateMachine;
using ScriptableObjects;

namespace Utilities
{
    public class GameContext
    {
        public GlobalContainer GlobalContainer { get; set; }

        public InputModel InputModel { get; set; }
        public PlayerModel PlayerModel { get; set; }
        
        public PlayerData PlayerData { get; set; }
        public CameraData CameraData { get; set; }

        public StateMachineEngine StateMachineEngine { get; set; }
    }
}