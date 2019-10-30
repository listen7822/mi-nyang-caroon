using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMachine : MonoBehaviour
{
    public enum STATE
    {
        NONE = 0,
        WAITING,
        WORKING,
    };


    private ClickDrinkButton OnClickDrinkButtonCallback= null;
    public delegate void ClickDrinkButton(Drink.TYPE buttonType);

    private List<GameObject> m_DrinkList = new List<GameObject>();
    private DrinkMachine.STATE m_State = DrinkMachine.STATE.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTeaButton()
    {

    }

    public void OnClickCoffeeButton()
    {

    }

    public void SetState(DrinkMachine.STATE state)
    {
        m_State = state;
    }

    public DrinkMachine.STATE GetState()
    {
        return m_State;
    }

    public void SetOnClickDrinkButtonCallback(ClickDrinkButton func)
    {
        OnClickDrinkButtonCallback += func;
    }
}
