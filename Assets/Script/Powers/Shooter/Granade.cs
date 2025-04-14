using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Granade : IShooter, IEntity
{
    private int _count;
    public string NamePower => "Granade";
    public int EffectValue { get => 1 * Count; }
    public float Probability { get => 1 * Count; }
    public int Time { get => 60 * Count; }
    public int EffectValueStart { get => 1 * Count; }
    public int EffectValuePower { get => 1 * Count; }
    public int Count { get => _count; set { _count = value; Init(); } }
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.Granade;

    // ACHIEVEMENTS
    [SerializeField]
    private int counterUnlock;
    public int MaxValueToUnlock => 100;

    public int CounterUnlock { get => counterUnlock; set { counterUnlock = value; } }

    public Granade()
    {
        Count = 0;
    }

    public void Init()
    {
        InitAchievement();
    }

    public void SetAchievementPower(object value)
    {
        if (Type.Equals(value.GetType(), this.GetType()))
        {
            PowersManager.Instance.RegisterPowerInterface<IMovement>(this);
        }
    }

    public void DoDamage(int value)
    {
        throw new System.NotImplementedException();
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {

    }

    public void DoShoot(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public void InitAchievement()
    {
        Mediator.Instance.SetAchievementPowerMediator<Granade>(this);
        Mediator.Instance.RegisterAction(SetAchievementPower, IEntity.TypeEvents.EnableAchievement);
    }

    public void UpdateAchievement()
    {
        Mediator.Instance.SetAchievementPowerMediator<Granade>( this);
    }

    public void RemoveAchievement()
    {
        Mediator.Instance.RemoveAchievementMediator<Granade>();
    }

    public void SaveAchievement()
    {
        Mediator.Instance.SaveAchievementsOnJson();
    }
}
