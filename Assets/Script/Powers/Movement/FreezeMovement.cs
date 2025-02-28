using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMovement :IMovement, IEntity
{
    private int count;
    public string NamePower => "FreezeMovement";
    public int EffectValue { get => 1 * Count; }
    public float Probability { get => 1 * Count; }
    public int Time { get => 1 * Count; }
    public int EffectValueStart { get => 1 * Count; }
    public int EffectValuePower { get => 1 * Count; }
    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public FreezeMovement()
    {
        Count = 0;
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
        powerInfo = new PowerInfo<IEntity>();
        powerInfo.Entity = this;
        powerInfo.Entity.TypePowers = IEntity.TypeEvents.FreezePosition;
        powerInfo.Name = typeof(FreezeMovement).FullName;
    }

    public void SetValuesStandard()
    {
        if(powerInfo is null)
        {
            powerInfo = new PowerInfo<IEntity>();
            powerInfo.Entity = this;
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.FreezePosition;
            powerInfo.Name = typeof(FreezeMovement).FullName;
            PowersManager.Instance.RegisterPowerInterface<IMovement>(powerInfo);
        }     
        Mediator.Instance.SetAction(powerInfo.Entity.EffectValueStart, IEntity.TypeEvents.FreezePosition);
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
