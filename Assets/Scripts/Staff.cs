using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public enum STATE
    {
        WAITING = 0,
        SERVING
    }
    public delegate void ServingIsDone(Ingredient.FOOD_TYPE foodType, int tableIndex);

    private STATE m_State = STATE.WAITING;
    private ServingIsDone OnServingIsDone = null;
    // Start is called before the first frame update
    void Start()
    {
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

    public void SetState(STATE state)
    {
        m_State = state;
    }

    public void OnSetServingIsDoneCallback(ServingIsDone func)
    {
        OnServingIsDone += func;
    }

    public void Serving(Ingredient.FOOD_TYPE foodType, int tableIndex)
    {
        // 애니메이션으로 테이블에 도착하면 true 리턴.,
        DeliverFoodToCustomer(foodType, tableIndex);
    }

    public void ReturnToWaitingPos()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(0, 0, 0),
            "isLocal", true, "easeType", iTween.EaseType.linear,
            "oncomplete", "OnArrivedWaitingPos", "oncompletetarget", gameObject));
    }

    public void DeliverFoodToCustomer(Ingredient.FOOD_TYPE foodType, int tableIndex)
    {
        // 테이블의 위치를 물어봐야한다...
        Vector2 pos = TableManager.Instance().GetTablePos(tableIndex);

        Hashtable hashtable = new Hashtable();
        hashtable.Add("foodType", foodType);
        hashtable.Add("tableIndex", tableIndex);

        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(1920 - (660) - (GetComponent<RectTransform>().sizeDelta.x), 0, 0),
            "isLocal", true, "easeType", iTween.EaseType.linear,
            "oncomplete", "OnArriveToCustomer", "oncompletetarget", gameObject, "oncompleteparams", hashtable));

    }

    public void OnArriveToCustomer(Hashtable hashtable)
    {
        Debug.LogWarning("Serving is Done. : " + (Ingredient.FOOD_TYPE)hashtable["foodType"]);
        OnServingIsDone((Ingredient.FOOD_TYPE)hashtable["foodType"], (int)hashtable["tableIndex"]);
    }

    public void OnArrivedWaitingPos()
    {
        m_State = STATE.WAITING;
    }
}
