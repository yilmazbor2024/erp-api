using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("hrEmployeeSGKBorrowing")]
    public partial class hrEmployeeSGKBorrowing
    {
        public hrEmployeeSGKBorrowing()
        {
        }

        [Key]
        [Required]
        public Guid EmployeeSGKBorrowingID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public DateTime OrganizedDate { get; set; }

        [Required]
        public byte SGKBorrowingTypeCode { get; set; }

        [Required]
        public decimal BorrowingAmount { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        public virtual hrEmployeePayrollProfile hrEmployeePayrollProfile { get; set; }
        public virtual cdSGKBorrowingType cdSGKBorrowingType { get; set; }

    }
}
