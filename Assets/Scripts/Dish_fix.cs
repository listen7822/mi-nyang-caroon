using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dish_fix : MonoBehaviour
{
    public bool IsSelected;

    public Food createdFood;

    private Button button;

    public Image[] images;

    public Image createdFoodImg;

    private bool[] setIng = new bool[3];

    public int? foodNum;

    
    public void ResetDish()
    {
        createdFood = null;
        IsSelected = false;
        foodNum = null;
        createdFoodImg.sprite = null;
        createdFoodImg.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            images[i].sprite = null;
            images[i].gameObject.SetActive(false);
            setIng[i] = false;
        }

    }

    public void SetDish(int _foodNum , int _typeNum)
    {
        if(foodNum == null)
        {
            foodNum = _foodNum;
        }
        else if(foodNum != _foodNum)
        {
            return;
        }

        setIng[_typeNum -1] = true;
        images[_typeNum -1].sprite = FoodIngredientsManager.Instance().GetImage(_foodNum, _typeNum);
        images[_typeNum -1].gameObject.SetActive(true);

        CheckDish();
    }

    public void CheckDish()
    {
        for (int i = 0; i < 3; i++)
        {
            if(!setIng[i])
            {
                return;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            images[i].sprite = null;
            images[i].gameObject.SetActive(false);
        }

        createdFoodImg.sprite = FoodIngredientsManager.Instance().GetImage((int)foodNum,0);
        createdFoodImg.gameObject.SetActive(true);
        createdFood = FoodIngredientsManager.Instance().GetFoodInfo((int)foodNum);

        DishManager_fix.Instance().CheckOrder();
    }


}
