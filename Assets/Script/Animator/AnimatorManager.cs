using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public Animator animatorFire;
    public SpriteRenderer spriteRenderer;
    bool idleHz = false;
    bool idleVt = false;

    void Start()
    {
        Mediator.Instance.RegisterAction(IdleHz, IEntity.TypeEvents.InputHz);
        Mediator.Instance.RegisterAction(IdleVt, IEntity.TypeEvents.InputVt);
        PowersManager.Instance.attackAction += Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (idleHz || idleVt)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }
    }

    public void IdleHz(object value)
    {
        if ((float)value != 0)
        {
            idleHz = true;
        }
        else
        {
            idleHz = false;
        }

    }

    public void IdleVt(object value)
    {

        if ((float)value != 0)
            idleVt = true;
        else
            idleVt = false;
    }

    public void Shoot(bool value)
    {
        if (spriteRenderer)
            spriteRenderer.enabled = value;
    }
}
