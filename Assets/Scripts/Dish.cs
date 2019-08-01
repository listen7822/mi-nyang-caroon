using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    enum FOOD_TYPE
    {
        NONE = 0,
        MACAROON = 1
    }

    enum DISH_STATE
    {
        NONE = 0,
        ACTIVE,
        INACTIVE
    }

    public delegate void CallbackDishManager(Dish dish);
    public delegate void CallbackTable(List<Ingredient> list);

    private CallbackDishManager m_CallbackDishManager = null;
    private CallbackTable m_CallbackTable = null;
    private FOOD_TYPE m_FoodType = 0;
    private List<Ingredient> m_Ingredients = new List<Ingredient>();
    private DISH_STATE m_State = DISH_STATE.NONE;

    // Start is called before the first frame update
    void Start()
    {
        m_State = DISH_STATE.INACTIVE;
        GameObject trayManager = GameObject.Find("TrayManager") as GameObject;
        foreach (Transform tray in trayManager.transform)
        {
            tray.GetComponent<IngredientsManager>().SetCallback(OnSettingIngredient);
        }

        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            food.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        m_CallbackDishManager(this);
    }

    // Public region.
    public void OnChooseFoodType()
    {
        m_FoodType = FOOD_TYPE.MACAROON;
    }

    public bool OnSettingIngredient(Ingredient ingredient)
    {
        if(DISH_STATE.ACTIVE != m_State)
        {
            return false;
        }

        int lastIndex = m_Ingredients.Count - 1;
        if(0 <= lastIndex)
        {
            if (m_Ingredients[lastIndex].GetIngredientType() >= ingredient.GetIngredientType())
            {
                Debug.LogWarning("잘못된 재료를 선택했습니다.");
                return false;
            }
        }

        Debug.Log("Ingrediant name : " + ingredient.GetType());
        m_Ingredients.Add(ingredient);
        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            Debug.Log("food name : " + food.name);
            Debug.Log("Ingredient type : " + ingredient.GetIngredientType().ToString());
            if (false == food.name.Contains(ingredient.GetType().ToString()))
            {
                continue;
            }

            if(food.name.ToUpper().Contains(ingredient.GetIngredientType().ToString()))
            {
                food.gameObject.SetActive(true);
            }
        }
        if(ingredient.GetIngredientType() == Ingredient.INGREDIENT_TYPE.BOT)
        {
            m_CallbackTable(m_Ingredients);
            StartCoroutine(Clear());
        }

        return true;
    }

    public void SetCallbackDishManager(CallbackDishManager func)
    {
        m_CallbackDishManager += func;
    }

    public void SetCallbackTable(CallbackTable func)
    {
        m_CallbackTable += func;
    }

    public void ChangeToActiveState()
    {
        Debug.Log("Active");
        m_State = DISH_STATE.ACTIVE;
    }

    public void ChangeToInActiveState()
    {
        Debug.Log("Inactive");
        m_State = DISH_STATE.INACTIVE;
    }

    public IEnumerator Clear()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            food.gameObject.SetActive(false);
        }
        m_Ingredients.Clear();
    }
}
