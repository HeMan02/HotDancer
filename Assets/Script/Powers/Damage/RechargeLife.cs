using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeLife : IDamage, IEntity
{
    private int count;
    public string NamePower => "RechargeLife";
    public int EffectValue { get => 10 * Count; }
    public float Probability { get => 10 * Count; }
    public int Time { get => 40 * Count; }
    public int EffectValueStart { get => 10 * Count; }
    public int EffectValuePower { get => 10 * Count; }
    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.RechargeLife;

    public int MaxValueToUnlock => 200;
    [SerializeField]
    private int counterUnlock;
    public int CounterUnlock { get => counterUnlock; set { counterUnlock = value; } }

    public RechargeLife()
    {
        Count = 0;
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
    }

    public void SetAchievementPower(object value)
    {
        if (Type.Equals(value.GetType(), this.GetType()))
        {
            PowersManager.Instance.RegisterPowerInterface<IMovement>(this);
        }
    }

    public void Init()
    {
        InitAchievement();
    }

    public void DoDamage(int value)
    {
        throw new System.NotImplementedException();
    }

    public void InitAchievement()
    {
        Mediator.Instance.SetAchievementPowerMediator<RechargeLife>(this);
        Mediator.Instance.RegisterAction(SetAchievementPower, IEntity.TypeEvents.EnableAchievement);
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
