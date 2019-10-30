using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMachineManager : MonoSingleton<DrinkMachineManager>
{
    public GameObject m_DrinkMachinePrefab = null;
    public delegate void ReadyDrink(Drink.TYPE drink);

    private GameObject m_DrinkMancine = null;
    private ReadyDrink OnReadyDrinkCallback = null;
    
    public void OnClickDrinkButton(Drink.TYPE orderedType)
    {
        m_DrinkMancine.GetComponent<DrinkMachine>().SetState(DrinkMachine.STATE.WORKING);
    }

    public void SetReadyDrinkCallback(ReadyDrink func)
    {
        OnReadyDrinkCallback += func;
    }
}
