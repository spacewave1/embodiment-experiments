using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWriter : MonoBehaviour
{
    private static ScoreWriter _instance;
    public static ScoreWriter Instance { get { return _instance; } }

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
    [SerializeField] private string pathToCSV;

    private void Start()
    {
        entries = Procedure.Instance.SceneStates.Select(state => new Entry(state.scoreLabel, state.type)).ToList();
    }

    public void write()
    {
        string testpath1 = Path.Combine(Application.persistentDataPath, "sheet.csv");
        
        System.IO.Directory.CreateDirectory(pathToCSV);
        StreamWriter streamWriter = File.CreateText(testpath1);
        entries.ForEach(entry => streamWriter.Write(string.Format("{0},{1}\n", entry.rowName, entry.scoreBoard.text)));
        streamWriter.Close();
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
}
