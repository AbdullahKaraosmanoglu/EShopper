using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace EShopper.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersModel
    {
        /// <summary>
        /// Properties'lerin oluşturulması.
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
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase      Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }

        [Display(Name = "DateOfBirth")]
        [Required(ErrorMessage = "Doğum tarihi alanını doldurmak zorundasınız")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Cinsiyet alanını doldurmak zorundasınız")]
        public int Gender { get; set; }
    }
}