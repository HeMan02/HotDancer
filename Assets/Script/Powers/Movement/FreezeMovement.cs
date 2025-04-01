using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FreezeMovement :IMovement, IEntity
{
    // POWER
    private int count;
    public string NamePower => "FreezeMovement";
    public int EffectValue { get => 1 * Count; }
    public float Probability { get => 1 * Count; }
    public int Time { get => 1 * Count; }
    public int EffectValueStart { get => 1 * Count; }
    public int EffectValuePower { get => 1 * Count; }
    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.FreezePosition;

    // ACHIEVEMENTS
    [SerializeField]
    private int counterUnlock;
    public int MaxValueToUnlock => 300;

    public int CounterUnlock { get => counterUnlock; set { counterUnlock = value; } }

    public FreezeMovement()
    {
        Count = 0;
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
    }

    public void SetValuesStandard()
    {
        Mediator.Instance.SetAction(EffectValueStart, IEntity.TypeEvents.FreezePosition);
    }

    public void SetAchievementPower(object value)
    {
        if (Type.Equals(value.GetType(),this.GetType()))
        {
            PowersManager.Instance.RegisterPowerInterface<IMovement>(this);
        }
    }

    public void Init()
    {
        SetValuesStandard();
        InitAchievement();
    }

    public void DoMovement(int value)
    {
        throw new System.NotImplementedException();
    }

    public void SetPower()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateAchievement()
    {
        Mediator.Instance.SetAchievementPowerMediator<FreezeMovement>(this);
    }

    public void RemoveAchievement()
    {
        Mediator.Instance.RemoveAchievementMediator<FreezeMovement>();
    }

    public void SaveAchievement()
    {
        Mediator.Instance.SaveAchievementsOnJson();
    }

    public void InitAchievement()
    {
        Mediator.Instance.SetAchievementPowerMediator<FreezeMovement>(this);
        Mediator.Instance.RegisterAction(SetAchievementPower, IEntity.TypeEvents.EnableAchievement);
    }
}
