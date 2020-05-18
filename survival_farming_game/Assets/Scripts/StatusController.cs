using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{

    [SerializeField]
    private int hp;
    private int currentHp;

    [SerializeField]
    private int sp;
    private int currentSp;

    [SerializeField]
    private int spIncreaseSpeed;

    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    private bool spUsed;


    [SerializeField]
    private int dp;
    private int currentDp;


    [SerializeField]
    private int hungry;
    private int currentHungry;

    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;


    [SerializeField]
    private int thirsty;
    private int currentThirsty;
    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;

    [SerializeField]
    private int satisfy;
    private int currentSatisfy;

    //필요한 이미지
    [SerializeField]
    private Image[] images_Guage;


    private const int HP = 0, DP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;


    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentSatisfy = satisfy;


    }

    // Update is called once per frame
    void Update()
    {
        Hungry();
        Thirsty();
        GuageUpdate();
    }


    private void Hungry() {
        if (currentHungry > 0)
        {

            if (currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime++;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else {
            Debug.Log("배고픔 수치가 0이 되었습니다.");
        }

    }
    private void Thirsty()
    {
        if (currentThirsty > 0)
        {

            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentThirstyDecreaseTime++;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("배고픔 수치가 0이 되었습니다.");
        }
    }

    private void GuageUpdate() {
        images_Guage[HP].fillAmount =(float) currentHp / hp;

        images_Guage[SP].fillAmount = (float)currentSp / sp;

        images_Guage[DP].fillAmount = (float)currentHp / dp;

        images_Guage[HUNGRY].fillAmount = (float)currentHungry / hungry;

        images_Guage[THIRSTY].fillAmount = (float)currentThirsty / thirsty;

        images_Guage[SATISFY].fillAmount = (float)currentSatisfy / satisfy;
    }



    public void IncreaseHP(int _count) {

        if (currentHp + _count < hp)
        {
            currentHp += _count;
        }
        else {
            currentHp = hp;
        }
    }

    public void DecreaseHP(int _count) {

        currentHp -= _count;
        if (currentHp < 0) {
            Debug.Log("캐릭터의 hp가 0이 되었습니다.");
        }
        
    }

    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry)
        {
            currentHungry += _count;
        }
        else
        {
            currentHungry = hp;
        }
    }

    public void DecreaseHungry(int _count)
    {
        
        if (currentHungry - _count < 0)
        {
            currentHungry=0;
        }
        currentHungry -= _count;
    }





}
