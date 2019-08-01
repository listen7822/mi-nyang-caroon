using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    public enum INGREDIENT_TYPE
    {
        NONE,
        TOP,
        MID,
        BOT
    }


    private Button m_Button = null;
    private IngredientsManager m_IngredientsManager = null;
    [SerializeField]
    protected INGREDIENT_TYPE m_Type = INGREDIENT_TYPE.NONE;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ingredient");
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClick);
        m_IngredientsManager = gameObject.transform.parent.GetComponent<IngredientsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("Click");
        m_IngredientsManager.ChangeIngredient(this.gameObject);
    }

    public INGREDIENT_TYPE GetIngredientType()
    {
        return m_Type;
    }

    public void SetIngredientType(INGREDIENT_TYPE type)
    {
        m_Type = type;

        if(INGREDIENT_TYPE.TOP == type)
        {
            this.GetComponentInChildren<Text>().text = "M_Top";
        }
        else if(INGREDIENT_TYPE.MID == type)
        {
            this.GetComponentInChildren<Text>().text = "M_Mid";
        }
        else if(INGREDIENT_TYPE.BOT == type)
        {
            this.GetComponentInChildren<Text>().text = "M_Bot";
        }
    }
}
