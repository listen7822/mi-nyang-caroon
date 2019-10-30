using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrayControll : MonoBehaviour
{
    private IngButton[] buttonList = null;

    public Button btn;

    private int buttonNum = 3;

    void Start()
    {
        buttonList = GetComponentsInChildren<IngButton>();
        buttonNum = (FoodIngredientsManager.Instance().foodList.Count);  
        MakeIng();
        btn.onClick.AddListener(MakeIng);
    }

    public void MakeIng()
    {
        foreach (var btn in buttonList)
        {
            btn.GetRandom(buttonNum);
        }
    }
}
