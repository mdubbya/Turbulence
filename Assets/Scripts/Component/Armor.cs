using System;

namespace Component
{
    [Serializable]
    public class Armor : Component
    {
        private float _damageProtection;
        public float damageProtection
        {
            get
            {
                return _damageProtection;
            }
        }

        private ArmorType _armorType;
        public ArmorType armorType
        {
            get
            {
                return _armorType;
            }
        }

        private ComponentSize _armorSize;
        public ComponentSize armorSize
        {
            get
            {
                return _armorSize;
            }
        }
    }
}
