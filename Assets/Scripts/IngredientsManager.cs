using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsManager : MonoBehaviour
{
    const int OFFSET = 10;
    const int MAX_COUNT = 7;
    public GameObject m_Ingredient;
    public delegate bool Callback(Ingredient ingredient);
    private Callback m_Callback = null;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 traySize;
        Vector2 ingredientSize = m_Ingredient.GetComponent<RectTransform>().sizeDelta;

        traySize = GetComponent<RectTransform>().sizeDelta;
        int nSpaceThatOneObjectCanUse = Mathf.FloorToInt(traySize.x / MAX_COUNT);
        int nCenterOffset = Mathf.FloorToInt((traySize.y - ingredientSize.y) / 2);
        int nIndex = 0;

        for (int i = 0; i < MAX_COUNT; ++i)
        {
            GameObject ingredient = GameObject.Instantiate(m_Ingredient) as GameObject;
            ingredient.transform.localPosition = new Vector2(nIndex * nSpaceThatOneObjectCanUse, -nCenterOffset);
            ingredient.transform.SetParent(this.transform, false);
            if (0 == nIndex % 3)
            {
                ingredient.AddComponent<Macaroon>().SetIngredientType(Ingredient.INGREDIENT_TYPE.TOP);
            }
            else if(1 == nIndex % 3)
            {
                ingredient.AddComponent<Macaroon>().SetIngredientType(Ingredient.INGREDIENT_TYPE.MID);
            }
            else if(2 == nIndex % 3)
            {
                ingredient.AddComponent<Macaroon>().SetIngredientType(Ingredient.INGREDIENT_TYPE.BOT);
            }
            ++nIndex;
        }
    } 

    // Update is called once per frame
    void Update()
    {
                
    }

    public void ChangeIngredient(GameObject ingredient)
    {
        if(false == m_Callback(ingredient.GetComponent<Ingredient>()))
        {
            return;
        }

        Destroy(ingredient.GetComponent<Ingredient>());
        ingredient.AddComponent<Macaroon>().SetIngredientType(Ingredient.INGREDIENT_TYPE.BOT);
    }

    public void SetCallback(Callback func)
    {
        m_Callback += func;
    }

    public void SelectedIngrediant(Ingredient ingredient)
    {
        // DishManager에 재료가 선택되었음을 알려준다 OnSetIngrediant.

    }
}
