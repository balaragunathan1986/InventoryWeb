using Inventory.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO
{
    [Table("Department")]
    public class DepartmentDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Department_Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? DepartmentCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
