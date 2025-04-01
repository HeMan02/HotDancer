using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public interface IAchievementsValues
{
    [SerializeField]
    public int MaxValueToUnlock { get;}
    [SerializeField]
    public int CounterUnlock { get; set; }

    public void InitAchievement();
    public void UpdateAchievement();
    public void RemoveAchievement();
    public void SaveAchievement();
}
