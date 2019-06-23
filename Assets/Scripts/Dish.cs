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

    public delegate void Callback(List<Ingredient> food);

    private Callback m_Callback = null;
    private FOOD_TYPE m_FoodType = 0;
    private List<Ingredient> m_Ingredients = new List<Ingredient>();

    // Start is called before the first frame update
    void Start()
    {
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
        m_Callback(m_Ingredients);
        Clear();
    }

    // Public region.
    public void OnChooseFoodType()
    {
        m_FoodType = FOOD_TYPE.MACAROON;
    }

    public void OnSettingIngredient(Ingredient ingredient)
    {
        Debug.Log("Ingrediant name : " + ingredient.NAME);
        m_Ingredients.Add(ingredient);
        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            Debug.Log("food name : " + food.name);
            if(ingredient.NAME == food.name)
            {
                food.gameObject.SetActive(true);
            }
        }
    }


    public void SetCallback(Callback func)
    {
        m_Callback += func;
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
