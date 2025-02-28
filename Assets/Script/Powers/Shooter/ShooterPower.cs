using UnityEngine;

public class ShooterPower : MonoBehaviour
{
    GameObject bulletSpawnPoint;
    float bulletSpeed = 30f;
    private int countBullet = 0;
    private int countGranade = 3;
    // Start is called before the first frame update
    void Start()
    {
        PowersManager.Instance.attackAction += ShootBullet;

        if (gameObject != null)
            bulletSpawnPoint = GameObject.Find("BulletSpawnPoint");
    }

    private void OnDestroy()
    {
        PowersManager.Instance.attackAction -= ShootBullet;
    }

    public void ShootBullet(bool value)
    {
        if (value)
        {
            if (transform && bulletSpawnPoint)
            {
                GameObject bulletPrefab = Resources.Load("Prefab/Bullet2") as GameObject;
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

                bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.transform.up * bulletSpeed; 

                if (Mediator.Instance.CheckPowerEnable(IEntity.TypeEvents.Granade))
                {
                    countBullet++;
                    if (countBullet >= countGranade)
                    {
                        GenerateGranade();
                        countBullet = 0;
                    }
                }
            }
        }
    }

    public void GenerateGranade()
    {
        int numberGranade = Mediator.Instance.GetValuePower(IEntity.TypeEvents.Granade);
        for (int i = 0; i < numberGranade; i++)
        {
            GameObject bulletPrefab = Resources.Load("Prefab/Granade2") as GameObject;
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0), bulletSpawnPoint.transform.rotation);
        }

    }
}
