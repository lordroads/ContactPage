using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactPage.Models
{
    public enum TypesItem
    {
        [Display(Name = "Гаджеты")]
        Gadgets,
        [Display(Name = "Мультимедиа")]
        Multimedia,
        [Display(Name = "Компьютеры")]
        Computers,
        [Display(Name = "Офисное оборудование")]
        OfficeEquipment,
        [Display(Name = "Сеть")]
        Network
    }
}
