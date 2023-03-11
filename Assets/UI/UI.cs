using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private int m_Score;
    private const string k_FormtToShow = @"Score: {0}";
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text orderText;

    public int Score
    {
        get => m_Score;
        set
        {
            m_Score = Math.Max(0, value);
            updatingText();
        }
    }

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("cont find the scoreText Text element");
        }
        else
        {
            Score = 0; 
        }
        if (gameOverText == null)
        {
            Debug.LogError("cont find the gameOverText Text element");
        }
        else
        {
            gameOverText.enabled = false;
        }
    }

    private void updatingText()
    {
        scoreText.text = string.Format(k_FormtToShow, m_Score);
    }

    public void Playerkilled()
    {
        PlayersFirstShotWasFired(); 
        gameOverText.enabled = true;
    }

    public void PlayersFirstShotWasFired()
    {
        orderText.enabled = false;
    }
}
