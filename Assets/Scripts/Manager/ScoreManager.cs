using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    private Text m_Text;
    private int m_Score;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponentInChildren<Text>();
    }

    public void OnGetPoint(int point)
    {
        m_Score += point;
        m_Text.text = m_Score.ToString();
        Debug.Log("OnGetPoint:" + point);
    }
}
