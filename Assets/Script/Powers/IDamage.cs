using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void DoDamage(int value);

    public void SetValuesPower(PowerInfo<IEntity> obj);
}
