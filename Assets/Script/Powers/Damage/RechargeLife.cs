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
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public RechargeLife()
    {
        Count = 0;
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
        powerInfo = new PowerInfo<IEntity>();
        powerInfo.Entity = this;
        powerInfo.Entity.TypePowers = IEntity.TypeEvents.RechargeLife;
        powerInfo.Name = typeof(RechargeLife).FullName;
    }

    public void SetValuesStandard()
    {

        if(powerInfo is null)
        {
            powerInfo = new PowerInfo<IEntity>();
            powerInfo.Entity = this;
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.RechargeLife;
            powerInfo.Name = typeof(RechargeLife).FullName;
            PowersManager.Instance.RegisterPowerInterface<IMovement>(powerInfo);
        }
    }

    public void Init()
    {
        SetValuesStandard();
    }

    public void DoDamage(int value)
    {
        throw new System.NotImplementedException();
    }
}
