using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0;
    
    private const string DIR_DATA = "/Data/";
    private const string FILE_HIGH_SCORE = "highScore.txt";
    private string PATH_HIGH_SCORE;
    
    public const string PREF_HIGH_SCORE = "hsScore";

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (score > HighScore)
            {
                HighScore = score;
            }
        }
    }

    private int highScore = 2;

    public int HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;
            if (!Directory.Exists(Application.dataPath + DIR_DATA))
            {
                Directory.CreateDirectory(Application.dataPath + DIR_DATA);
            }
            File.WriteAllText(PATH_HIGH_SCORE, "" + highScore);
        }
    }
    
    public int currentLevel = 0;
    public int targetScore = 2;

    public TextMeshPro textMeshPro;

    public GameObject prizePrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PATH_HIGH_SCORE = Application.dataPath + DIR_DATA + FILE_HIGH_SCORE;
        if (File.Exists(PATH_HIGH_SCORE))
        {
            HighScore = Int32.Parse(File.ReadAllText(PATH_HIGH_SCORE));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            File.WriteAllText(PATH_HIGH_SCORE, "0");
        }

        textMeshPro.text =
            "Level: " + (currentLevel + 1) + "\n" +
            "Score: " + score + "\n" +
            "HighScore: " + HighScore;
        
        if (score == targetScore)
        {
            currentLevel++;
            targetScore *= 2;
            SceneManager.LoadScene(currentLevel);
        }
    }
}
