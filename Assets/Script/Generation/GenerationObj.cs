using System;
using UnityEngine;

public class GenerationObj : MonoBehaviour
{
    [SerializeField] public GameObject spawnPointPlayer;
    void Awake()
    {
        GenerateObj<SingletonManager>();
        GenerateObj<Mediator>();
        GenerateObj<PowersManager>();

        // PLAYER
        GameObject playerPrefab = Resources.Load("Prefab/Player2") as GameObject;
        var player = Instantiate(playerPrefab, spawnPointPlayer.transform.position, playerPrefab.transform.rotation);

    }
    // Start is called before the first frame update
    void Start()
    {
        // POWER PLAYER
        GenerateObj<DamagePower>();
        GenerateObj<SpeedPower>();
        GenerateObj<RechargePower>();
        GenerateObj<BounceBullet>();


        // ACHIEVEMENTS
        GenerateObj<Granade>();
        GenerateObj<RechargeLife>();
        GenerateObj<FreezeMovement>();
        // POWER ENEMY
        GenerateObj<EnemySpeed>();
        GenerateObj<EnemyLife>();
        GenerateObj<EnemyDamage>();

        // GENERATION ACHIEVEMENTS SALVATI JSON
        Mediator.Instance.TryActiveAchievements();

    }

    public void GenerateObj<T>() where T : class
    {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
    }
}
