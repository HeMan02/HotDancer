using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : IPowerEnemy, IEntity
{
    private int count;
    public string NamePower => "EnemyDamage";

    public int EffectValue { get => 10 * Count; }

    public int EffectValueStart { get => 10 * Count; }

    public int EffectValuePower { get => 10 * Count; }

    public float Probability { get => 1 * Count; }

    public int Time { get => 1 * Count; }

    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public EnemyDamage()
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
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.EnemyDamage;
            powerInfo.Name = typeof(EnemyDamage).FullName;
            PowersManager.Instance.RegisterPowerInterface<IPowerEnemy>(powerInfo);
        }

        // Da valutare
        Mediator.Instance.SetAction(powerInfo.Entity.EffectValueStart, IEntity.TypeEvents.EnemyDamage);
    }

    private void OnDestroy()
    {
        PowersManager.Instance.UnregisterPower<IPowerEnemy>(powerInfo);
    }

}
