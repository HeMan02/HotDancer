using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class EnemySpeed : IPowerEnemy, IEntity
{
    private int count;
    public string NamePower => "EnemySpeed";

    public int EffectValue { get => 3 * Count; }

    public int EffectValueStart { get => 3 * Count; }

    public int EffectValuePower { get => 3 * Count; }

    public float Probability { get => 3 * Count; }

    public int Time { get => 1 * Count; }

    public int Count { get => count; set { count = value; Init(); } }
    public IEntity.TypeEvents TypePowers { get; set; }

    PowerInfo<IEntity> powerInfo = null;

    public EnemySpeed()
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
            powerInfo.Entity.TypePowers = IEntity.TypeEvents.EnemySpeed;
            powerInfo.Name = typeof(EnemySpeed).FullName;
            PowersManager.Instance.RegisterPowerInterface<IPowerEnemy>(powerInfo);
        }

        // Da valutare
        Mediator.Instance.SetAction(powerInfo.Entity.EffectValueStart, IEntity.TypeEvents.EnemySpeed);
    }

    private void OnDestroy()
    {
        PowersManager.Instance.UnregisterPower<IPowerEnemy>(powerInfo);
    }
}
