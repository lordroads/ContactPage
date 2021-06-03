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
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column(TypeName ="nvarchar(25)")]
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

        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "Длина строки должна быть от 15 до 100 символов")]
        [DisplayName("Почта")]
        [Required(ErrorMessage = "Почта обязательное поле для ввода.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Mail { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 150 символов")]
        [DisplayName("Компания")]
        [Required(ErrorMessage = "Название компании обязательное поле для ввода.")]
        public string Company { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 250 символов")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 200 символов")]
        [DisplayName("Сфера деятельности")]
        [Required(ErrorMessage = "Сфера деятельности обязательное поле для ввода.")]
        public string FieldOfActivity { get; set; }

        [DisplayName("Завершенные проекты")]
        [Column(TypeName = "nvarchar(1000)")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 1000 символов")]
        public string CompletedProjects { get; set; }
    }
}
