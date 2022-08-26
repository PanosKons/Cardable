using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SavedData
{
    public uint UnlockedLevel;
    public bool[] UnlockedCards;
    public void Init()
    {
        UnlockedLevel = LevelManager.UnlockedLevel;
    }
}
public class Saving : MonoBehaviour
{
    public static Saving Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Load();
        }
        else
            Destroy(gameObject);
    }

    void Load()
    {
        string path = Application.persistentDataPath + "/game.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SavedData savedData = formatter.Deserialize(stream) as SavedData;
            LevelManager.UnlockedLevel = savedData.UnlockedLevel;
            stream.Close();
        }
        else
        {
            LevelManager.UnlockedLevel = 0;
        }
    }
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        SavedData savedData = new SavedData();
        savedData.Init();
        formatter.Serialize(stream, savedData);
        stream.Close();

    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
