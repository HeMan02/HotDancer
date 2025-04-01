using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IConvertibleValues
{
    public GameObject player;
    public int damageEnemy = 10;
    public int startDamageEnemy = 40;
    public float life = 200;
    public bool explosion;
    private float radius = 1f;
    private bool stopMovement = false;
    SpriteRenderer spriteRenderer;
    private float timerFreezeMovement = 0;
    private float resetMovementEnemy = 4;
    int damageToPlayer = 10;
    public ParticleSystem particleHit;
    public ParticleSystem particleDeath;


    NavMeshAgent navMeshAgent;

    void OnEnable()
    {
        Mediator.Instance.RegisterAction(SetDamageEnemyObj, IEntity.TypeEvents.DamageToEnemy);
        Mediator.Instance.RegisterAction(SetDamage, IEntity.TypeEvents.Damage);
        Mediator.Instance.RegisterAction(SetExplosion, IEntity.TypeEvents.ExplosionDamageEnemy);
        Mediator.Instance.RegisterAction(SetVelocity, IEntity.TypeEvents.EnemySpeed);
        Mediator.Instance.RegisterAction(SetLife, IEntity.TypeEvents.EnemyLife);
        Mediator.Instance.RegisterAction(SetDamageToPlayer, IEntity.TypeEvents.EnemyDamage);
        Mediator.Instance.RegisterAction(SetImageEnemy, IEntity.TypeEvents.ImageEnemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        damageEnemy = Mediator.Instance.GetValuePower(IEntity.TypeEvents.Damage);
        explosion = Mediator.Instance.CheckPowerEnable(IEntity.TypeEvents.ExplosionDamageEnemy);
        spriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        CheckPowersEnable();
    }

    private void OnDestroy()
    {
        Mediator.Instance.UnregisterAction(SetDamageEnemyObj, IEntity.TypeEvents.DamageToEnemy);
        Mediator.Instance.UnregisterAction(SetDamage, IEntity.TypeEvents.Damage);
        Mediator.Instance.UnregisterAction(SetExplosion, IEntity.TypeEvents.ExplosionDamageEnemy);
        Mediator.Instance.UnregisterAction(SetVelocity, IEntity.TypeEvents.EnemySpeed);
        Mediator.Instance.UnregisterAction(SetLife, IEntity.TypeEvents.EnemyLife);
        Mediator.Instance.UnregisterAction(SetDamageToPlayer, IEntity.TypeEvents.EnemyDamage);
        Mediator.Instance.UnregisterAction(SetImageEnemy, IEntity.TypeEvents.ImageEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent != null)
        {
            if (player == null)
                return;
            if (stopMovement)
            {
                navMeshAgent.SetDestination(this.transform.position);
            }
            else
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
        }
        EnemyRaceCondition.Instance.SetObjectActive(this.gameObject);
        if (stopMovement)
        {
            timerFreezeMovement += Time.deltaTime;
            resetMovementEnemy *= Mediator.Instance.GetValuePower(IEntity.TypeEvents.FreezePosition);
            if (timerFreezeMovement >= resetMovementEnemy)
            {
                spriteRenderer.color = new Vector4(255, 255, 255, 255);
                timerFreezeMovement = 0;
                stopMovement = false;
            }
        }
    }

    public void SetDamage(object value)
    {
        damageEnemy = (int)value;
    }

    public void SetExplosion(object value)
    {
        explosion = true;
    }

    public void SetDamageEnemyObj(object valueObj)
    {
        GameObject target = (GameObject)valueObj;
        if (target != null && this.gameObject != null)
        {
            if (target.Equals(this.gameObject))
            {
                particleHit.Play();
                if (Mediator.Instance.CheckPowerEnable(IEntity.TypeEvents.FreezePosition))
                {
                    stopMovement = true;
                    spriteRenderer.color = new Vector4(0, 233, 245, 255);
                }

                this.life -= damageEnemy;
                if (life <= 0)
                {
                    if (Mediator.Instance.GetValuePower(IEntity.TypeEvents.RechargeLife) != 0)
                        Mediator.Instance.SetAction(Mediator.Instance.GetValuePower(IEntity.TypeEvents.RechargeLife), IEntity.TypeEvents.RechargeLife);
                    Mediator.Instance.SetAction(true, IEntity.TypeEvents.KillEnemy);
                    Mediator.Instance.SetAction(-1, IEntity.TypeEvents.SetNumEnemy);
                    Mediator.Instance.SetAction(1, IEntity.TypeEvents.SetCoins);
                    Mediator.Instance.UpdateValueCounterAchievementMediator(1);
                    Mediator.Instance.SetAction(1, IEntity.TypeEvents.UpdateAchievements);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Potere non implementato
    /// </summary>
    public void ExplosionEnemy()
    {
        // DA RIVEDRE
        GameObject explosionPrefab = Resources.Load("Prefab/ExplosionDamage") as GameObject;
        var explosion = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        if (hitColliders.Length != 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.tag.Equals("Enemy") && !hitColliders[i].gameObject.Equals(this.gameObject))
                    Mediator.Instance.SetAction(hitColliders[i].gameObject, IEntity.TypeEvents.DamageToEnemy);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Mediator.Instance.SetAction(damageToPlayer, IEntity.TypeEvents.Life);
            Mediator.Instance.SetAction(-1, IEntity.TypeEvents.SetNumEnemy);
            Destroy(this.gameObject);
        }
    }

    public void SetVelocity(object value)
    {
        navMeshAgent.speed = float.Parse(value.ToString());
    }

    public void SetLife(object value)
    {
        life = float.Parse(value.ToString());
    }

    public void SetDamageToPlayer(object value)
    {
        damageToPlayer = int.Parse(value.ToString());
    }

    public void SetImageEnemy(object value)
    {
        spriteRenderer.sprite = (Sprite)value;
    }

    public void CheckPowersEnable()
    {
        if (Mediator.Instance.CheckPowerEnable(IEntity.TypeEvents.EnemyDamage))
            SetDamageToPlayer(Mediator.Instance.GetValuePower(IEntity.TypeEvents.EnemyDamage));
        if (Mediator.Instance.CheckPowerEnable(IEntity.TypeEvents.EnemyLife))
            SetLife(Mediator.Instance.GetValuePower(IEntity.TypeEvents.EnemyLife));
        if (Mediator.Instance.CheckPowerEnable(IEntity.TypeEvents.EnemySpeed))
            SetVelocity(Mediator.Instance.GetValuePower(IEntity.TypeEvents.EnemySpeed));
    }

}
