using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Service.Infrastructure.ViewModels;

namespace HomeManager.Service.Infrastructure.RequestHandlers
{
    public interface IRequestHandler
    {
        Task HandleRequest();

        Task HandleRequest(IEnumerable<string> uiKeys);

        Task<IEnumerable<IViewModel>> GenerateViewModels(IEnumerable<string> uiKeys);

        Task<IEnumerable<IViewModel>> GenerateViewModelsOnInvalidModelState(IEnumerable<string> uiKeys);
    }
}
