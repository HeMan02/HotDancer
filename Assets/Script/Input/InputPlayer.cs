using System.Security.Principal;
using UnityEngine;


public class InputPlayer : MonoBehaviour
{
    float timerShooter = 0;
    float timeShoot = 3f;
    bool canFire = true;
    // Update is called once per frame
    void Update()
    {
        Mediator.Instance.SetAction(Input.GetAxisRaw("Horizontal"), IEntity.TypeEvents.InputHz);
        Mediator.Instance.SetAction(Input.GetAxisRaw("Vertical"), IEntity.TypeEvents.InputVt);
        

        if (!canFire)
        {
            timerShooter += Time.deltaTime * 10;
            if (timerShooter > timeShoot)
            {
                timerShooter = 0;
                canFire = true;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Mediator.Instance.StartAttack(Input.GetMouseButton(0));
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            Mediator.Instance.SetAction(true, IEntity.TypeEvents.EnableAchievementsPanel);
        }
    }
}
