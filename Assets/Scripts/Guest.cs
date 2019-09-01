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
        // 나가기 애니매이션 후 true 리턴.
        Clear();
    }

    public void Clear()
    {
        m_State = STATE.WAITING_OUTSIDE;
        m_TableIndex = 0;
    }

    public void GoToTable(int index)
    {
        // 손님이 선호하는 음식 전달.
        Vector2 pos = TableManager.Instance().GetTablePos(index);
        // pos 위치로 이동하는 애니메이션 후 다도착하면 음식 주문.
        Debug.Log("손님 이동 시작");
        OnPath1(index);
    }

    void OnPath1(int tableIndex)
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(gameObject.transform.localPosition.x, -215, 0),
            "isLocal", true, "easeType", iTween.EaseType.linear,
            "oncomplete", "OnPath2", "oncompletetarget", gameObject, "oncompleteparams", tableIndex));
    }

    void OnPath2(int tableIndex)
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(641, -215, 0),
           "isLocal", true, "easeType", iTween.EaseType.linear,
           "oncomplete", "OnPath3", "oncompletetarget", gameObject, "oncompleteparams", tableIndex));
    }

    void OnPath3(int tableIndex)
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 5, "position", new Vector3(641, -100, 0),
           "isLocal", true, "easeType", iTween.EaseType.linear,
           "oncomplete", "OnArrivedTable", "oncompletetarget", gameObject, "oncompleteparams", tableIndex));
    }

    void OnArrivedTable(int tableIdex)
    {
        Debug.Log("손님이 테이블에 도착했습니다.");
        GuestManager.Instance().ArrivedTable(Ingredient.FOOD_TYPE.MACARRON, tableIdex);
    }
}
