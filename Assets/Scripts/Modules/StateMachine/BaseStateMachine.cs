using UnityEngine;

namespace Modules.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        private BaseState _currentState;

        public void SwitchState(BaseState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }

        private void Update()
        {
            _currentState?.Tick();
        }
    }
}
