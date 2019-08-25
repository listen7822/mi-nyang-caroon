using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceManager : MonoBehaviour
{
    // 손님으로 부터 이벤트를 받아서 열고 닫자.
    // Start is called before the first frame update
    public GameObject m_Guest;
    private const int GUEST_COUNT = 4;
    private List<GameObject> m_Children = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // int count = Mathf.FloorToInt(m_Size.x / (prefabSizeOfX + OFFSET));
        for (int i = 0; i < GUEST_COUNT; ++i)
        {
            GameObject child = GameObject.Instantiate(m_Guest) as GameObject;
            child.transform.position = new Vector2(0, 0);
            child.transform.SetParent(this.transform, false);
            child.SetActive(false);
            m_Children.Add(child);
        }

        StartCoroutine(ActiveGuest());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Vector2 GetPos()
    {
        // 입구 위치 리턴.
        return new Vector2();
    }

    private IEnumerator ActiveGuest()
    {
        yield return new WaitForSeconds(3.0f);
        m_Children[0].SetActive(true);
    }
}
