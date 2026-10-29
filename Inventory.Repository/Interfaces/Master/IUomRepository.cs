using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Master
{
    public interface IUomRepository
    {
        Task<List<UomDTO>> GetUom();
        Task<UomDTO> GetUom(Guid Uom_Id);
        Task<UomDTO> Save(UomDTO Uom);
        Task<UomDTO> Update(UomDTO Uom);
        Task<bool> Delete(Guid Uom_Id);
    }
}
