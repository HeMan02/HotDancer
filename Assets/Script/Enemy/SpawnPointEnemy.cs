using System.Collections;
using UnityEngine;

public class SpownPointEnemy : MonoBehaviour, ISpawnPoint
{

    public float timerSpawn = 0;
    public bool startTimer = false;
    InfoSpawnPoin<ISpawnPoint> spawnPoint = new InfoSpawnPoin<ISpawnPoint>();
    private IEnumerator coroutine;

    public int maxEnemyToSpawnPoint { get; set; }
    public float timeToSpawn { get; set; }

    string namePrefabEnemy;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint.Entity = new SpownPointEnemy();
        spawnPoint.Name = this.gameObject.name;
        spawnPoint = EnemyManager.Instance.RegisterSpawnPoint(gameObject, spawnPoint);
        EnemyManager.Instance.startGenerationEnemy += TryGeneration;
        namePrefabEnemy = "Enemy" + EnemyManager.Instance.numRandEnemy;
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.startGenerationEnemy -= TryGeneration;
    }

    public void GenerateEnemy()
    {
        Mediator.Instance.SetAction(1, IEntity.TypeEvents.SetNumEnemy);
        GameObject enemyPrefab = Resources.Load("Prefab/" + namePrefabEnemy) as GameObject;
        var enemy = Instantiate(enemyPrefab, transform.position, transform.transform.rotation);
        EnemyManager.Instance.SetGenerationEnemyValue();
    }

    private IEnumerator RunGeneration(float waitTime)
    {
        GenerateEnemy();
        yield return new WaitForSeconds(waitTime);
        EnemyManager.Instance.ReturnNextGenerationEnemy();
    }

    public void TryGeneration(GameObject target)
    {
        if (target.Equals(this.gameObject))
        {
            coroutine = RunGeneration(spawnPoint.Entity.timeToSpawn);
            StartCoroutine(coroutine);
        }
    }
}
