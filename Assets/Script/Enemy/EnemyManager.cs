using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    // OBJ SPAWN
    [SerializeField] public GameObject[] spawnPoints;

    public GameData gameData;
    public static EnemyManager Instance;
    public Dictionary<GameObject, InfoSpawnPoin<ISpawnPoint>> dicSpawnPoint = new Dictionary<GameObject, InfoSpawnPoin<ISpawnPoint>>();

    // COROUTINE + ACTION
    private IEnumerator coroutine;
    public Action<GameObject> startGenerationEnemy;


    // DATA SPAWN
    [SerializeField]
    public int maxTimeToSpawn = 3;
    public int[] maxEnemyForSpawnPoint;
    public int counterEnemyOnSpawnPoint = 0;
    [FormerlySerializedAs("thisIsMyOldField")]
    [Range(0, 30)]
    public int maxEnemyTotal;
    public int numRandEnemy;
    public int counterEnemyLive = 0;

    // TIMER BALANCING
    float timerUpgradeEnemy = 0;
    float stepUpgradeEnemyTimer = 0;

    // TIMER SPAWN
    float timerSpawnEnemy = 0;
    float stepSpawnTimer = 20;
    int counterRunGenerationEnemy = 0;
    int numRunGenerationEnemy = 4;

    IEntity.TypeEvents[] arrayPowersEnemy = new IEntity.TypeEvents[]
    {
        IEntity.TypeEvents.EnemyDamage,
        IEntity.TypeEvents.EnemyLife,
        IEntity.TypeEvents.EnemySpeed
    };

    public void Awake()
    {
        if (gameData.debugMode)
        {
            maxEnemyTotal = gameData.maxEnemySpwanDBM;
            this.maxTimeToSpawn = gameData.maxTimeToSpwanDBM;
        }

        SingletonManager.Instance.RegisterObj<EnemyManager>(this);
        Instance = SingletonManager.Instance.GetObjInstance<EnemyManager>();
        maxEnemyForSpawnPoint = new int[spawnPoints.Length];
        SetBalanceSpawnEnemy();

        numRandEnemy = UnityEngine.Random.Range(0, gameData.numEnemiesGeneration);
        counterEnemyLive = maxEnemyTotal;
    }

    // Start is called before the first frame update
    void Start()
    {
        Mediator.Instance.RegisterAction(SetNumberEnemy, IEntity.TypeEvents.SetNumEnemy);
        stepUpgradeEnemyTimer = gameData.stepUpgradeEnemyTimer;
        SetTypeEnemy();
        coroutine = StartGeneration(2);
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        UpdatePowerEnemy();
    }


    private void UpdatePowerEnemy()
    {
        timerUpgradeEnemy += Time.deltaTime;

        if (timerUpgradeEnemy >= stepUpgradeEnemyTimer)
        {
            if (stepUpgradeEnemyTimer <= 5)
            {
                stepUpgradeEnemyTimer = 5;
                timerUpgradeEnemy = 0;
                SetPowerEnemy();
            }
            else
            {
                stepUpgradeEnemyTimer /= 2;
                timerUpgradeEnemy = 0;
                SetPowerEnemy();
            }

        }
    }

    public bool ExistSpawnPointe(GameObject obj)
    {
        if (dicSpawnPoint.ContainsKey(obj))
        {
            if (dicSpawnPoint[obj] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public InfoSpawnPoin<ISpawnPoint> RegisterSpawnPoint(GameObject obj, InfoSpawnPoin<ISpawnPoint> spawnPoint)
    {
        if (!ExistSpawnPointe(obj))
        {
            spawnPoint = GenerateRandomValueSpawnPoint(spawnPoint);
            dicSpawnPoint.Add(obj, spawnPoint);
            return spawnPoint;
        }
        else
        {
            Debug.Log("ERRORE: Oggetto spawn point già presente");
            return null;
        }
    }

    public InfoSpawnPoin<ISpawnPoint> GenerateRandomValueSpawnPoint(InfoSpawnPoin<ISpawnPoint> spawnPoint)
    {
        spawnPoint.Entity.maxEnemyToSpawnPoint = maxEnemyForSpawnPoint[counterEnemyOnSpawnPoint];
        spawnPoint.Entity.timeToSpawn = UnityEngine.Random.Range(0, this.maxTimeToSpawn);
        counterEnemyOnSpawnPoint++;

        return spawnPoint;
    }

    public void SetBalanceSpawnEnemy()
    {
        int maxVal = maxEnemyTotal;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == spawnPoints.Length - 1)
            {
                maxEnemyForSpawnPoint[i] = maxVal;
            }
            else
            {
                int value = UnityEngine.Random.Range(0, maxVal - spawnPoints.Length);
                maxEnemyForSpawnPoint[i] = value;
                maxVal -= value;
            }

        }
    }

    private IEnumerator StartGeneration(float waitTime)
    {
        startGenerationEnemy?.Invoke(spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)]);
        yield return new WaitForSeconds(waitTime);
    }

    public void ReturnNextGenerationEnemy()
    {
        startGenerationEnemy?.Invoke(spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)]);
    }

    public void SetGenerationEnemyValue()
    {
        maxEnemyTotal--;
        if (maxEnemyTotal <= 0)
        {
            //for (int i = 0; i < spawnPoints.Length; i++)
            //{
            //    Destroy(spawnPoints[i]);
            //}
        }
    }

    public void SetTypeEnemy()
    {
        Mediator.Instance.SetAction(this.numRandEnemy, IEntity.TypeEvents.ImageEnemyCount);
    }

    public void SetPowerEnemy()
    {
        int randomPower = UnityEngine.Random.Range(0, arrayPowersEnemy.Length);

        PowerInfo<IEntity> powerEnemy = Mediator.Instance.GetPowerMediator(arrayPowersEnemy[randomPower]);
        Mediator.Instance.SetActivePower(powerEnemy);
        Mediator.Instance.SetAction((int)Mediator.Instance.GetValuePower(arrayPowersEnemy[randomPower]), arrayPowersEnemy[randomPower]);
        Mediator.Instance.SetAction(randomPower, IEntity.TypeEvents.SetPowerEnemy);
    }

    public void SetNumberEnemy(object value)
    {
        counterEnemyLive += int.Parse(value.ToString());
        if (counterEnemyLive <= 0)
        {
            Mediator.Instance.SetAction(true, IEntity.TypeEvents.EndGame);
        }
    }
}
