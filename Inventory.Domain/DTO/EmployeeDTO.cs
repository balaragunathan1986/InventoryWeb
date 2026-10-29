using Inventory.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO
{
    [Table("Employee")]
    public class EmployeeDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Employee_Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? DisplayName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }
        public string? MobileNumber { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string? Landmark { get; set; }

        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int? EmployeeStatusId { get; set; }

        public DateTime? JoiningDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }

        public string? ProfileImagePath { get; set; }
        public string? Remarks { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
