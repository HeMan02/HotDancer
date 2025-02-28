using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConvertibleValues
{
    public class DamageObj
    {
        public float value;
        public GameObject target;
    }

    public enum Events
    {
        InputHz,
        InputVt,
        DamageToEnemy,
        Speed,
        Recharge,
        StartAttack,
        Life,
        Damage,
        ExplosionDamageEnemy,
        BounceBullet,
        Granade,
        FreezePosition,
        RechargeLife,
        ExplosionGranade,
        Reload,
        ChangePower,
        KillEnemy,
        EndGame,
        SetNumEnemy,
        EnemyLife,
        EnemyDamage,
        EnemySpeed,
        ImageEnemy,
        SetPowerEnemy,
        ImageEnemyCount,
    }

}
