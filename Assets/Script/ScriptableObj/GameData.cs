using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    [SerializeField] public int life = 100;
    [SerializeField] public int maxEnemySpwanDBM = 500;
    [SerializeField] public int maxTimeToSpwanDBM = 0;

    [SerializeField] public bool debugMode = false;
    [SerializeField] public bool godMode = false;

    [SerializeField] public float stepUpgradeEnemyTimer = 100;
    [SerializeField] public float timerGenerationPowerPlayer = 20000;
    [SerializeField]  public int numEnemiesGeneration = 4;
}
