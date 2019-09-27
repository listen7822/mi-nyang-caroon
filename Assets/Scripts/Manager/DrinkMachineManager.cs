using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMachineManager : MonoSingleton<DrinkMachineManager>
{
    public GameObject m_DrinkMachinePrefab = null;
    public delegate void ReadyDrink(Drink.TYPE drink);

    private GameObject m_DrinkMancine = null;
    private ReadyDrink OnReadyDrinkCallback = null;


    // Start is called before the first frame update
    void Start()
    {
        GameObject drinkMachine = GameObject.Instantiate(m_DrinkMachinePrefab) as GameObject;
        drinkMachine.transform.localPosition = new Vector2(0, 0);
        drinkMachine.transform.SetParent(this.transform, false);
        drinkMachine.GetComponent<DrinkMachine>().SetState(DrinkMachine.STATE.WAITING);
        drinkMachine.GetComponent<DrinkMachine>().SetOnClickDrinkButtonCallback(OnClickDrinkButton);
        m_DrinkMancine = drinkMachine;
    }

    public void OnClickDrinkButton(Drink.TYPE orderedType)
    {
        m_DrinkMancine.GetComponent<DrinkMachine>().SetState(DrinkMachine.STATE.WORKING);
    }

    public void SetReadyDrinkCallback(ReadyDrink func)
    {
        OnReadyDrinkCallback += func;
    }
}
