using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIngredientsManager : MonoSingleton<FoodIngredientsManager>
{

    public Sprite[] foodImage;

    public List<Food> foodList = new List<Food>();

    void Awake()
    {
        foodList.Add(CreateFoodInfo("redmac" , "Macaron_top" , "Macaron_mid" ,"Macaron_bot" , 0));
        foodList.Add(CreateFoodInfo("yelmac" , "Macaron_top" , "Macaron_mid" ,"Macaron_bot" , 1));
        foodList.Add(CreateFoodInfo("purmac" , "Macaron_top" , "Macaron_mid" ,"Macaron_bot" , 2));
        foodList.Add(CreateFoodInfo("picmac" , "Macaron_top" , "Macaron_mid" ,"Macaron_bot" , 3));
        foodList.Add(CreateFoodInfo("blurmac" , "Macaron_top" , "Macaron_mid" ,"Macaron_bot" , 4));
        foodList.Add(CreateFoodInfo("Iceflake" , "Iceflake_top" , "Iceflake_mid" ,"Iceflake_bot" , 5));
    }

    public Sprite GetImage(int _num , string name)
    {
        int spriteNum = _num * 4;

        if(name.Contains("top"))
        {
            spriteNum += 1;
        }
        else if(name.Contains("mid"))
        {
            spriteNum += 2;
        }
        else if(name.Contains("bot"))
        {
            spriteNum += 3;
        }
       
        return foodImage[spriteNum];
    }

    public Sprite GetImage(int _num , int _type)
    {
        int spriteNum = _num * 4;

        spriteNum += _type;

        return foodImage[spriteNum];
    }

    public Sprite GetImage(int _num)
    {
        int spriteNum = _num * 4;

        return foodImage[spriteNum];
    }

    public Food GetFoodInfo(int _num)
    {
        return foodList[_num];
    }

    public Food CreateFoodInfo(string _name , string _top , string _mid , string _bot , int _num)
    {
        Food createfood = new Food();
        createfood.name = _name;
        createfood.ingredients_top = _top;
        createfood.ingredients_mid = _mid;
        createfood.ingredients_bot = _bot;
        createfood.foodNum = _num;
        return createfood;
    }
}

public class Food
{
    public string name;

    public string ingredients_top;

    public string ingredients_mid;

    public string ingredients_bot;

    public int foodNum;
}