using InputManager;
using Player;
using Player.States;
using Player.States.Base;

namespace Utilities
{
    public class GameContext
    {
        public GlobalContainer GlobalContainer { get; set; }

        public StatesEngine StatesEngine { get; set; }

        public InputModel InputModel { get; set; }
        public PlayerModel PlayerModel { get; set; }
        public PlayerData PlayerData { get; set; }
    }
}