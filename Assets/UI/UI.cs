using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private static byte m_Hp;
    private static short m_Score;
    private const string k_FormtToShow = @"Score: {0}
life : {1}";
    private static Text s_Text;

    public static short Score
    {
        get => m_Score;
        set
        {
            m_Score = value;
            updatingText();
        }
    }

    public static byte HP
    {
        get => m_Hp;
        set
        {
            m_Hp = value;
            updatingText();
        }
    }

    private void Awake()
    {
        s_Text = GetComponent<Text>();
    }
    private static void updatingText()
    {
        s_Text.text = string.Format(k_FormtToShow, m_Score, m_Hp);
    }
}
