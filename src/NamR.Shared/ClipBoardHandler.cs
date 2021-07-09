using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace NamR.Shared
{
    public class ClipBoardHandler
    {
        private readonly IJSRuntime _runtime;

        public ClipBoardHandler(IJSRuntime jSRunTime)
        {
            _runtime = jSRunTime;
        }

        public ValueTask<string> WriteTextAsync(string text)
        {
            return _runtime.InvokeAsync<string>($"navigator.clipboard.writeText", text);
        }
    }
}
