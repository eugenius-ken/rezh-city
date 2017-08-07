using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum GarageType
    {
        [Description("Не указано")]
        NoGarageType,
        [Description("Железобетонный")]
        Ferroconcrete,
        [Description("Кирпичный")]
        Brick,
        [Description("Металлический")]
        Metal
    }
}
