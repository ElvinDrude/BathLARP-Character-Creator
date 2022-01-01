using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponsForm;
using WeaponsForm.character.record;

namespace WeaponsSelector.src.character.record
{
    /// <summary>
    /// Factory to create a SkillRecord instance for a given 
    /// </summary>
    class SkillRecordFactory
    {
        //TODO: This whole thing might not be terribly useful - ended up implementing parts of this
        // logic inside the *RowControls classes...

        /// <summary>
        /// Create a SkillReocrd. This will usually be a specific subtype of SkillRecord, as each category needs slightly
        /// different handling
        /// </summary>
        /// <param name="cost">The cost, in character points, of this record</param>
        /// <param name="controlType">Determines the type of SkillRecord that will be created</param>
        /// <param name="displayString">The string to display in the output area</param>
        /// <returns></returns>
        public static SkillRecord createSkillRecord(long cost, AbstractRowControls controlType, string displayString)
        {
            switch (controlType)
            {
                case SpellsRowControls _:
                    return new CastingRecord(cost, displayString);

            }

            throw new Exception("Unrecognised skill, could not create record");
        }

    }
}
