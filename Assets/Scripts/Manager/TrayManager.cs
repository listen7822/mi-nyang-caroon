using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayManager : MonoSingleton<TrayManager>
{
    private List<GameObject> m_Traies = new List<GameObject>();
    private float m_velocity = 30.0f;
    private FinishTraySetting OnFinishTraySetting = null;

    public int m_nTrayCnt = 3;
    public GameObject m_Tray;
    public delegate void FinishTraySetting();
    // Start is called before the first frame update
    void Start()
    {
        int nIndex = 0;
        for(int i = 0; i < m_nTrayCnt; ++i)
        {
            ++nIndex;
            GameObject tray = GameObject.Instantiate(m_Tray) as GameObject;
            tray.transform.localPosition = new Vector2(
                GetComponent<RectTransform>().rect.width * nIndex
                , 0
                );
            m_Traies.Add(tray);
            tray.transform.SetParent(this.transform, false);
            tray.GetComponent<Tray>().SetIngrediant();
        }

        OnFinishTraySetting();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < m_nTrayCnt; ++i)
        {
            // 오브젝트 이동.
            m_Traies[i].transform.Translate(Vector2.left * m_velocity * Time.deltaTime);
        }
    }

    public void SetFinishTraySetting(FinishTraySetting func)
    {
        OnFinishTraySetting += func;
    }
}
