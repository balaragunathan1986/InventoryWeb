using Inventory.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO
{
    [Table("Designation")]
    public class DesignationDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Designation_Id { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public string? DesignationCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
