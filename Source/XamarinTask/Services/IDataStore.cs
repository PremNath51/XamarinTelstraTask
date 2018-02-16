using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinTask
{
    #region Interface Method Decelration
    public interface IDataStore<T>
    {
        Task<T> GetItemsAsync(bool forceRefresh = false);
        Task<T> GetItemsAsync(string SortOrder);
    }
    #endregion
}
