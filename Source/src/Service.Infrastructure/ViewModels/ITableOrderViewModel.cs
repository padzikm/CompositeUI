using System.Collections.Generic;
using CompositeUI.Service.Infrastructure.Models;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    public interface ITableOrderViewModel : IViewModel
    {
        List<ServicePublicData> SortedIds { get; }
    }
}
