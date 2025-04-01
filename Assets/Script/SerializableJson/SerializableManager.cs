using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public static class SerializableManager<T> where T : class
{
    private static string pathOutputJson = Application.persistentDataPath;
    private static Dictionary<Type, SaveData<T>> dicSerializableObj = new Dictionary<Type, SaveData<T>>();
    private static List<object> listObj = new List<object>();

    // Application.persistentDataPath per path in build

    public interface ISerializable
    {
        object GetObj();
        void SetObj(object obj);
    }

    [Serializable]
    public class SaveData<T> : ISerializable where T : class
    {
        public object GetObj()
        {
            return data;
        }

        public void SetObj(object obj)
        {
            data = (T)obj;
        }

        [SerializeReference]
        public T data;
    }

    [Serializable]
    [SerializeField]
    public class ListSaveData<T> where T : class
    {
        [SerializeField]
        public List<SaveData<T>> listData;
    }

    public static void RegisterObj(T data)
    {
        if (!ExistId(data.GetType()))
        {
            SaveData<T> sd = new SaveData<T>();
            sd.data = data;
            dicSerializableObj.Add(data.GetType(), sd);
        }
        else
        {
            SaveData<T> sd = new SaveData<T>();
            sd.data = data;
            dicSerializableObj.Remove(data.GetType());
            dicSerializableObj.Add(data.GetType(), sd);
            //Debug.Log("Oggetto già presente nel dictionary");
        }
    }

    public static bool ExistId(Type key)
    {
        if (dicSerializableObj.ContainsKey(key))
            return true;
        else
            return false;
    }

    public static List<object> GenerationObjSaved()
    {
        if (!File.Exists(pathOutputJson + "/Achievement.json"))
            return null;

        string jsonString = File.ReadAllText(pathOutputJson + "/Achievement.json");
        ListSaveData<T> objSaved = JsonUtility.FromJson<ListSaveData<T>>(jsonString);

        listObj = new List<object>();
        // Istanzio oggetto ma non serve, devo richiamare action
        for (int i = 0; i < objSaved.listData.Count; i++)
        {
            listObj.Add(objSaved.listData[i].GetObj());
        }

        //da  qua richiamare evento attivazione achievemnts
        return listObj;
    }

    public static void GenerateJsonData()
    {
        //Debug.LogError("PATH: " + pathOutputJson);
        ListSaveData<T> list = new ListSaveData<T>();
        list.listData = new List<SaveData<T>>();
        foreach (Type key in dicSerializableObj.Keys)
        {
            SaveData<T> sd = new SaveData<T>();
            sd = dicSerializableObj[key];
            list.listData.Add(sd);
        }

        string json = JsonUtility.ToJson(list);

        if (File.Exists(pathOutputJson + "/Achievement.json"))
            File.Delete(pathOutputJson + "/Achievement.json");
        File.WriteAllText(pathOutputJson + "/Achievement.json", json);
    }

    public static List<string> GetListAchievements()
    {
        SaveData<T> sd = new SaveData<T>();

        List<string> arrayAchievemnts = new List<string>();
        foreach (Type key in dicSerializableObj.Keys)
        {
            arrayAchievemnts.Add(key.ToString());
        }
        return arrayAchievemnts;
    }

    public static List<object> GetListAchievementsObj()
    {
        List<object> arrayAchievemnts = new List<object>();
        foreach (Type key in dicSerializableObj.Keys)
        {
            SaveData<T> sd = new SaveData<T>();
            sd = dicSerializableObj[key];
            arrayAchievemnts.Add(sd.GetObj());
        }
        return arrayAchievemnts;
    }

    public static void RemoveAchievement<T>()
    {
        if (!ExistId(typeof(T)))
        {
            dicSerializableObj.Remove(typeof(T));
        }
    }

    public static void UpdateValueMultiAchievements(object value)
    {
        List<Type> keys = new List<Type>(dicSerializableObj.Keys);

        foreach (Type key in keys)
        {
            SaveData<T> sd = dicSerializableObj[key];
            IEntity achievement = (IEntity)sd.GetObj();
            achievement.CounterUnlock += (int)value;
            sd.SetObj(achievement);
            dicSerializableObj[key] = sd;
            GenerateJsonData();
        }
    }
}
