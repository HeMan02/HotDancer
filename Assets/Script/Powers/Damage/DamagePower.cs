public class DamagePower : IDamage, IEntity
{
    private int count;
    public string NamePower => "DamageExtra";
    public int EffectValue { get => 40 * Count; }
    public float Probability { get => 40 * Count; }
    public int Time { get => 40 * Count; }
    public int EffectValueStart { get => 40 * Count; }
    public int EffectValuePower { get => 40 * Count; }
    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.Damage;

    public int MaxValueToUnlock => throw new System.NotImplementedException();

    public int CounterUnlock { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
        PowersManager.Instance.RegisterPowerInterface<IDamage>(this);
        Mediator.Instance.SetAction(EffectValueStart, IEntity.TypeEvents.Damage);
    }

    public void DoDamage(int value)
    {
        throw new System.NotImplementedException();
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {

    }

    public void SetPower()
    {
        throw new System.NotImplementedException();
    }
    private void OnDestroy()
    {
        PowersManager.Instance.UnregisterPower<IDamage>(this);
    }

    public void InitAchievement()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateAchievement()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveAchievement()
    {
        throw new System.NotImplementedException();
    }

    public void SaveAchievement()
    {
        throw new System.NotImplementedException();
    }
}
