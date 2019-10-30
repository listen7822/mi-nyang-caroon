using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoSingleton<GuestManager>
{
    public GameObject m_Guest;
    private const int GUEST_COUNT = 1;
    private List<GameObject> m_Guests = new List<GameObject>();
    private FinishToEatCallback OnFinishToEatCallback = null;
    private GetOrderDrinkCallback OnGetOrderDrinkCallback = null;
    public delegate void FinishToEatCallback(int tableIndex);
    public delegate void GetOrderDrinkCallback(Drink.TYPE drinkType, int tableIndex);

    public GameObject Door;

    public Guest[] guests;

    public Queue<Guest> guestQueue = new Queue<Guest>();

    void Start()
    {
        guests = transform.GetComponentsInChildren<Guest>();
        for (int i = 0; i < Door.transform.childCount; i++)
        {
            Guest guest = Door.transform.GetChild(i).GetComponent<Guest>();
            guestQueue.Enqueue(guest);
        }
    }

    public void OnOrederDrink(Drink.TYPE drink, int tableIndex)
    {
        OnGetOrderDrinkCallback(drink, tableIndex);
    }

    //테이블이 비어있을 경우, 캐릭터를 생성해주는 루틴 (큐 이용한 오브젝트 풀로 보완 예정)
    public IEnumerator GuestSpawn()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(10.0f);
        
        Table table;
         while(true)
        {
            table = TableManager.Instance().GetEmptyTable();
            if(table != null)
            {
                Guest guest = guestQueue.Dequeue();
                //Debug.Log(table.TableNum + ""+ guest);
                TableManager.Instance().GetTablePos(table.TableNum , guest);
                if(table.TableNum <2)
                guest.transform.SetAsFirstSibling();
                else
                guest.transform.SetAsLastSibling();
            }
            yield return waitForSeconds;

       }
        
    }
}
