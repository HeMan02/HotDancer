using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter
{
    public void DoShoot(GameObject target);
    public void SetValuesPower(PowerInfo<IEntity> obj);
}
