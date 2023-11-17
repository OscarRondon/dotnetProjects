using Microsoft.AspNetCore.Components;

namespace BlazorServerApp.BaseComponents
{
    public class DateBaseComponent: ComponentBase
    {
        public DateTime currentDate { get; set; } = DateTime.Now;

        public DateBaseComponent()
        {
            
        }
    }
}
