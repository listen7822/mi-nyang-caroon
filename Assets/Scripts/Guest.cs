using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    private GuestManager m_GuestManager = null;
    public enum STATE
    {
        WAITING_OUTSIDE = 0,
        INSIDE
    };
    private int m_TableIndex = 0;
    public GameObject OrderOBJ;
    private Food m_OrderedFood;
    private OrderFood OnOrderFood = null;
    private OrderDrink OnOrderDrink = null;
    public Animator animator;
    public delegate void OrderFood(Ingredient.FOOD_TYPE foodType, int tableIndex);
    public delegate void OrderDrink(Drink.TYPE drinkType, int tableIndex);

    private STATE m_State = STATE.WAITING_OUTSIDE;
    // Start is called before the first frame update
    void Start()
    {
        m_GuestManager = gameObject.transform.parent.GetComponent<GuestManager>();
        animator = GetComponent<Animator>();
    }

    public STATE GetState()
    {
        return m_State;
    }

    public void SetState(STATE state)
    {
        m_State = state;
    }

    public int GetTableIndex()
    {
        return m_TableIndex;
    }

    public int Eat()
    {
        // 먹은 음식에 대한 스코어를 리턴해준다.
        return 100;
    }

    public void Leave()
    {
        m_State = STATE.WAITING_OUTSIDE;
        m_TableIndex = 0;
        gameObject.SetActive(false);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Clear()
    {
    }

    public void SetOrderFoodCallback(OrderFood func)
    {
        OnOrderFood += func;
    }

    public void SetOrderDrinkCallback(OrderDrink func)
    {
        OnOrderDrink += func;
    }

    public void GoToTable(int index)
    {
        // 손님이 선호하는 음식 전달.
        Vector2 pos = TableManager.Instance().GetTablePos(index);
        // pos 위치로 이동하는 애니메이션 후 다도착하면 음식 주문.
        Debug.Log("손님 이동 시작");
        m_TableIndex = index;
        OnPath1(index);
    }

    void OnPath1(int tableIndex)
    {
        animator.SetBool("IsFront", true);
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(150, -457, 0),
            "isLocal", true, "easeType", iTween.EaseType.linear,
            "oncomplete", "OnPath2", "oncompletetarget", gameObject, "oncompleteparams", tableIndex));
    }

    void OnPath2(int tableIndex)
    {
        animator.SetBool("IsFront", false);
        animator.SetBool("IsSide", true);
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(764, -457, 0),
           "isLocal", true, "easeType", iTween.EaseType.linear,
           "oncomplete", "OnPath3", "oncompletetarget", gameObject, "oncompleteparams", tableIndex));
    }

    void OnPath3(int tableIndex)
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 1, "position", new Vector3(764, -355, 0),
           "isLocal", true, "easeType", iTween.EaseType.linear,
           "oncomplete", "OnArrivedTable", "oncompletetarget", gameObject, "oncompleteparams", tableIndex));
    }

    void OnArrivedTable(int tableIdex)
    {
        animator.SetBool("IsSide", false);
        animator.SetBool("IsSeat", true);
        Debug.Log("손님이 테이블에 도착했습니다.");
        OnOrderFood(Ingredient.FOOD_TYPE.MACARRON, tableIdex);
        OnOrderDrink(Drink.TYPE.COFFEE, tableIdex);
        iTween.ScaleTo(OrderOBJ, Vector3.one, 1);
    }
}
