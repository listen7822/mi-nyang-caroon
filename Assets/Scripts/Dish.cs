using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public enum DISH_STATE
    {
        NONE = 0,
        ACTIVE,
        INACTIVE
    }
    public delegate void SelectedDish(int dishIndex);

    private Dictionary<Ingredient.FOOD_TYPE, List<Ingredient.INGREDIENT_TYPE>> m_IngrediantDic
        = new Dictionary<Ingredient.FOOD_TYPE, List<Ingredient.INGREDIENT_TYPE>>();
    private DISH_STATE m_State = DISH_STATE.NONE;
    private int m_Index = 0;
    private SelectedDish OnSelectedDishCallback = null;

    // Start is called before the first frame update
    void Start()
    {
        m_State = DISH_STATE.INACTIVE;
        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            food.gameObject.SetActive(false);
        }

        GameObject ingrediants = this.transform.Find("Ingrediants").gameObject;
        foreach(Transform ingrediant in ingrediants.transform)
        {
            ingrediant.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        OnSelectedDishCallback(m_Index);
    }

    public void SetSelectedDishCallback(SelectedDish func)
    {
        OnSelectedDishCallback += func;
    }

    public void SetIndex(int index)
    {
        m_Index = index;
    }

    public int GetIndex()
    {
        return m_Index;
    }

    public bool SetIngrediant(Ingredient.FOOD_TYPE foodType, Ingredient.INGREDIENT_TYPE ingrediantType)
    {
        Debug.LogWarning("SetIngrediant");
        // Check 함수로 뺼까 말까?
        if(DISH_STATE.ACTIVE != m_State)
        {
            return false;
        }

        if(false == m_IngrediantDic.ContainsKey(foodType))
        {
            List<Ingredient.INGREDIENT_TYPE> tmpList = new List<Ingredient.INGREDIENT_TYPE>();
            m_IngrediantDic.Add(foodType, tmpList);
        }

        if(true == m_IngrediantDic[foodType].Contains(ingrediantType))
        {
            Debug.LogWarning("잘못된 재료를 선택했습니다.");
            return false;
        }

        m_IngrediantDic[foodType].Add(ingrediantType);
        GameObject ingrediants = this.transform.Find("Ingrediants").gameObject;
        foreach(Transform ingrediant in ingrediants.transform)
        {
            Debug.LogWarning("ingrediantType1 : " + ingrediantType);
            Debug.LogWarning("ingrediantType2 : " + ingrediant.GetComponent<Ingredient>().GetIngredientType());
            Debug.LogWarning("FoodType1 : " + foodType);
            Debug.LogWarning("FoodType2 : " + ingrediant.GetComponent<Ingredient>().GetFoodType());
            if(ingrediantType != ingrediant.GetComponent<Ingredient>().GetIngredientType())
            {
                continue;
            }

            if(foodType != ingrediant.GetComponent<Ingredient>().GetFoodType())
            {
                continue;
            }

            ingrediant.gameObject.SetActive(true);
        }

        if(3 != m_IngrediantDic[foodType].Count)
        {
            return false;
        }

        Debug.Log("요리가 만들어졌습니다.");

        return true;
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

    public DISH_STATE GetState()
    {
        return m_State;
    }

    public void Clear(Ingredient.FOOD_TYPE foodType)
    {
        GameObject ingrediants = this.transform.Find("Ingrediants").gameObject;
        foreach(Transform ingrediant in ingrediants.transform)
        {
            ingrediant.gameObject.SetActive(false);
        }

        m_IngrediantDic[foodType].Clear();
    }
}
