using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;

    public string activePlayerName;

    public int playerPointHighscore;
    public string playerNameHighscore;

    //Singleton Instanziert ein Objekt was nicht gelöscht wird beim laden einer Scene.

    private void Awake()
    {
        //Verhindert das beim wiederkehren in eine Scene das Objekt nochmal Instanziert wird.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //Tags Variablen die gespeichert werden sollen

    [System.Serializable]
    class SaveData
    {
        public int playerPointHighscore;
        public string playerNameHighscore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerPointHighscore = playerPointHighscore;
        data.playerNameHighscore = playerNameHighscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerPointHighscore = data.playerPointHighscore;
            playerNameHighscore = data.playerNameHighscore;
        }
    }
}