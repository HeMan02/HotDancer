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

    public List<IEntity> GetRandomPowersMediator()
    {
        List<IEntity> randomPowers = PowersManager.Instance.GetRandomPowers();
        return randomPowers;
    }

    public List<IEntity> GetListPowersMediator()
    {
        List<IEntity> randomPowers = PowersManager.Instance.GetListPowers();
        return randomPowers;
    }

    public IEntity GetPowerMediator(IEntity.TypeEvents eventPower)
    {
        return PowersManager.Instance.GetPower(eventPower);
    }

    public void SetActivePower(IEntity power)
    {
        PowersManager.Instance.UpdatePower(power);
    }

    public void SetAchievementPowerMediator<T>(IEntity data) where T : class
    {
        SerializableManager<IEntity>.RegisterObj(data);
    }

    public List<IEntity> GetAchievementsMediatorObj()
    {
        List<object> listObj = SerializableManager<IEntity>.GetListAchievementsObj();
        List<IEntity> listAchievements = listObj.Cast<IEntity>().ToList();

        return listAchievements;
    }

    public void SaveAchievementsValueMediator()
    {
        SerializableManager<IEntity>.GenerationObjSaved();
    }

    public void TryActiveAchievements()
    {
        // Verifico se ci sono Achievement sbloccati dentro al json
        // Se presenti gli attivo la action per renderli utilizzabili
        List<object> listObj = SerializableManager<IEntity>.GenerationObjSaved();
        if (listObj == null)
            return;

        foreach (object obj in listObj)
        {
            IEntity acvmObj = (IEntity)obj;
            if (acvmObj.CounterUnlock >= acvmObj.MaxValueToUnlock)
                SetAction(obj, IEntity.TypeEvents.EnableAchievement);
        }
    }

    public void SaveAchievementsOnJson()
    {
        SerializableManager<IEntity>.GenerateJsonData();
    }

    public void RemoveAchievementMediator<T>() where T : class
    {
        SerializableManager<IEntity>.RemoveAchievement<T>();
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

    public void DisablePower(IEntity power)
    {
        IEntity powerUpdate = PowersManager.Instance.DisablePower(power);
    }

    public void UpdateValueCounterAchievementMediator(object value)
    {
        SerializableManager<IEntity>.UpdateValueMultiAchievements(value);
    }
}
