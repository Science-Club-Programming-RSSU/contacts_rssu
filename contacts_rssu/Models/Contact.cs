using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace contacts_rssu.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }
        [Column("Name")]
        [Display(Name="Имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина поля \"Имя\" должна быть от 3 до 50 символов")]
        [Required(ErrorMessage = "Полe \"Имя\" должно быть заполнено")]
        public string Name { get; set; }
        [Column("Phone")]
        [Display(Name="Телефон")]
        [StringLength(14, MinimumLength = 6, ErrorMessage = "Длина поля \"Телефон\" должна быть от 6 до 14 символов")]
        [Required(ErrorMessage = "Полe \"Телефон\" должно быть заполнено")]
        public string Phone { get; set; }
        [Column("Email")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Длина поля \"Почта\" должна быть от 6 до 30 символов")]
        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Полe \"Почта\" должно быть заполнено")]
        public string Email { get; set; }
    }
}
