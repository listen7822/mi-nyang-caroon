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
    private Food m_OrderedFood;

    private STATE m_State = STATE.WAITING_OUTSIDE;
    // 손님이 가져야하는 상태는?
    // 밖에서 기다리는 상태
    // 입구로 들어오는 상태
    // 테이블까지 이동하는 상태
    // 테이블에 앉은 상태
    // 테이블에서 주문하는 상태
    // 계산 및 만족도 표시 상태
    // 입구로 나가는 상태
    // Start is called before the first frame update
    void Start()
    {
        m_GuestManager = gameObject.transform.parent.GetComponent<GuestManager>();
    }

    public STATE GetState()
    {
        return m_State;
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

    public bool Leave()
    {
        // 나가기 애니매이션 후 true 리턴.
        return true;
    }

    public void Clear()
    {
        m_State = STATE.WAITING_OUTSIDE;
        m_TableIndex = 0;
    }

    public Food GoToTable(int index)
    {
        // 손님이 선호하는 음식 전달.
        Vector2 pos = TableManager.GetTablePos(index);
        // pos 위치로 이동하는 애니메이션 후 다도착하면 음식 주문.
        return new Food();
    }

    void OnEnable()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(gameObject.transform.localPosition.x, -215, 0),
            "isLocal", true, "easeType", iTween.EaseType.linear,
            "oncomplete", "OnPath1", "oncompletetarget", gameObject));
    }

    void OnPath1()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(641, -215, 0),
           "isLocal", true, "easeType", iTween.EaseType.linear,
           "oncomplete", "OnPath2", "oncompletetarget", gameObject));
    }

    void OnPath2()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(641, -100, 0),
           "isLocal", true, "easeType", iTween.EaseType.linear,
           "oncomplete", "OnArriveToCustomer", "oncompletetarget", gameObject));
    }
}
