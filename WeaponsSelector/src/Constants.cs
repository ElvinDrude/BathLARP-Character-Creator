using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponsForm
{
    static class Constants
    {
        //Note: These constants are used as both a name in the JSON file and as a display string to the user
        public const string Weapons = "Weapons";
        public const string Armour = "Armour";
        public const string PhysicalMental = "Physical/Mental"; 
        public const string Medical = "Medical"; 
        public const string Magic = "Magic";

        //This one isn't displayed to the user but is in the JSON file
        public const string Special = "Special";

        //Constants below here are not used in JSON, only as user display strings.
        public const string Spells = "Spells";
        public const string Miracles = "Miracles";
    }
}
