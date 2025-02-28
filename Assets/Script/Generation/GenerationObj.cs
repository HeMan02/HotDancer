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
        GenerateObj<DamagePower>();
        GenerateObj<SpeedPower>();
        GenerateObj<RechargePower>();
        //GenerateObj<ExplosionEnemyDamage>();
        GenerateObj<BounceBullet>();
        GenerateObj<Granade>();
        GenerateObj<FreezeMovement>();
        GenerateObj<RechargeLife>();
        GenerateObj<EnemySpeed>();
        GenerateObj<EnemyLife>();
        GenerateObj<EnemyDamage>();



    }

    public void GenerateObj<T>() where T : class
    {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
    }
}
