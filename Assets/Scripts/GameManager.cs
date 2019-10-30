using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public Slider slider;
    public int stageLevel;
    public GameObject popup;
    public Button popupBtn;
    // Start is called before the first frame update
    
    private void ResetGame()
    {

    }
    private void Start()
    {
        popupBtn.onClick.AddListener(()=>{popup.gameObject.SetActive(false); StartGame();});
    }

    //스테이지 형식으로 하기 위해, 전체 리셋 후, 스폰, 타임 등을 설정해 주어야 함
    public void StartGame()
    {
        ResetGame();
        StartCoroutine(GuestManager.Instance().GuestSpawn());
    }

    //게임 스테이지 타이머 (시간 및 표시용 기획 필요)
    private IEnumerator StartCount(float stage)
    {
        float GameTime = stage * 120f;
        slider.maxValue = slider.value = GameTime;
        while (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
            yield return null;
        }
        EndGame();
    }

    public void EndGame()
    {

    }

}
