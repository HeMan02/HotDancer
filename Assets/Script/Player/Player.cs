using System.Security.Principal;
using UnityEngine;

public class Player : MonoBehaviour, IConvertibleValues
{
    // VARIABLE 
    public int moveSpeed = 0;
    private int life = 100;

    // INPUT
    private float hzInput;
    private float verInput;

    // ROTATION
    Vector3 mouse_pos;
    Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;

    // PARTICLE
    public ParticleSystem particleHit;

    public GameData gameData;

    void OnEnable()
    {
        Mediator.Instance.RegisterAction(SetSpeed, IEntity.TypeEvents.Speed);
        Mediator.Instance.RegisterAction(SetInputHz, IEntity.TypeEvents.InputHz);
        Mediator.Instance.RegisterAction(SetInputVt, IEntity.TypeEvents.InputVt);
        Mediator.Instance.RegisterAction(SetDamagePlayer, IEntity.TypeEvents.Life);
        Mediator.Instance.RegisterAction(SetLifeRecharge, IEntity.TypeEvents.RechargeLife);
    }

    private void OnDestroy()
    {
        Mediator.Instance.UnregisterAction(SetSpeed, IEntity.TypeEvents.Speed);
        Mediator.Instance.UnregisterAction(SetInputHz, IEntity.TypeEvents.InputHz);
        Mediator.Instance.UnregisterAction(SetInputVt, IEntity.TypeEvents.InputVt);
        Mediator.Instance.UnregisterAction(SetDamagePlayer, IEntity.TypeEvents.Life);
        Mediator.Instance.UnregisterAction(SetLifeRecharge, IEntity.TypeEvents.RechargeLife);
    }
    // Start is called before the first frame update
    void Start()
    {
        target = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // TRASLATE
        Vector3 movementDirection = new Vector3(hzInput, verInput, 0);
        movementDirection.Normalize();
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

        // ROTATION MOUSE DIRECTION
        RotatePlayerMouseDirection();
    }

    public void SetValueFromScriptableObj()
    {
        life = gameData.life;
    }

    private void RotatePlayerMouseDirection()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = object_pos.x - mouse_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.Euler(new Vector3(0, 0, -angle + 90));
    }

    public void SetInputHz(object value)
    {
        hzInput = (float)value;
    }

    public void SetInputVt(object value)
    {
        verInput = (float)value;
    }

    public void SetSpeed(object value)
    {
        moveSpeed = (int)value;
    }

    public void SetLifeRecharge(object value)
    {
        life += (int)value;
    }

    public void SetDamagePlayer(object value)
    {
        if (!gameData.godMode)
        {
            particleHit.Play();
            life -= (int)value;
            if (life <= 0)
            {
                Mediator.Instance.SetAction(false, IEntity.TypeEvents.EndGame);
                Destroy(gameObject);
            }
        }
    }
}
