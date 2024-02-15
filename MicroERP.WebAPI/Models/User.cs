using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroERP.Models
{

    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(200)")]
        public string? FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string? SupervisorId { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(200)")]
        public string? LastName { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? UserPassword { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(200)")]
        public string? Name { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string? FatherName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string? CNIC { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string? Address { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string? Designation { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string? MSCardNo { get; set; }
        [PersonalData]
        [Column(TypeName = "int")]
        public int? RoleId { get; set; }
        [PersonalData]
        [Column (TypeName = "nvarchar(256)")]
        public override string? UserName { get; set; }
        [PersonalData]
        [Column (TypeName = "int")]
        public int? ClientId { get; set; }
        [PersonalData]
        [Column (TypeName = "int")]
        public int ModuleId { get; set; }
    }


}
