namespace BlazorServerApp.Stores
{
    public class ActionDispatcher : IActionDispatcher
    {
        private Action<IAction> _registeredActionHandlers;

        public void Subscript(Action<IAction> action) => _registeredActionHandlers += action;

        public void Unsubscript(Action<IAction> action) => _registeredActionHandlers -= action;

        public void Dispatch(IAction action) => _registeredActionHandlers?.Invoke(action);
    }
}
