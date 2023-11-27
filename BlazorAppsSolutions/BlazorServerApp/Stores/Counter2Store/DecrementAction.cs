namespace BlazorServerApp.Stores.Counter2Store
{
    public class DecrementAction: IAction
    {
        public const string DECREMENT = "DECREMENT";
        public int Count { get; set; }

        public string Name => DECREMENT;
    }
}
