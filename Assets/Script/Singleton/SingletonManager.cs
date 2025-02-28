using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager
{

    public static SingletonManager Instance;

    private static Dictionary<Type, object> dicSingleton = new Dictionary<Type, object>();

    public SingletonManager()
    {
        dicSingleton.Clear();
        Instance = this;
    }

    public void RegisterObj<T>(T instance) where T : class
    {
        if (!ExistClassDicSingleton<T>())
        {
            dicSingleton.Add(typeof(T), instance);
        }
        else
            Debug.Log("Errore: classe già presente nel Dic Singleton");
    }

    public bool ExistClassDicSingleton<T>() where T : class
    {
        if (dicSingleton.ContainsKey(typeof(T)))
        {
            if (dicSingleton[typeof(T)] != null)
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

    public T GetObjInstance<T>() where T : class
    {
        if (!ExistClassDicSingleton<T>())
        {
            Debug.Log("Errore: classe non presente nel Dic Singleton");
            return null;
        }
        else
            return (T)dicSingleton[typeof(T)];
    }

}
