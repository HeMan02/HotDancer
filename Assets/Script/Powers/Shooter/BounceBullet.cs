using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : IShooter, IEntity
{
    private int _count;
    public string NamePower => "BounceBullet";
    public int EffectValue { get => 1 * Count; }
    public float Probability { get => 1 * Count; }
    public int Time { get => 60 * Count; }
    public int EffectValueStart { get => 1 * Count; }
    public int EffectValuePower { get => 1 * Count; }
    public int Count { get => _count; set { _count = value; Init(); } }
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.BounceBullet;

    public int MaxValueToUnlock => throw new System.NotImplementedException();

    public int CounterUnlock { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    public BounceBullet()
    {
        Count = 0;
    }

    public void Init()
    {
        SetValuesStandard();
    }

    private void SetValuesStandard()
    {
            PowersManager.Instance.RegisterPowerInterface<IDamage>(this);
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
