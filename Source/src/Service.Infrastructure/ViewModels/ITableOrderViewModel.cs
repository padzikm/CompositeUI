using System.Collections.Generic;

namespace CompositeUI.Service.Infrastructure
{
    public interface ITableOrderViewModel : IViewModel
    {
        List<ServicePublicData> SortedIds { get; }
    }
}
