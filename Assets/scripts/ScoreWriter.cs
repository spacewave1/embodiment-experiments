using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreWriter : MonoBehaviour
{
    private static ScoreWriter _instance;

    public static ScoreWriter Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        else
        {
            _instance = this;
        }
    }

    [SerializeField] private List<Entry> entries;
    [SerializeField] private string _directoryPath;

    private void Start()
    {
        entries = Procedure.Instance.SceneStates.Select(state => new Entry(state.scoreLabel, state.type)).ToList();
    }


    public void write()
    {
#if PLATFORM_ANDROID && !UNITY_EDITOR
        string SavedTextsCompleteFilePath;
        SavedTextsCompleteFilePath = Application.persistentDataPath;
#elif UNITY_EDITOR
        string SavedTextsCompleteFilePath = "Assets/Resources";
#endif
        // set the base file path, then add the directory if it's not there yet
        SavedTextsCompleteFilePath = MakeFolder(SavedTextsCompleteFilePath, _directoryPath);
        if (File.Exists(Path.Combine(SavedTextsCompleteFilePath, "sheet.csv")))
        {
            File.AppendAllText(Path.Combine(SavedTextsCompleteFilePath, "sheet.csv"), string.Format("\nData {0}:\n",
                SceneManager.GetActiveScene().name) +
                String.Join("\n", entries.Select(entry => entry.ToString())) + "\n", System.Text.Encoding.ASCII);
        }
        else
        {
            File.WriteAllText(Path.Combine(SavedTextsCompleteFilePath, "sheet.csv"), string.Format("Data {0}:\n",
                SceneManager.GetActiveScene().name) +
                String.Join("\n", entries.Select(entry => entry.ToString())) + "\n", System.Text.Encoding.ASCII);
        }
    }


    private string MakeFolder(string path, string savedTextsFolder)
    {
        string saveDirectory = path + savedTextsFolder;
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
            Debug.Log("directory created! at: " + path);
        }

        return saveDirectory;
    }
}

[Serializable]
public class Entry
{
    public string rowName;
    public Text scoreBoard;

    public Entry(Text scoreBoard, StateType stateType)
    {
        rowName = stateType.ToString().ToLower();
        this.scoreBoard = scoreBoard;
    }

    public override string ToString()
    {
        return rowName + ", " + scoreBoard.text;
    }
}