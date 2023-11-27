namespace BlazorServerApp.Stores.Counter2Store
{
    public class IncrementAction: IAction
    {
        public const string INCREMENT = "INCREMENT";
        public int Count { get; set; }

        public string Name => INCREMENT;
    }
}
