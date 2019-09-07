using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsManager : MonoSingleton<IngredientsManager>
{
    public delegate void SetIngrediant(Ingredient.FOOD_TYPE foodType, Ingredient.INGREDIENT_TYPE ingrediantType);
    private SetIngrediant OnSetIngrediant = null;

    void Awake()
    {
        TrayManager.Instance().SetFinishTraySetting(OnFinishTraySetting);
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    void OnFinishTraySetting()
    {
        Tray[] traies = transform.GetComponentsInChildren<Tray>();
        foreach(Tray tray in traies)
        {
            int nIndex = 0;
            foreach(Transform ingredient in tray.transform)
            {
                if (0 == nIndex % 3)
                {
                    ingredient.gameObject.AddComponent<MacaroonIngrediant>().SetIngredientType(Ingredient.FOOD_TYPE.MACARRON, Ingredient.INGREDIENT_TYPE.TOP);
                }
                else if(1 == nIndex % 3)
                {
                    ingredient.gameObject.AddComponent<MacaroonIngrediant>().SetIngredientType(Ingredient.FOOD_TYPE.MACARRON, Ingredient.INGREDIENT_TYPE.MID);
                }
                else if(2 == nIndex % 3)
                {
                    ingredient.gameObject.AddComponent<MacaroonIngrediant>().SetIngredientType(Ingredient.FOOD_TYPE.MACARRON, Ingredient.INGREDIENT_TYPE.BOT);
                }
                ingredient.gameObject.GetComponent<MacaroonIngrediant>().SetSelectedIngrediantCallback(OnSelectedIngrediant);
                ++nIndex;
            }
        }
    } 

    public void SetIngrediantCallback(SetIngrediant func)
    {
        OnSetIngrediant += func;
    }

    public void OnSelectedIngrediant(Ingredient.FOOD_TYPE foodType, Ingredient.INGREDIENT_TYPE ingrediantType)
    {
        // DishManager에 재료가 선택되었음을 알려준다 OnSetIngrediant.
        OnSetIngrediant(foodType, ingrediantType);
    }
}
