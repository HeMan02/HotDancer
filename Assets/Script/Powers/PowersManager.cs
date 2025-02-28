using System;
using System.Collections.Generic;

public class PowersManager
{
    public event Action<bool> attackAction;
    public static PowersManager Instance;
    public Dictionary<Type, List< PowerInfo<IEntity>>> dicPowersInterface = new Dictionary<Type, List<PowerInfo<IEntity>>>();

    public PowersManager()
    {
        Init();
    }

    public void Init()
    {
        SingletonManager.Instance.RegisterObj<PowersManager>(this);
        Instance = SingletonManager.Instance.GetObjInstance<PowersManager>();
    }
    public bool ExistPowerInterface<T>() where T : class
    {
        if (dicPowersInterface.ContainsKey(typeof(T)))
        {
            if (dicPowersInterface[typeof(T)] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void UnregisterPower<T>(PowerInfo<IEntity> power) where T : class
    {
        if (!ExistPowerInterface<T>())
        {
            // Non esiste, quindi nulla
        }
        else
        {
            if (dicPowersInterface[typeof(T)].Contains(power))
            {
                dicPowersInterface[typeof(T)].Remove(power);
            }
        }
    }

    public void RegisterPowerInterface<T>(PowerInfo<IEntity> power) where T : class
    {
        if (!ExistPowerInterface<T>())
        {
            List<PowerInfo<IEntity>> listPowers = new List<PowerInfo<IEntity>>();
            listPowers.Add(power);
            dicPowersInterface.Add(typeof(T), listPowers);
        }
        else
        {
            if (!dicPowersInterface[typeof(T)].Contains(power))
            {
                dicPowersInterface[typeof(T)].Add(power);
            }
        }
    }

    public int GetValuePower(IEntity.TypeEvents typePower)
    {
        foreach (Type type in dicPowersInterface.Keys)
        {
            foreach (PowerInfo<IEntity> power in dicPowersInterface[type])
            {
                if (power.Entity.TypePowers.Equals(typePower))
                {
                    return power.Entity.EffectValuePower;
                }
            }
        }
        return 0;
    }

    public bool CheckPower(IEntity.TypeEvents typePower)
    {
        foreach (Type type in dicPowersInterface.Keys)
        {
            foreach (PowerInfo<IEntity> power in dicPowersInterface[type])
            {
                if (power.Entity.TypePowers.Equals(typePower))
                {
                    if(power.Entity.Count == 0)
                    return false;
                    else
                    return true;
                }
            }
        }
        return false;
    }

    public PowerInfo<IEntity> UpdatePower(PowerInfo<IEntity> power)
    {
        foreach (Type item in dicPowersInterface.Keys)
        {
            PowerInfo<IEntity> powerTmp = dicPowersInterface[item].Find(o => o.Entity == power.Entity);
            if (powerTmp!=null)
            {
                powerTmp.Entity.Count += 1;
                dicPowersInterface[item].Remove(power);
                dicPowersInterface[item].Add(powerTmp);
                return powerTmp;
            }
        }
        return null;
    }

    public PowerInfo<IEntity> DisablePower(PowerInfo<IEntity> power)
    {
        foreach (Type item in dicPowersInterface.Keys)
        {
            PowerInfo<IEntity> powerTmp = dicPowersInterface[item].Find(o => o.Entity == power.Entity);
            if (powerTmp != null)
            {
                powerTmp.Entity.Count -= 1;
                dicPowersInterface[item].Remove(power);
                dicPowersInterface[item].Add(powerTmp);
                return powerTmp;
            }
        }
        return null;
    }

    public void SetAttack(bool value)
    {
        attackAction?.Invoke(value);
    }

    public List<PowerInfo<IEntity>> GetRandomPowers()
    {
         
        List<PowerInfo<IEntity>> randomPowersTmp = new List<PowerInfo<IEntity>>();
        List<PowerInfo<IEntity>> randomPowers = new List<PowerInfo<IEntity>>();
        int countMaxRandomValue;

        foreach (Type key in dicPowersInterface.Keys)
        {
            randomPowersTmp.AddRange(dicPowersInterface[key]);
        }

        countMaxRandomValue = randomPowersTmp.Count;

        for (int i = 0; i < 3; i++)
        {
            randomPowers.Add(randomPowersTmp[UnityEngine.Random.Range(0, countMaxRandomValue)]);
        }

        return randomPowers;
    }

    public List<PowerInfo<IEntity>> GetListPowers()
    {
        List<PowerInfo<IEntity>> randomPowers = new List<PowerInfo<IEntity>>();
        foreach (Type key in dicPowersInterface.Keys)
        {
            if (key.Equals(typeof(IPowerEnemy)))
                continue;
            randomPowers.AddRange(dicPowersInterface[key]);
        }
        return randomPowers;
    }

    public PowerInfo<IEntity> GetPower(IEntity.TypeEvents typePower) 
    {
        foreach (Type type in dicPowersInterface.Keys)
        {
            foreach (PowerInfo<IEntity> power in dicPowersInterface[type])
            {
                if (power.Entity.TypePowers.Equals(typePower))
                {
                    return power;
                }
            }
        }
        return null;
    }
}
