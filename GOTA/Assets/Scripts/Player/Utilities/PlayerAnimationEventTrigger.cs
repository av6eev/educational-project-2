using UnityEngine;

namespace Player.Utilities
{
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        [SerializeField] private StartController _controller;
        [SerializeField] private PlayerView _view;
        
        public void OnAnimationEnterEventTrigger()
        {
            if (IsAnimationTransition()) return;
            
            _controller.OnAnimationEnterEvent();
        }
        
        public void OnAnimationExitEventTrigger()
        {
            if (IsAnimationTransition()) return;

            _controller.OnAnimationExitEvent();
        }
        
        public void OnAnimationTransitionEventTrigger()
        {
            if (IsAnimationTransition()) return;

            _controller.OnAnimationTransitionEvent();
        }

        private bool IsAnimationTransition(int layerIndex = 0)
        {
            return _view.Animator.IsInTransition(layerIndex);
        }
    }
}