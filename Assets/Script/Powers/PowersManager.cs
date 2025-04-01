using System;
using System.Collections.Generic;
using System.Linq;

public class PowersManager
{
    public event Action<bool> attackAction;
    public static PowersManager Instance;
    public Dictionary<Type, List<IEntity>> dicPowersInterface = new Dictionary<Type, List<IEntity>>();

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

    public void UnregisterPower<T>(IEntity power) where T : class
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

    public void RegisterPowerInterface<T>(IEntity power) where T : class
    {
        if (!ExistPowerInterface<T>())
        {
            List<IEntity> listPowers = new List<IEntity>();
            listPowers.Add(power);
            dicPowersInterface.Add(typeof(T), listPowers);
        }
        else
        {
            if (!dicPowersInterface[typeof(T)].Contains(power))
            {
               IEntity objFind = dicPowersInterface[typeof(T)].Where(o => o.TypePowers.Equals(power.TypePowers)).FirstOrDefault();
                if (objFind is null)
                    dicPowersInterface[typeof(T)].Add(power);
                else
                {
                    dicPowersInterface[typeof(T)].Remove(objFind);
                    dicPowersInterface[typeof(T)].Add(power);
                }
            }
        }
    }

    public int GetValuePower(IEntity.TypeEvents typePower)
    {
        foreach (Type type in dicPowersInterface.Keys)
        {
            foreach (IEntity power in dicPowersInterface[type])
            {
                if (power.TypePowers.Equals(typePower))
                {
                    return power.EffectValuePower;
                }
            }
        }
        return 0;
    }

    public bool CheckPower(IEntity.TypeEvents typePower)
    {
        foreach (Type type in dicPowersInterface.Keys)
        {
            foreach (IEntity power in dicPowersInterface[type])
            {
                if (power.TypePowers.Equals(typePower))
                {
                    if(power.Count == 0)
                    return false;
                    else
                    return true;
                }
            }
        }
        return false;
    }

    public IEntity UpdatePower(IEntity power)
    {
        foreach (Type item in dicPowersInterface.Keys)
        {
            IEntity powerTmp = dicPowersInterface[item].Find(o => o == power);
            if (powerTmp!=null)
            {
                powerTmp.Count += 1;
                dicPowersInterface[item].Remove(power);
                dicPowersInterface[item].Add(powerTmp);

                return powerTmp;
            }
        }
        return null;
    }

    public IEntity DisablePower(IEntity power)
    {
        foreach (Type item in dicPowersInterface.Keys)
        {
            IEntity powerTmp = dicPowersInterface[item].Find(o => o == power);
            if (powerTmp != null)
            {
                powerTmp.Count -= 1;
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

    public List<IEntity> GetRandomPowers()
    {
         
        List<IEntity> randomPowersTmp = new List<IEntity>();
        List<IEntity> randomPowers = new List<IEntity>();
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

    public List<IEntity> GetListPowers()
    {
        List<IEntity> randomPowers = new List<IEntity>();
        foreach (Type key in dicPowersInterface.Keys)
        {
            if (key.Equals(typeof(IPowerEnemy)))
                continue;
            randomPowers.AddRange(dicPowersInterface[key]);
        }
        return randomPowers;
    }

    public IEntity GetPower(IEntity.TypeEvents typePower) 
    {
        foreach (Type type in dicPowersInterface.Keys)
        {
            foreach (IEntity power in dicPowersInterface[type])
            {
                if (power.TypePowers.Equals(typePower))
                {
                    return power;
                }
            }
        }
        return null;
    }
}
