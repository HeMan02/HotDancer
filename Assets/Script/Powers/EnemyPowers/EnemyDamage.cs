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
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.EnemyDamage;

    public int MaxValueToUnlock => throw new System.NotImplementedException();

    public int CounterUnlock { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


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
        PowersManager.Instance.RegisterPowerInterface<IPowerEnemy>(this);
        // Da valutare
        Mediator.Instance.SetAction(EffectValueStart, IEntity.TypeEvents.EnemyDamage);
    }

    private void OnDestroy()
    {
        PowersManager.Instance.UnregisterPower<IPowerEnemy>(this);
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
