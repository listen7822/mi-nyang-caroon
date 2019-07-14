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

    public delegate void Callback(Ingredient ingredient);

    private Callback m_Callback = null;
    private Button m_Button = null;
    [SerializeField]
    protected INGREDIENT_TYPE m_Type = INGREDIENT_TYPE.NONE;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ingredient");
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("Click");
        m_Callback(this);
    }

    public INGREDIENT_TYPE GetIngredientType()
    {
        return m_Type;
    }

    public void SetIngredientType(INGREDIENT_TYPE type)
    {
        m_Type = type;
    }

    public void SetCallback(Callback func)
    {
        m_Callback += func;
    }
}
