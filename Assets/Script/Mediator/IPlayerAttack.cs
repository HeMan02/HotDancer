using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAttack
{
    // PLAYER ATTACK
    public event Action<bool> attackAction;

    public void StartAttack(bool value, Action<bool> attackAction) => attackAction?.Invoke(value);

}
