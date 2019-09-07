using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    public enum INGREDIENT_TYPE
    {
        NONE = 0,
        TOP,
        MID,
        BOT
    };

    public enum FOOD_TYPE
    {
        NONE = 0,
        MACARRON = 1
    };

    public delegate void SelectedIngrediant(Ingredient.FOOD_TYPE foodType, Ingredient.INGREDIENT_TYPE ingrediantType);
    private Button m_Button = null;
    private SelectedIngrediant OnSelectedIngrediant = null;
    [SerializeField]
    protected INGREDIENT_TYPE m_IngrediantType = INGREDIENT_TYPE.NONE;
    [SerializeField]
    protected FOOD_TYPE m_FoodType = FOOD_TYPE.NONE;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log("Ingredient");
        m_Button = GetComponent<Button>();
        if(null == m_Button)
        {
            return;
        }

        m_Button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedIngrediantCallback(SelectedIngrediant func)
    {
        OnSelectedIngrediant += func;
    }
    public void OnClick()
    {
        Debug.Log("Click");
        OnSelectedIngrediant(m_FoodType, m_IngrediantType);
        SetIngredientType(Ingredient.FOOD_TYPE.MACARRON, Ingredient.INGREDIENT_TYPE.BOT);
    }

    public INGREDIENT_TYPE GetIngredientType()
    {
        return m_IngrediantType;
    }

    public void SetIngredientType(FOOD_TYPE foodType,INGREDIENT_TYPE ingrediantType)
    {
        m_IngrediantType = ingrediantType;
        m_FoodType = foodType;

        if(INGREDIENT_TYPE.TOP == ingrediantType)
        {
            this.GetComponentInChildren<Text>().text = "M_Top";
        }
        else if(INGREDIENT_TYPE.MID == ingrediantType)
        {
            this.GetComponentInChildren<Text>().text = "M_Mid";
        }
        else if(INGREDIENT_TYPE.BOT == ingrediantType)
        {
            this.GetComponentInChildren<Text>().text = "M_Bot";
        }
    }

    public FOOD_TYPE GetFoodType()
    {
        return m_FoodType;
    }

    public void SetFoodType(FOOD_TYPE foodType)
    {
        m_FoodType = foodType;
    }
}
