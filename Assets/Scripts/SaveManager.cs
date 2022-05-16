using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance; // instance static nesne olu�turuyoruz

    public string Name;
    public int Score;
    
    private void Awake()
    {
        if(Instance != null) // �nce i�ini bo�alt�yoruz
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [Serializable]
    class SaveData
    {
        public string Name;
        public string Score;
    }

    public void SaveScore() // bu metot �a��r�ld���nda instance nesnesi dolmaya ba�layacak
    {
        SaveData data = new SaveData();
        data.Name = Name; 
        data.Score = Score.ToString();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // �zerine yaz�yoruz
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // buradaki veriyi okuyoruz

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Name = data.Name;
            Score = Convert.ToInt32(data.Score);
        }
    }
}
