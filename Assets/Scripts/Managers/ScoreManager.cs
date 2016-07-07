using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Text;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreManager : MonoBehaviour
{
    public static int Score { get; set; }
    public static int HiScore { get; set; }
    public static int KilledEnemies { get; internal set; }
    public int Money
    {
        get
        {
            return _money;
        }
        internal set
        {
            _money = value;
            moneyText.text = "Money: $" + _money.ToString();
        }
    }

    public static string filepath = @"D:\HiScore.txt";

    public Text scoreText;
    public Text hiScoreText;
    public Text moneyText;

    private Text _text;
    private int _money;

    private void ReadFile()
    {
        if (File.Exists(filepath))
        {
            try
            {
                using (Stream stream = File.Open(filepath, FileMode.Open))
                {
                    HiScore = (int)(new BinaryFormatter().Deserialize(stream));
                }
            }
            catch (Exception)
            {
                HiScore = 0;
            }

            hiScoreText.text = "HiScore: " + HiScore;
        }
        else
        {
            File.Create(filepath);
            HiScore = 0;
        }
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
        Score = 0;

        ReadFile();
    }

    private void Update()
    {
        if (Score > HiScore)
        {
            HiScore = Score;
            hiScoreText.color = Color.Lerp(Color.clear, Color.green, 500);
        }

        scoreText.text = "Score: " + Score;
        hiScoreText.text = "HiScore: " + HiScore;
    }
}
