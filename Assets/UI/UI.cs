using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private int m_Score;
    private const string k_FormtToShow = @"Score: {0}";
    private Text m_Text;

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
        m_Text = GetComponentInChildren<Text>();
        if (m_Text == null)
        {
            Debug.LogError("cont find the Text element");
        }
        else
        {
            Score = 0; 
        }
    }

    private void updatingText()
    {
        m_Text.text = string.Format(k_FormtToShow, m_Score);
    }
}
