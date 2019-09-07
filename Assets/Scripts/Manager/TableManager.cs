using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoSingleton<TableManager>
{
    const int X_TABLE_COUNT = 2;
    const int Y_TABLE_COUNT = 2;
    private static List<GameObject> m_Tables = new List<GameObject>();
    private ArrivedFood OnArrivedFood = null;
    private AvailableTable OnAvailableTable = null;
    public GameObject m_Table;
    public delegate void ArrivedFood(int tableIndex);
    public delegate void AvailableTable(int tableIndex);
    // Start is called before the first frame update
    void Start()
    {
        Vector2 hallSize = GetComponent<RectTransform>().sizeDelta;
        Vector2 spaceThatOneObjectCanUse = new Vector2(hallSize.x / X_TABLE_COUNT, hallSize.y / Y_TABLE_COUNT );
        int indexX = 0;
        int indexY = 0;
        int tableIndex = 1;

        for (int i = 0; i < Y_TABLE_COUNT; ++i)
        {
            for(int j = 0; j < X_TABLE_COUNT; ++j)
            {
                GameObject table = GameObject.Instantiate(m_Table) as GameObject;
                table.transform.localPosition = new Vector2(indexX * spaceThatOneObjectCanUse.x, -indexY * spaceThatOneObjectCanUse.y);
                table.transform.SetParent(this.transform, false);
                table.GetComponent<Table>().SetIndex(tableIndex);
                table.GetComponent<Table>().SetState(Table.STATE.EMPTY);
                m_Tables.Add(table);
                ++indexX;
                ++tableIndex;
            }
            indexX = 0;
            ++indexY;
        }

        StaffManager.Instance().SetServedCallback(OnServed);
        GuestManager.Instance().SetFinishToEatCallback(OnFinishToEat);
        StartCoroutine(CheckTableTimer());
    }

    private IEnumerator CheckTableTimer()
    {
        while(true)
        {
            foreach(GameObject table in m_Tables)
            {
                yield return new WaitForSeconds(3.0f);
                if(Table.STATE.EMPTY == table.GetComponent<Table>().GetState())
                {
                    Debug.Log("AvailableTable : " + table.GetComponent<Table>().GetIndex());
                    table.GetComponent<Table>().SetState(Table.STATE.USED);
                    OnAvailableTable(table.GetComponent<Table>().GetIndex());
                    break;
                }
            }
        }
    }

    public void OnServed(Ingredient.FOOD_TYPE foodType, int tableIndex)
    {
        foreach(GameObject table in m_Tables)
        {
            if(true == table.GetComponent<Table>().SetFoodOnTable(foodType))
            {
                // GuestManager에게 음식이 도착했음을 알린다.
                OnArrivedFood(tableIndex);
                break;
            }
        }
    }


    public void SetOnArrivedFoodCallback(ArrivedFood func)
    {
        OnArrivedFood += func;
    }

    public void SetOnAvailableTableCallback(AvailableTable func)
    {
        OnAvailableTable += func;
    }

    public void OnFinishToEat(int tableIndex)
    {
        foreach(GameObject table in m_Tables)
        {
            if(tableIndex == table.GetComponent<Table>().GetIndex())
            {
                table.GetComponent<Table>().ClearTable();
            }
        }
    }

    public Vector2 GetTablePos(int tableIndex)
    {
        foreach(GameObject table in m_Tables)
        {
            if(tableIndex == table.GetComponent<Table>().GetIndex())
            {
                return table.GetComponent<Transform>().localPosition;
            }
        }

        return new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
