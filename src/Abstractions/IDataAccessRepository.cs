using System.Collections.Generic;
using System.Threading.Tasks;
using RruleTool.Models;

namespace RruleTool.Abstractions
{
    public interface IDataAccessRepository
    {
        // This is async operation because we assume that this operation is for reading data from some database server
        Task<IReadOnlyList<AvailabilityDataModel>> GetAvailabilitiesAsync();
    }
}
