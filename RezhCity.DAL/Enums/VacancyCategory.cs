using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum VacancyCategory
    {
        [Display(Name = "Бухгалтерия, финансы")]
        Finance,
        [Display(Name = "Менеджмент, управление")]
        Managment,
        [Display(Name = "Гос. служба")]
        Government,
        [Display(Name = "Дизайн, искусство")]
        Design,
        [Display(Name = "ИТ и интернет")]
        IT,
        [Display(Name = "Кадровые службы")]
        HR,
        [Display(Name = "Логистика, склад, закупки")]
        Logistic,
        [Display(Name = "Маркетинг, реклама")]
        Marketing,
        [Display(Name = "Медицина")]
        Medicine,
        [Display(Name = "Образование")]
        Education,
        [Display(Name = "Охрана, безопасность")]
        Security,
        [Display(Name = "Персонал для дома")]
        HomeStaff,
        [Display(Name = "Продажи, торговля")]
        Trade,
        [Display(Name = "Промышленность")]
        Industry,
        [Display(Name = "Работа для молодежи")]
        Young,
        [Display(Name = "Рабочий персонал")]
        WorkingStaff,
        [Display(Name = "Кафе, столовые")]
        Cafe,
        [Display(Name = "Сельское хозяйство")]
        Agriculture,
        [Display(Name = "Спорт")]
        Sport,
        [Display(Name = "Страхование")]
        Insurance,
        [Display(Name = "Строительство")]
        Building,
        [Display(Name = "Сфера услуг")]
        Service,
        [Display(Name = "Транспорт")]
        Transport,
        [Display(Name = "Юриспруденция")]
        Jurisprudence,
        [Display(Name = "Прочее")]
        Other
    }
}
