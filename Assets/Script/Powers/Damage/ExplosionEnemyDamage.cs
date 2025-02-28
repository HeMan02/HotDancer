public class ExplosionEnemyDamage : IDamage, IEntity
{
    private int _count;
    public string NamePower => "ExplosionDamage";
    public int EffectValue { get => 1 * Count; }
    public float Probability { get => 1 * Count; }
    public int Time { get => 60 * Count; }
    public int EffectValueStart { get => 1 * Count; }
    public int EffectValuePower { get => 1 * Count; }  // Da cambiare gli altri parametri 
    public int Count { get => _count; set { _count = value; Init(); } } // ANDRE SUGGERIMENTO CON INIT
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public ExplosionEnemyDamage()
    {
        Count = 0;
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
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.ExplosionDamageEnemy;
            powerInfo.Name = typeof(ExplosionEnemyDamage).FullName;
            PowersManager.Instance.RegisterPowerInterface<IDamage>(powerInfo);
        }
    }

    public void DoDamage(int value)
    {
        throw new System.NotImplementedException();
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
        powerInfo = new PowerInfo<IEntity>();
        powerInfo.Entity = this;
        powerInfo.Entity.TypePowers = IEntity.TypeEvents.ExplosionDamageEnemy;
        powerInfo.Name = typeof(ExplosionEnemyDamage).FullName;
    }

    public void SetPower()
    {
        throw new System.NotImplementedException();
    }
}
