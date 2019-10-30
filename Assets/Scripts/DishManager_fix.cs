using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishManager_fix : MonoSingleton<DishManager_fix>
{
    private Dish_fix[] dishes;

    public Table[] tables;

    public Dish_fix selectedDish;
    // Start is called before the first frame update
    void Awake()
    {
        dishes = GetComponentsInChildren<Dish_fix>();

        for (int i = 0; i < 3; i++)
        {
            int a = i;
            dishes[a].GetComponent<Button>().onClick.AddListener(()=>{SelectDish(a);});
        }
    }

    public void SetFood(int _foodNum , int _TypeNum)
    {
        if(selectedDish == null)
        return;

        selectedDish.SetDish(_foodNum,_TypeNum);
    }

    public void ResetSelect()
    {
        selectedDish = null;
        foreach (var dish in dishes)
        {
            dish.IsSelected = false;
        }
    }

    public void SelectDish(int num)
    {
        ResetSelect();
        selectedDish = dishes[num];
        selectedDish.IsSelected = true;
    }

    //접시에 담겨 있는 음식과 테이블에서 주문한 음식이 동일한지 체크
    public void CheckOrder()
    {
        if(StaffManager.Instance().staff.GetState() == Staff.STATE.SERVING)
        {
            return;
        }
        foreach (var dish in dishes)
        {
            if(dish.createdFood == null)
            {
                continue;
            }
            foreach (var table in tables)
            {
                if(table.orderfood == null)
                {
                    continue;
                }
                else if(table.orderfood == dish.createdFood)
                {
                    ServerFood(dish,table);
                    return;
                }
            }
        }
    }

    private void ServerFood(Dish_fix dish , Table table)
    {
        dish.ResetDish();
        table.sitGuest.ResetGauge();
        TableManager.Instance().GetStaffTablePos(table.TableNum);
    }
}
