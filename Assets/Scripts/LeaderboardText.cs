using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LeaderboardText : MonoBehaviour
{
    [SerializeField]
    private string pathToScoreFile;

    private Text leaderboardText;

    private void Start()
    {
        
        leaderboardText = GetComponent<Text>();
        leaderboardText.text = ReadString();
    }

    public string ReadString()
    {
        string path = Application.dataPath + "/" + pathToScoreFile;
        StreamReader reader = new StreamReader(path);
        var leadersFromFile = reader.ReadToEnd();
        if (leadersFromFile.Length < 3)
        {
            Debug.Log("Записей в файле нет, длина файла - " + leadersFromFile.Length);
            leadersFromFile = "Пока записей нет!";
        }
        Debug.Log(leadersFromFile);
        reader.Close();
        return leadersFromFile;
    }


}
