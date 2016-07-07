using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Text moneyText;
    public GameObject[] optieButtons;

    Animator anim;
    private ScoreManager scoreManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        scoreManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ScoreManager>();
    }

    //________________
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            int bonus = 0;
            int punten = Convert.ToInt32(Math.Round(ScoreManager.Score / 10f, 0));
            int total = bonus + punten + ScoreManager.KilledEnemies + scoreManager.Money;

            moneyText.text = string.Format("${5}{0}${1}{0}${2}{0}${3}{0}________________${4}", Environment.NewLine, scoreManager.Money, ScoreManager.KilledEnemies, bonus, total, punten);

            anim.SetTrigger("GameOver");

            foreach (GameObject g in optieButtons)
            {
                g.SetActive(true);
            }

            if (File.Exists(ScoreManager.filepath))
            {
                try
                {
                    if (ScoreManager.HiScore < ScoreManager.Score)
                    {
                        ScoreManager.HiScore = ScoreManager.Score;

                        using (Stream stream = File.Open(ScoreManager.filepath, FileMode.OpenOrCreate))
                        {
                            new BinaryFormatter().Serialize(stream, ScoreManager.HiScore);
                        }
                    }
                }
                catch (Exception)
                {
                    ScoreManager.HiScore = 0;
                }
            }
            else
            {
                File.Create(ScoreManager.filepath);
                ScoreManager.HiScore = 0;
            }
        }
    }
}
