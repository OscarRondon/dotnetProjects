namespace BlazorServerApp.Stores.Counter2Store
{
    public class CounterState
    {
        public int Count { get; }

        public CounterState(int count)
        {
            Count = count;
        }
    }
    public class CounterStore
    {
        private CounterState _state;
        private readonly IActionDispatcher _actionDispatcher;

        public CounterStore(IActionDispatcher actionDispatcher)
        {
            _state = new CounterState(0);
            _actionDispatcher = actionDispatcher;
            _actionDispatcher.Subscript(HandleActions);
        }

        ~CounterStore()
        {
            _actionDispatcher.Unsubscript(HandleActions);
        }

        private void HandleActions(IAction action)
        {
            switch (action.Name)
            {
                case IncrementAction.INCREMENT:
                    IncrementCount();
                    break;
                case DecrementAction.DECREMENT:
                    DecrementCount();
                    break;
                default:
                    break;
            }
        }

        public CounterState GetState()
        {
            return _state;
        }

        private void IncrementCount()
        {
            var count = this._state.Count;
            count++;
            this._state = new CounterState(count);
            BroadcastStateChange();
        }

        private void DecrementCount()
        {
            var count = this._state.Count;
            count--;
            this._state = new CounterState(count);
            BroadcastStateChange();
        }

        #region Observer Pattern

        private Action _listener;

        public void AddStateChangeListeners(Action listener)
        {
            _listener += listener;
        }

        public void RemoveStateChangeListeners(Action listener)
        {
            _listener -= listener;
        }

        private void BroadcastStateChange()
        {
            _listener.Invoke();
        }

        #endregion
    }
}
