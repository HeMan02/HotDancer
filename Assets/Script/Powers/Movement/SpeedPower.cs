using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;
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
        powerInfo = new PowerInfo<IEntity>();
        powerInfo.Entity = this;
        powerInfo.Entity.TypePowers = IEntity.TypeEvents.Speed;
        powerInfo.Name = typeof(SpeedPower).FullName;
    }

    private void SetValuesStandard()
    {
        if(powerInfo is null)
        {
            powerInfo = new PowerInfo<IEntity>();
            powerInfo.Entity = this;
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.Speed;
            powerInfo.Name = typeof(SpeedPower).FullName;
            PowersManager.Instance.RegisterPowerInterface<IMovement>(powerInfo);
        }

        Mediator.Instance.SetAction(powerInfo.Entity.EffectValueStart, IEntity.TypeEvents.Speed);

    }

    public void SetPower()
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        SetValuesStandard();
    }
}
