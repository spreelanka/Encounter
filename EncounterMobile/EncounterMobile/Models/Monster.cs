using System;
namespace EncounterMobile.Models
{
	public class Monster
	{
        public string name { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public string group { get; set; }
        public string alignment { get; set; }
        public string armor_class { get; set; }
        public string armor_desc { get; set; }
        public string hit_points { get; set; }
        public string hit_dice { get; set; }
        public string strength { get; set; }
        public string dexterity { get; set; }
        public string constitution { get; set; }
        public string intelligence { get; set; }
        public string wisdom { get; set; }
        public string charisma { get; set; }
        public string strength_save { get; set; }
        public string dexterity_save { get; set; }
        public string constitution_save { get; set; }
        public string intelligence_save { get; set; }
        public string wisdom_save { get; set; }
        public string charisma_save { get; set; }
        public string perception { get; set; }
        public string damage_vulnerabilities { get; set; }
        public string damage_resistances { get; set; }
        public string damage_immunities { get; set; }
        public string condition_immunities { get; set; }
        public string senses { get; set; }
        public string languages { get; set; }
        public string challenge_rating { get; set; }
        public string cr { get; set; }
        

        public Dictionary<string,string> speed { get; set; }

        public List<SpecialAbility> special_abilities { get; set; }
        public List<Action> actions { get; set; }
        public List<Action> reactions { get; set; }

        public class SpecialAbility
        {
            public string name { get; set; }
            public string desc { get; set; }
        }

        public class Action
        {
            public string name { get; set; }
            public string desc { get; set; }
            public string attack_bonus { get; set; }
            public string damage_dice { get; set; }
        }
    }
}

