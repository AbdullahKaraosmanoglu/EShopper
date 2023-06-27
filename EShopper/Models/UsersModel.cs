using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace EShopper.Models
{
    public class UsersModel
    {
        /// <summary>
        /// Prop'ların oluşturulması.
        /// </summary>
        [Display(Name = "UserId")]
        [Required(ErrorMessage = " ID alanını doldurmak zorundasınız")]
        public int UserId { get; set; }

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
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        //[Required(ErrorMessage = "Cinsiyet alanını doldurmak zorundasınız")]
        public int? Gender { get; set; }
    }
}