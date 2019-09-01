using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Text m_Text;
    private int m_Score;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tableManager = GameObject.Find("TableManager") as GameObject;
        foreach (Transform table in tableManager.transform)
        {
            //table.GetComponent<Table>().SetCallback(OnGetPoint);
        }
        m_Text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = m_Score.ToString();
    }

    public void OnGetPoint(int point)
    {
        m_Score += point;
        Debug.Log("OnGetPoint:" + point);
    }
}
