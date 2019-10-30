using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{

    public GameObject dishObj;
    public Image foodImage;
    public Food orderfood;
    public Guest sitGuest;
    public int TableNum;
    
    public Transform seat;

    public bool IsEmpty = true;

    void Start()
    {
        GameObject foods = this.transform.Find("Foods").gameObject;

        GameObject guestManager = GameObject.Find("TableManager") as GameObject;
    }

    public void Reset()
    {
        IsEmpty = true;
        dishObj.gameObject.SetActive(false);
        foodImage.sprite = null;
        orderfood = null;
        sitGuest = null;
    }

    public void SetFoodOnTable()
    {
        // 테이블에 음식에 맞는 이미지를 띄운다.
        foodImage.sprite = null;
        foodImage.sprite = FoodIngredientsManager.Instance().GetImage(orderfood.foodNum);
        sitGuest.Eat();
        dishObj.gameObject.SetActive(true);
        orderfood = null;
    }
}
