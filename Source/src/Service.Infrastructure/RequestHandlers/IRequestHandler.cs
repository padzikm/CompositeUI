using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompositeUI.Service.Infrastructure
{
    public interface IRequestHandler
    {
        Task HandleRequest();

        Task HandleRequest(Dictionary<string,object> parameters);

        Task HandleRequest(IEnumerable<string> uiKeys);

        Task HandleRequest(IEnumerable<string> uiKeys, Dictionary<string,object> parameters);
        
        Task<IEnumerable<IViewModel>> GenerateViewModels(IEnumerable<string> uiKeys);

        Task<IEnumerable<IViewModel>> GenerateViewModels(IEnumerable<string> uiKeys, Dictionary<string, object> parameters);

        Task<IEnumerable<IViewModel>> GenerateViewModelsOnInvalidModelState(IEnumerable<string> uiKeys);

        Task<IEnumerable<IViewModel>> GenerateViewModelsOnInvalidModelState(IEnumerable<string> uiKeys, Dictionary<string,object> parameters);
    }
}
