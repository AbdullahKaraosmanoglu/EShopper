using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EShopper.Models
{
    [Bind(Include = "UserId,Name,Surname,Address,Email,Password,DateOfBirth,Gender ")]
    public class UsersModel
    {

        [Display(Name = "UserId")]
        [Required(ErrorMessage = " ID alanını doldurmak zorundasınız")]
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = " İsim alanını doldurmak zorundasınız")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Soyisim alanını doldurmak zorundasınız")]
        public string Surname { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Adress alanını doldurmak zorundasınız")]
        public string Address { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Geçersiz E-mail Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password alanını doldurmak zorundasınız")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Display(Name = "DateOfBirth")]
        [Required(ErrorMessage = "Doğum tarihi alanını doldurmak zorundasınız")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public bool? Gender { get; set; }
        public string GenderTypeText { get; set; }

        public List<UsersModel> UserList { get; set; }
    }
}