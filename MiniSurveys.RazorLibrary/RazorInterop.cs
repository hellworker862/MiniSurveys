using Microsoft.JSInterop;

namespace MiniSurveys.RazorLibrary
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class RazorInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public RazorInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/MiniSurveys.RazorLibrary/razorLibrary.js").AsTask());
        }

        public async ValueTask<string> ShowToast(string message, string title, string type = "success")
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("showToast", message, title, type);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}