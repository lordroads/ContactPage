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
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [DisplayName("Внутриний номер")]
        public Guid GuidId { get; set; }

        [DisplayName("Тип оборудования")]
        public TypesItem TypeItem { get; set; } = TypesItem.OfficeEquipment;

        [DisplayName("Наименование")]
        [Required(ErrorMessage = "Наименование обязательное поле для ввода.")]
        public string Name { get; set; }

        [DisplayName("Компания")]
        [Required(ErrorMessage = "Компания обязательное поле для ввода.")]
        public string Company { get; set; }

        [DisplayName("Серия")]
        [Required(ErrorMessage = "Серия обязательное поле для ввода.")]
        public string Serial { get; set; }

        [DisplayName("Заголовок описания")]
        public string NameDescriptionInfo { get; set; }
        [DisplayName("Описание")]
        public string DescriptionInfo { get; set; }
        [DisplayName("Заголово дополнительный №1")]
        public string NameDescriptionProgramm { get; set; }
        [DisplayName("Описание дополнительное №1")]
        public string DescriptionProgramm { get; set; }
        [DisplayName("Заголово дополнительный №2")]
        public string NameDescriptionLicense { get; set; }
        [DisplayName("Описание дополнительное №2")]
        public string DescriptionLicense { get; set; }
        [DisplayName("Закрепленный сотрудник")]
        public string AssignedEmployee { get; set; }
        [DisplayName("Где находится")]
        public string WhereIs { get; set; }
    }
}
