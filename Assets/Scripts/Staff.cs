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

    public Animator animator;
    private STATE m_State = STATE.WAITING;
    public Vector3 staffPos;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public STATE GetState()
    {
        return m_State;
    }

    public void Co_MoveToTable(Vector3 first, Vector3 second, int tableIndex)
    {
        m_State = STATE.SERVING;
        StartCoroutine(MoveToTable(first, second, tableIndex));
    }

    //TableManager의 Route위치를 이용해 해당 테이블 근처로 이동하는 코드(수정 예정)
    IEnumerator MoveToTable(Vector3 first, Vector3 second, int tableIndex)
    {
        animator.SetBool("IsServe", true);

        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 3, "position", first, "isLocal", false, "easeType", iTween.EaseType.linear));
        yield return new WaitForSeconds(3f);

        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 3, "position", second, "isLocal", false, "easeType", iTween.EaseType.linear));
        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(1f);

        TableManager.Instance().tables[tableIndex].SetFoodOnTable();

        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);

        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 3, "position", first, "isLocal", false, "easeType", iTween.EaseType.linear));
        yield return new WaitForSeconds(3f);
        
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 3, "position", new Vector3(-830, 0, 0), "isLocal", true, "easeType", iTween.EaseType.linear));
        yield return new WaitForSeconds(3f);
        
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        
        animator.SetBool("IsServe", false);
        animator.SetBool("IsWait", true);
        
        m_State = STATE.WAITING;
        DishManager_fix.Instance().CheckOrder();
    }
}
