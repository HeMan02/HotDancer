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
    public IEntity.TypeEvents TypePowers => IEntity.TypeEvents.EnemySpeed;

    public int MaxValueToUnlock => throw new System.NotImplementedException();

    public int CounterUnlock { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
        PowersManager.Instance.RegisterPowerInterface<IPowerEnemy>(this);
        // Da valutare
        Mediator.Instance.SetAction(EffectValueStart, IEntity.TypeEvents.EnemySpeed);
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
