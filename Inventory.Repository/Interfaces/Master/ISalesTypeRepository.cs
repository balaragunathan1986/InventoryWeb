using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Master
{
    public interface ISalesTypeRepository
    {
        Task<List<SalesTypeDTO>> GetSalesType();
        Task<SalesTypeDTO> GetSalesType(Guid SalesType_Id);
        Task<SalesTypeDTO> Save(SalesTypeDTO SalesType);
        Task<SalesTypeDTO> Update(SalesTypeDTO SalesType);
        Task<bool> Delete(Guid SalesType_Id);
    }
}
