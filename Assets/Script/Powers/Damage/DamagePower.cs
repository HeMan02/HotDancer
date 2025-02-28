public class DamagePower : IDamage, IEntity
{
    private int count;
    public string NamePower => "DamageExtra";
    public int EffectValue { get => 40 * Count; }
    public float Probability { get => 40 * Count; }
    public int Time { get => 40 * Count; }
    public int EffectValueStart { get => 40 * Count; }
    public int EffectValuePower { get => 40 * Count; }  // Da cambiare gli altri parametri 
    public int Count { get => count; set { count = value; Init(); } } // ANDRE SUGGERIMENTO CON INIT
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public DamagePower()
    {
        Count = 1;
    }


    public void Init()
    {
        SetValuesStandard();
    }

    private void SetValuesStandard()
    {
        if (powerInfo is null)
        {
            powerInfo = new PowerInfo<IEntity>();
            powerInfo.Entity = this;
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.Damage;
            powerInfo.Name = typeof(DamagePower).FullName;
            PowersManager.Instance.RegisterPowerInterface<IDamage>(powerInfo);
        }

        Mediator.Instance.SetAction(powerInfo.Entity.EffectValueStart, IEntity.TypeEvents.Damage);
    }

    public void DoDamage(int value)
    {
        throw new System.NotImplementedException();
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
        powerInfo = new PowerInfo<IEntity>();
        powerInfo.Entity = this;
        powerInfo.Entity.TypePowers = IEntity.TypeEvents.Damage;
        powerInfo.Name = typeof(DamagePower).FullName;
    }

    public void SetPower()
    {
        throw new System.NotImplementedException();
    }
    private void OnDestroy()
    {
        PowersManager.Instance.UnregisterPower<IDamage>(powerInfo);
    }
}
