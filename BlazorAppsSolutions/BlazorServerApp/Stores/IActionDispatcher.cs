
namespace BlazorServerApp.Stores
{
    public interface IActionDispatcher
    {
        void Dispatch(IAction action);
        void Subscript(Action<IAction> action);
        void Unsubscript(Action<IAction> action);
    }
}