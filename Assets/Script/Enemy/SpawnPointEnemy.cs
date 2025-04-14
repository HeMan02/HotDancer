
using System.Collections;
using UnityEngine;

public class SpownPointEnemy : MonoBehaviour, ISpawnPoint
{
    public bool startTimer = false;
    InfoSpawnPoin<ISpawnPoint> spawnPoint = new InfoSpawnPoin<ISpawnPoint>();
    private IEnumerator coroutine;

    public int maxEnemyToSpawnPoint { get; set; }
    public float timeToSpawn { get; set; }

    string namePrefabEnemy;

    float timerGeneration = 0;
    int maxValueGeneration = 5;
    int valueMaxRandom;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint.Entity = new SpownPointEnemy();
        spawnPoint.Name = this.gameObject.name;
        spawnPoint = EnemyManager.Instance.RegisterSpawnPoint(gameObject, spawnPoint);
        namePrefabEnemy = "Enemy" + EnemyManager.Instance.numRandEnemy;
        valueMaxRandom = Random.Range(1, maxValueGeneration);
    }

    void Update()
    {
        timerGeneration += Time.deltaTime;
        if(timerGeneration >= valueMaxRandom)
        {
            GenerateEnemy();
            timerGeneration = 0;
        }
    }

    public void GenerateEnemy()
    {
        Mediator.Instance.SetAction(1, IEntity.TypeEvents.SetNumEnemy);
        GameObject enemyPrefab = Resources.Load("Prefab/" + namePrefabEnemy) as GameObject;
        var enemy = Instantiate(enemyPrefab, transform.position, transform.transform.rotation);
        EnemyManager.Instance.SetGenerationEnemyValue();
    }
}
