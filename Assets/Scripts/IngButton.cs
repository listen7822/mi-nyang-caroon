using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngButton : MonoBehaviour
{

    private Image btnSprite;

    private Food foodinfo;

    private Button button;

    int foodNum;
    int TypeNum;

    public enum Type
    {
        None = 0,
        Top = 1,
        Middle = 2,
        Bot = 3,
    }


    // Start is called before the first frame update
    void Awake()
    {
        btnSprite = GetComponent<Image>(); 
        GetComponent<Button>().onClick.AddListener(SetDish);
    }

    public void ChangeButton(Sprite _sprite)
    {
        btnSprite.sprite = null;
        btnSprite.sprite = _sprite;
    }

    public void SetDish()
    {
        if(DishManager_fix.Instance().selectedDish ==null)
        {
            return;
        }
        DishManager_fix.Instance().selectedDish.SetDish(foodNum,TypeNum);
        GetRandom(FoodIngredientsManager.Instance().foodList.Count);
    }

    public void GetRandom(int _foodCount)
    {
        foodNum = Random.Range(0,_foodCount);
        TypeNum = Random.Range(1,4);

        ChangeButton(FoodIngredientsManager.Instance().GetImage(foodNum,TypeNum));
    }
}
