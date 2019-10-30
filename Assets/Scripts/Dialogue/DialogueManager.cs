using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private GameObject DialogueTab;

    private Image BG;

    private Image BlackBG;

    private Image LeftChar , MiddleChar , RightChar;

    private GameObject LineBG;

    private GameObject NameBG;

    private Text Line;

    private Text Name;

    private Button ClickArea;

    private int CsvNum;

    public List<Dictionary<string,object>> data;

    public float LineSpeed = 0.2f;

    public bool IsReady = false;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DialogueTab = transform.GetChild(0).gameObject;
        BG = DialogueTab.transform.GetChild(0).GetComponent<Image>();
        BlackBG = DialogueTab.transform.GetChild(1).GetComponent<Image>();
        LeftChar = DialogueTab.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        MiddleChar = DialogueTab.transform.GetChild(2).GetChild(1).GetComponent<Image>();
        RightChar = DialogueTab.transform.GetChild(2).GetChild(2).GetComponent<Image>();
        LineBG = DialogueTab.transform.GetChild(3).gameObject;
        NameBG = LineBG.transform.GetChild(1).gameObject;
        Line = LineBG.transform.GetChild(0).GetComponent<Text>();
        Name = NameBG.transform.GetChild(0).GetComponent<Text>();
        ClickArea = DialogueTab.transform.GetChild(4).GetComponent<Button>();
        ClickArea.onClick.AddListener(()=> {ClickArea.gameObject.SetActive(false); StartLine();});

        StartCoroutine(DialogueOpen("SceneSample3"));
    }
    
    //Assets\Resources\Dialogue\Scene 폴더에 있는 csv 이름을 찾아서 읽어옴
    public IEnumerator DialogueOpen(string sceneName)
    {
        CsvNum = 0;
        data = null;

        data = CSVReader.Read("Dialogue/Scene/" + sceneName , this);

        while(!IsReady)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        //data = CSVReader.Read1("Dialogue/Scene/" + sceneName);
        DialogueTab.gameObject.SetActive(true);
        StartLine();
    }

    //csv 스크립트의 Function 내용에 따라 다른 액션을 하도록 함.
    void StartLine()
    {
        if(CsvNum >= data.Count)
        {
            EndDialogue();
            return;
        }
        switch (data[CsvNum]["Function"].ToString())
        {
            case "L" :
            StartCoroutine(ActiveLine());
            break;

            case "S" :
            ActiveSprite();
            break;
            
            case "E" :
            StartCoroutine(ActiveEffect());
            break;
        }
    }

    
    void EndDialogue()
    {
        Debug.Log("Dialogue Is Over");
        
        data = null;

        CsvNum = 0;

        DialogueTab.gameObject.SetActive(false);
        
    }

    IEnumerator ActiveLine()
    {
        Line.text = "";

        if(string.IsNullOrWhiteSpace(data[CsvNum]["Name"].ToString()))
        {
            NameBG.gameObject.SetActive(false);
        }
        else
        {
            NameBG.gameObject.SetActive(true);
            Name.text = data[CsvNum]["Name"].ToString();
        }

        string tempStr = data[CsvNum]["Line"].ToString();
        for (int i = 0; i < tempStr.Length; i++)
        {
            Line.text += tempStr[i];
            yield return new WaitForSeconds(LineSpeed);
        }

        GotoNextLine();
    }

    void ActiveSprite()
    {
        LeftChar.sprite = RightChar.sprite = MiddleChar.sprite = BG.sprite = null;
        BlackBG.gameObject.SetActive(false);


        if (!string.IsNullOrWhiteSpace(data[CsvNum]["LeftChar"].ToString()))
        {
            LeftChar.sprite = Resources.Load("Dialogue/Image/" + data[CsvNum]["LeftChar"].ToString(), typeof(Sprite)) as Sprite;
        }
        else
        {
            LeftChar.gameObject.SetActive(false);
        }

        if(!string.IsNullOrWhiteSpace(data[CsvNum]["RightChar"].ToString()))
        {
            RightChar.sprite = Resources.Load("Dialogue/Image/" + data[CsvNum]["RightChar"].ToString(), typeof(Sprite)) as Sprite;
        }
        else
        {
            RightChar.gameObject.SetActive(false);
        }

        if (!string.IsNullOrWhiteSpace(data[CsvNum]["MiddleChar"].ToString()))
        {
            MiddleChar.sprite = Resources.Load("Dialogue/Image/" + data[CsvNum]["MiddleChar"].ToString(), typeof(Sprite)) as Sprite;
        }
        else
        {
            MiddleChar.gameObject.SetActive(false);
        }

        if (!string.IsNullOrWhiteSpace(data[CsvNum]["BG"].ToString()))
        {
            BG.sprite = Resources.Load("Dialogue/Image/" + data[CsvNum]["BG"].ToString(), typeof(Sprite)) as Sprite;
        }

        GotoNextLine(false);
    }

    IEnumerator ActiveEffect()
    {
        if(!string.IsNullOrWhiteSpace(data[CsvNum]["BlackBG"].ToString()))
        {
            BlackBG.gameObject.SetActive(true);
        }

        if(!string.IsNullOrWhiteSpace(data[CsvNum]["CameraShake"].ToString()))
        {
            float Time = float.Parse(data[CsvNum]["CameraShake"].ToString());
            Debug.Log(Time);
            iTween.ShakePosition(DialogueTab.gameObject , new Vector3(30,30,30) , Time);
        }
    
        yield return null;
        GotoNextLine();
    }

    public void GotoNextLine(bool IsButtonActive = true)
    {
        CsvNum++;

        if(IsButtonActive)
        {
            ClickArea.gameObject.SetActive(true);
        }
        else
        {
            StartLine();
        }
    }
}