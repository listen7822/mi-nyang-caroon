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

    public delegate void Callback(Dish dish);

    private Callback m_Callback = null;
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
            foreach(Transform ingrediant in tray.transform)
            {
                ingrediant.GetComponent<Ingredient>().SetCallback(OnSettingIngredient);
            }
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
        m_Callback(this);
    }

    // Public region.
    public void OnChooseFoodType()
    {
        m_FoodType = FOOD_TYPE.MACAROON;
    }

    public void OnSettingIngredient(Ingredient ingredient)
    {
        if(DISH_STATE.ACTIVE != m_State)
        {
            return;
        }

        Debug.Log("Ingrediant name : " + ingredient.GetType());
        m_Ingredients.Add(ingredient);
        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            Debug.Log("food name : " + food.name);
            //if(ingredient.GetType() == )
            //{
                food.gameObject.SetActive(true);
            //}
        }
    }


    public void SetCallback(Callback func)
    {
        m_Callback += func;
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

    public void Clear()
    {
        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            food.gameObject.SetActive(false);
        }
        m_Ingredients.Clear();
    }
}
