using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    private GuestManager m_GuestManager = null;
    public enum STATE
    {
        WAITING_OUTSIDE = 0,
        INSIDE
    };
    private int m_TableIndex = 0;

    private OrderDrink OnOrderDrink = null;
    public Animator animator;

    public delegate void OrderDrink(Drink.TYPE drinkType, int tableIndex);

    private STATE m_State = STATE.WAITING_OUTSIDE;

    IEnumerator countGauge;


    public float waitTime = 100f;

    public Slider slider;

    private int sitTableNum;

    private Table sitTable;

    public GameObject OrderOBJ;
    public Image OrderImage;

    void Start()
    {
        m_GuestManager = gameObject.transform.parent.GetComponent<GuestManager>();
        animator = GetComponent<Animator>();
        countGauge = CountGauge(30f);
    }

    public void Eat()
    {
        //ResetGauge();
        ResetObj();
        animator.SetBool("IsEat",true);
        Invoke("Reset_All" , 5);
    }

    public void Co_MoveToTable(Vector3 first , Vector3 second, Vector3 third, int tableIndex)
    {
        //Debug.Log(first);
        Reset();
        gameObject.SetActive(true);
        StartCoroutine(MoveToTable(first ,second,third,tableIndex));
        sitTableNum = tableIndex;   
    }

    IEnumerator MoveToTable(Vector3 first , Vector3 second, Vector3 third, int tableIndex)
    {
        animator.SetBool("IsFront", true);
        
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 3, "position", first,"isLocal", false, "easeType", iTween.EaseType.linear));
        yield return new WaitForSeconds(3f);
        
        animator.SetBool("IsFront", false);
        animator.SetBool("IsSide", true);
        
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 3, "position", second,"isLocal", false, "easeType", iTween.EaseType.linear));
        yield return new WaitForSeconds(3f);
        
        iTween.MoveTo(gameObject, iTween.Hash("x", 1, "time", 3, "position", third,"isLocal", false, "easeType", iTween.EaseType.linear));
        yield return new WaitForSeconds(3f);

        OnArrivedTable(tableIndex);
    }
    void OnArrivedTable(int tableIdex)
    {
        animator.SetBool("IsSide", false);
        animator.SetBool("IsSeat", true);
        //Debug.Log("손님이 테이블에 도착했습니다.");

        sitTable = TableManager.Instance().tables[tableIdex];
        sitTable.sitGuest = this;
        sitTable.IsEmpty = false;
        StartCoroutine(OrderFoods());
    }

    IEnumerator OrderFoods()
    {
        yield return new WaitForSeconds(1);
        int orderNum = Random.Range(0,4);
        sitTable.orderfood = FoodIngredientsManager.Instance().GetFoodInfo(orderNum);
        OrderImage.sprite = FoodIngredientsManager.Instance().GetImage(orderNum);
        iTween.ScaleTo(OrderOBJ, Vector3.one, 1);
        StartCoroutine(countGauge);
        DishManager_fix.Instance().CheckOrder();
    }

    public IEnumerator CountGauge(float time)
    {
        slider.gameObject.SetActive(true);
        slider.maxValue = slider.value = time;
        while(slider.value > 0)
        {
            slider.value--;
            yield return new WaitForSeconds(1);
        }
        ResetGauge();
        ResetObj();
        Reset_All();
    }

    public void ResetGauge()
    {
        StopCoroutine(countGauge);
        countGauge = CountGauge(30);
        slider.value = slider.maxValue;
        slider.gameObject.SetActive(false);
    }

    void ResetObj()
    {
        OrderOBJ.transform.localScale = Vector3.zero;
    }

    public void Reset_All()
    {
        sitTable.Reset();
        GuestManager.Instance().guestQueue.Enqueue(this);
        Reset();
    }
    public void Reset()
    {
        transform.localPosition = new Vector2(0, 0);
        gameObject.SetActive(false);  
    }
}
