using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPoint
{
    public int maxEnemyToSpawnPoint { get; set; }
    public float timeToSpawn { get; set; }
}
