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
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public RechargePower()
    {
        Count = 1;
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
        powerInfo = new PowerInfo<IEntity>();
        powerInfo.Entity = this;
        powerInfo.Entity.TypePowers = IEntity.TypeEvents.Recharge;
        powerInfo.Name = typeof(RechargePower).FullName;
    }

    public void SetValuesStandard()
    {

        if (powerInfo is null)
        {
            powerInfo = new PowerInfo<IEntity>();
            powerInfo.Entity = this;
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.Recharge;
            powerInfo.Name = typeof(RechargePower).FullName;
            PowersManager.Instance.RegisterPowerInterface<IMovement>(powerInfo);
        }
        Mediator.Instance.SetAction(powerInfo.Entity.EffectValueStart, IEntity.TypeEvents.Recharge);
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
}
