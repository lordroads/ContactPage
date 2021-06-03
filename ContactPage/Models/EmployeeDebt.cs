using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactPage.Models
{
    public class EmployeeDebt
    {
        [Key]
        public int EmployeeDebtId { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Имя обязательное поле для ввода.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 25 символов")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("Телефон")]
        [Required(ErrorMessage = "Телефон обязательное поле для ввода.")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Некорректный телефон (Пример: 9991234567).")]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 150 символов")]
        [DisplayName("Оборудование")]
        [Required(ErrorMessage = "Название компании обязательное поле для ввода.")]
        public string Equipment { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 250 символов")]
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
