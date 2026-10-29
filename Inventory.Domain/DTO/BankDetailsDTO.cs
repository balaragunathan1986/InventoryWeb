using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO
{
    public class BankDetailsDTO
    {
        [Key]
        public int Id { get; set; }
        public Guid Bank_Id { get; set; }
        public string? Bank_Name { get; set; }
        public string? Branch_Addresss { get; set; }
        public string? Account_Holder_Name { get; set; }
        public string? Account_Number { get; set; }
        public string? Ifsc_Code { get; set; }
        public string? Micr_Code { get; set; }
        public string? Phone_Number { get; set; }
        public string? Email { get; set; }
        public Guid Reference_Id { get; set; }

    }
}
