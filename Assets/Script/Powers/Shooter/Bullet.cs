using UnityEngine;

public class Bullet : MonoBehaviour, IShooter, IConvertibleValues
{
    public GameObject particleExplosion;
    float timer = 0f;
    int valueBouncePower = 0;

    void Start()
    {
        valueBouncePower = Mediator.Instance.GetValuePower(IEntity.TypeEvents.BounceBullet);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            if (this.gameObject.name.Contains("Granade"))
            {
                GameObject particle = Resources.Load("Prefab/EXPLOSIONGRANADE") as GameObject;
                var enemy = Instantiate(particle, transform.position, transform.transform.rotation);
                Mediator.Instance.SetAction(true, IEntity.TypeEvents.ExplosionGranade);
            }
            Destroy(gameObject);
        }
    }

    public void DoShoot(GameObject target)
    {
        if (target != null)
        {
            Mediator.Instance.SetAction(target, IEntity.TypeEvents.DamageToEnemy);
        }
    }

    public void SetValuesPower(PowerInfo<IEntity> obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("TRIGGER!!");
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            //Debug.Log("DANNO TRIGGER!!");
            DoShoot(collision.gameObject);
            if (Mediator.Instance.CheckPowerEnable(IEntity.TypeEvents.BounceBullet))
            {
                if (valueBouncePower > 0)
                {
                    Vector2 newPosition = new Vector2((collision.gameObject.transform.position.x - this.gameObject.transform.position.x), (collision.gameObject.transform.position.y - this.gameObject.transform.position.y));
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = newPosition * 20;
                    valueBouncePower--;
                }
            }
            else
            {
                if (this.gameObject.name.Contains("Granade"))
                {
                    GameObject particle = Resources.Load("Prefab/EXPLOSIONGRANADE") as GameObject;
                    var enemy = Instantiate(particle, transform.position, transform.transform.rotation);
                    Mediator.Instance.SetAction(true, IEntity.TypeEvents.ExplosionGranade);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
