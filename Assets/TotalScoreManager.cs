using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreManager : MonoBehaviour
{
    public GameObject scoreObject;

    public Text scoreText;

    public Text countText;

    public Text countMenuText;

    private Button nextBtn;

    private Button staffBtn;

    private Button menuBtn;

    private void Awake()
    {
        nextBtn = scoreObject.transform.GetChild(4).GetChild(0).GetComponent<Button>();
        staffBtn = scoreObject.transform.GetChild(4).GetChild(1).GetComponent<Button>();
        menuBtn = scoreObject.transform.GetChild(4).GetChild(2).GetComponent<Button>();
        
        float AlphaThreshold = 0.1f;

        scoreObject.transform.GetChild(4).GetChild(0).GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        scoreObject.transform.GetChild(4).GetChild(1).GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        scoreObject.transform.GetChild(4).GetChild(2).GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;

        nextBtn.onClick.AddListener(()=>{OnNext();});
        staffBtn.onClick.AddListener(()=>{OnStaffMenu();});
        menuBtn.onClick.AddListener(()=>{OnMenu();});

        OpenScore(1000000,5,10);
    }

    public void OpenScore(int totalScore , int totalCount , int totalMenu)
    {
        OpenScore((float)totalScore , (float)totalCount , (float)totalMenu);
    }

    public void OpenScore(float totalScore , float totalCount , float totalMenu)
    {
        scoreObject.gameObject.SetActive(true);
        StartCoroutine(CountScore(scoreText , totalScore));
        StartCoroutine(CountPerson(countText , totalCount));
        StartCoroutine(CountMenu(countMenuText , totalMenu));
    }

    public void CloseScore()
    {
        ResetScore();
        scoreObject.gameObject.SetActive(false);
    }

    private void ResetScore()
    {
        scoreText.text = countText.text = countMenuText.text = "000000";
    }

    IEnumerator CountScore(Text text, float target)
    {
        float duration = 0.7f; // 카운팅에 걸리는 시간 설정.
        float current = 0;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * Time.deltaTime;
            text.text = string.Format("{0:0,000,000}", current);
            yield return null;
        }

        current = target;
        text.text = string.Format("{0:0,000,000}", current);
    }
    IEnumerator CountPerson(Text text, float target)
    {
        float duration = 0.7f; // 카운팅에 걸리는 시간 설정.
        float current = 0;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * Time.deltaTime;
            text.text = string.Format("{0:00}", current);
            yield return null;
        }

        current = target;
        text.text = string.Format("{0:00}",current);
    }
    IEnumerator CountMenu(Text text, float target)
    {
        float duration = 0.7f; // 카운팅에 걸리는 시간 설정.
        float current = 0;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * Time.deltaTime;
            text.text = string.Format("{0:000}",current);
            yield return null;
        }

        current = target;
        text.text = string.Format("{0:000}",current);
    }

    private void OnNext()
    {
        CloseScore();
    }

    private void  OnStaffMenu()
    {
        CloseScore();
    }

    private void OnMenu()
    {
        CloseScore();
    }

}
