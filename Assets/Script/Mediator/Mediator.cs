using System;
using System.Collections.Generic;
using System.Linq;

public class Mediator : IPlayerMovement, IConvertibleValues
{
    // GUI
    public Func<bool, bool> consumeBullet;

    //DIC ACTION
    public Dictionary<IEntity.TypeEvents, Action<object>> actions = new Dictionary<IEntity.TypeEvents, Action<object>>();

    public static Mediator Instance;

    public Mediator()
    {
        Init();
    }

    public void Init()
    {
        SingletonManager.Instance.RegisterObj<Mediator>(this);
        Instance = SingletonManager.Instance.GetObjInstance<Mediator>();
    }

    public void SetAction(object value, IEntity.TypeEvents events)
    {
        if (actions.Keys.Contains(events))
            actions[events].Invoke(value);
    }

    public void StartAttack(bool value)
    {
        bool isRecharge = consumeBullet.Invoke(value);
        if (!isRecharge)
            PowersManager.Instance.SetAttack(value);
    }

    public List<PowerInfo<IEntity>> GetRandomPowersMediator()
    {
        List<PowerInfo<IEntity>> randomPowers = PowersManager.Instance.GetRandomPowers();
        return randomPowers;
    }

    public List<PowerInfo<IEntity>> GetListPowersMediator()
    {
        List<PowerInfo<IEntity>> randomPowers = PowersManager.Instance.GetListPowers();
        return randomPowers;
    }

    public PowerInfo<IEntity> GetPowerMediator(IEntity.TypeEvents eventPower)
    {
        return PowersManager.Instance.GetPower(eventPower);
    }

    public void SetActivePower(PowerInfo<IEntity> power)
    {
        PowersManager.Instance.UpdatePower(power);
    }

    public void RegisterAction(Action<object> a, IEntity.TypeEvents events)
    {
        if (actions.ContainsKey(events))
        {
            actions[events] += a;
        }
        else
        {
            actions.Add(events, a);
        }
    }

    public int GetValuePower(IEntity.TypeEvents eventPower)
    {

                return PowersManager.Instance.GetValuePower(eventPower);
    }

    public void UnregisterAction(Action<object> a, IEntity.TypeEvents events)
    {
        if (actions.ContainsKey(events))
        {
            actions[events] -= a;
        }
    }

    public bool CheckPowerEnable(IEntity.TypeEvents eventPower)
    {
         return PowersManager.Instance.CheckPower(eventPower);
    }

    public void DisablePower(PowerInfo<IEntity> power)
    {
        PowerInfo<IEntity> powerUpdate = PowersManager.Instance.DisablePower(power);
    }
}
