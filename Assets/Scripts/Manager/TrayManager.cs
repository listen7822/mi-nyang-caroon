using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayManager : MonoSingleton<TrayManager>
{
    private List<GameObject> m_Traies = new List<GameObject>();
    private float m_velocity = 30.0f;
    private FinishTraySetting OnFinishTraySetting = null;

    public GameObject m_Tray;
    public delegate void FinishTraySetting();
    // Start is called before the first frame update
    void Start()
    {
        int nIndex = 0;
        GameObject tray = GameObject.Instantiate(m_Tray) as GameObject;
        tray.transform.localPosition = new Vector2(0, 0);
        m_Traies.Add(tray);
        tray.transform.SetParent(this.transform, false);
        tray.GetComponent<Tray>().SetIngrediant();

        OnFinishTraySetting();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetFinishTraySetting(FinishTraySetting func)
    {
        OnFinishTraySetting += func;
    }
}
