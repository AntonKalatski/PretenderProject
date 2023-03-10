namespace Modules.StateMachine
{
    public abstract class BaseState
    {
        public abstract void Enter();
        public abstract void Tick();
        public abstract void Exit();
    }
}