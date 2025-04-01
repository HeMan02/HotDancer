using System;

public class SpeedPower : IMovement, IEntity
{
    private int count;
    public string NamePower => "SpeedExtra";
    public int EffectValue { get => 5 * Count; }
    public float Probability { get => 5 * Count; }

    public int Time { get => 5 * Count; }
    public int EffectValueStart { get => 5 * Count; }
    public int EffectValuePower { get => 5 * Count; }

    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.Speed;

    public int MaxValueToUnlock => throw new NotImplementedException();

    public int CounterUnlock { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public SpeedPower()
    {
        Count = 1;
    }

    public void DoMovement(int value)
    {
        throw new System.NotImplementedException();
    }


    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
    }

    private void SetValuesStandard()
    {

        PowersManager.Instance.RegisterPowerInterface<IMovement>(this);

        Mediator.Instance.SetAction(EffectValueStart, IEntity.TypeEvents.Speed);

    }

    public void SetPower()
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        SetValuesStandard();
    }

    public void InitAchievement()
    {
        throw new NotImplementedException();
    }

    public void UpdateAchievement()
    {
        throw new NotImplementedException();
    }

    public void RemoveAchievement()
    {
        throw new NotImplementedException();
    }

    public void SaveAchievement()
    {
        throw new NotImplementedException();
    }
}
