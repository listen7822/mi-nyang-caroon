using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public delegate void CallbackTable(List<Ingredient> list);
    public enum STATE
    {
        WAITING = 0,
        SERVING
    }

    private STATE m_State = STATE.WAITING;
    private CallbackTable m_CallbackTable = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dishManager = GameObject.Find("DishManager") as GameObject;
        foreach(Transform dish in dishManager.transform)
        {
            dish.GetComponent<Dish>().SetCallbackStaff(OnDeliverToCustomer);
        }


        //Debug.Log(GetComponent<RectTransform>().sizeDelta.x);
        ////iTween.MoveBy(gameObject, iTween.Hash("x", 1920, "islocal", true, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public STATE GetState()
    {
        return m_State;
    }

    public bool Serving(int tableIndex, Food food)
    {
        // 애니메이션으로 테이블에 도착하면 true 리턴.,

        return true;
    }

    public void OnDeliverToCustomer(List<Ingredient> food)
    {
        List<Ingredient> tmp = new List<Ingredient>(food);
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(1920 - (660) - (GetComponent<RectTransform>().sizeDelta.x), 0, 0),
            "isLocal", true, "easeType", iTween.EaseType.linear,
            "oncomplete", "OnArriveToCustomer", "oncompletetarget", gameObject, "oncompleteparams", tmp));

    }

    public void SetCallbackTable(CallbackTable func)
    {
        m_CallbackTable += func;
    }

    public void OnArriveToCustomer(List<Ingredient> food)
    {
        Debug.LogWarning("food count : " + food.Count);
        m_CallbackTable(food);
    }
}
