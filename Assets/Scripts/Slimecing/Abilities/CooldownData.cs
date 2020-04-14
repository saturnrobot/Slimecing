namespace Slimecing.Abilities
{
    [System.Serializable]
    public class CooldownData 
    {
        private Ability ability;
        private float cooldown;
        private int useAmount = 0;
        public Ability GetAbility => ability;
        public int UseAmount => useAmount;
        public bool IsCoolin => cooldown > 0 || useAmount > 0; //? true : false;

        public CooldownData(Ability ability, float cooldown)
        {
            this.ability = ability;
            this.cooldown = cooldown;
        }

        public CooldownData (Ability ability, float cooldown, int useAmount)
        {
            this.ability = ability;
            this.cooldown = cooldown;
            this.useAmount = useAmount - 1;
        }
        
        public void TickCooldown(float delta)
        {
            if (useAmount > 0) return;
            cooldown -= delta;

        }
        public void SubUseAmount()
        {
            useAmount -= 1;
        }
    }
}
