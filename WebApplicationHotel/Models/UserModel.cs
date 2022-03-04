using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationHotel.Models
{
    public class UserModel
    {

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Tegemist pole emaili aadressiga. Palun kontrolli üle")]
        //[DataType(DataType.EmailAddress, ErrorMessage ="Tegemist pole emaili aadressiga. Palun kontrolli üle")]
        [Required(ErrorMessage ="E-maili aadress on kohustuslik")]
        public string EmailAddress { get; set; }
        [Display(Name ="Emaili kinnitus")]
        [Compare("EmailAddress",ErrorMessage ="Email ja Emaili kinnitus ei klapi")]
        public string ConfirmEmail { get; set; }
        [Display(Name ="Parool")]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength =8,ErrorMessage ="Parool peab olema vähemalt 8 tähemärki pikk ja kuni 20 kohta")]
        public string Password { get; set; }
        [Display (Name ="Parooli kinnitamine")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Paroolid ei klapi")]
        public string  ConfirmPassword { get; set; }
    }
}