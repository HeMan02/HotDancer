using System;
using System.ComponentModel;
using System.Threading;

public interface IEntity
{
    public string NamePower { get;}

    public int EffectValue { get; }

    public int EffectValueStart { get;  }
    public int EffectValuePower { get; } 
    public float Probability { get; }
    public int Time { get; }
    public int Count { get; set; }

    public TypeEvents TypePowers { get; set; }

    // 0000 
    // 0001 1<<0
    // 0010 1<<1
    // 0111 7  DamageSpeed
    // Mischiare poteri
    [Flags]
    public enum TypeEvents
    {
        None = 0,
        Damage = 1<<0,
        Speed = 1<<1,
        Recharge = 1<<2,
        ExplosionDamageEnemy = 1<<3,
        BounceBullet = 1<<4,
        Granade = 1<<5,
        FreezePosition = 1<<6,
        RechargeLife = 1<<7,
        EnemySpeed = 1<<8,
        EnemyLife = 1<<9,
        EnemyDamage = 1<<10,
        DamageSpeed = Damage | Speed | Recharge,
        InputHz,
        InputVt,
        DamageToEnemy,
        StartAttack,
        Life,
        ExplosionGranade,
        Reload,
        ChangePower,
        KillEnemy,
        EndGame,
        SetNumEnemy,
        ImageEnemy,
        SetPowerEnemy,
        ImageEnemyCount,
        SetCoins,
    }

    public void Init();
}