using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : IPowerEnemy, IEntity
{
    private int count;
    public string NamePower => "EnemyLife";

    public int EffectValue { get => 150 * Count; }

    public int EffectValueStart { get => 150 * Count; }

    public int EffectValuePower { get => 150 * Count; }

    public float Probability { get => 1 * Count; }

    public int Time { get => 1 * Count; }

    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public EnemyLife()
    {
        Count = 1;
    }

    public void Init()
    {
        SetValuesStandard();
    }

    private void SetValuesStandard()
    {
        if (powerInfo is null)
        {
            powerInfo = new PowerInfo<IEntity>();
            powerInfo.Entity = this;
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.EnemyLife;
            powerInfo.Name = typeof(EnemyLife).FullName;
            PowersManager.Instance.RegisterPowerInterface<IPowerEnemy>(powerInfo);
        }

        // Da valutare
        Mediator.Instance.SetAction(powerInfo.Entity.EffectValueStart, IEntity.TypeEvents.EnemyLife);
    }

    private void OnDestroy()
    {
        PowersManager.Instance.UnregisterPower<IPowerEnemy>(powerInfo);
    }
}
