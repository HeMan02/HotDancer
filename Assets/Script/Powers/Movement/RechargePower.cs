using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargePower : IMovement, IEntity
{
    private int count;
    public string NamePower => "RechargeExtra";
    public int EffectValue { get => 2 * Count; }
    public float Probability { get => 2 * Count; }
    public int Time { get => 2 * Count; }
    public int EffectValueStart { get => 2 * Count; }
    public int EffectValuePower { get => 2 * Count; }
    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.Recharge;

    public int MaxValueToUnlock => throw new System.NotImplementedException();

    public int CounterUnlock { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public RechargePower()
    {
        Count = 1;
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {

    }

    public void SetValuesStandard()
    {
        PowersManager.Instance.RegisterPowerInterface<IMovement>(this);
        Mediator.Instance.SetAction(EffectValueStart, IEntity.TypeEvents.Recharge);
    }

    public void Init()
    {
        SetValuesStandard();
    }

    public void DoMovement(int value)
    {
        throw new System.NotImplementedException();
    }

    public void SetPower()
    {
        throw new System.NotImplementedException();
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
