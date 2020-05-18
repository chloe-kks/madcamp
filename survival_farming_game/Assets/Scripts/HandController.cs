using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HandController : CloseWeaponController
{
    //활성화 여부
    public static bool isActivate = false;

    [SerializeField]
    private GameObject[] Carrot_jip;

    private GameObject Carrot_Fruit;
    private GameObject Dirt_Pile;
    private GameObject Carrot_Plant;
    private GameObject Cube;

    private int no_double = 0;


    private int timer = 0;

    /*
    private Dictionary<int,GameObject> Alive_Dirty_Pile = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> Alive_Carrot_Plant = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> Alive_Carrot_Fruit = new Dictionary<int, GameObject>();
    */
    private ArrayList Alive_Dirty_Pile1 = new ArrayList();
    private ArrayList Alive_Dirty_Pile2 = new ArrayList();
    private ArrayList Alive_Carrot_Plant = new ArrayList();
    private ArrayList Alive_Carrot_Fruit = new ArrayList();



    void Start()
    {

        Carrot_Fruit = Carrot_jip[0];

        Carrot_Plant = Carrot_jip[2];


        Dirt_Pile = Carrot_jip[1];
        Cube = Carrot_jip[3];


    }



    void Update()
    {

        if (no_double > 0)
        {
            no_double = no_double - 1;
        }

        timer = timer + 1;

        if (isActivate)
        {
            TryAttack();

            if (no_double == 0)
            {
                MakingCarrot();
            }
        }

    }

    private void MakingCarrot()
    {
        no_double = 30;
        float randomX = Random.Range(-0.5f, 0.5f);
        int checkTime;
        GameObject checkGameObject;
        Vector3 checkposition;
        Vector3 checkposition2;

        if (Input.GetButton("Fire2"))
        {
            GameObject dirty_pile = (GameObject)Instantiate(Dirt_Pile, new Vector3(transform.position.x + 1, 10, transform.position.z), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
            Alive_Dirty_Pile1.Add(timer);
            Alive_Dirty_Pile2.Add(dirty_pile);
        }

        for (int i = 0; i < Alive_Dirty_Pile1.Count; i++)
        {
            if (timer - (int)Alive_Dirty_Pile1[i] == 300)
            {
                checkTime = (int)Alive_Dirty_Pile1[i];
                checkGameObject = (GameObject)Alive_Dirty_Pile2[i];
                checkposition = checkGameObject.transform.position;
                GameObject carrot_plant = (GameObject)Instantiate(Carrot_Plant, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                Alive_Dirty_Pile2[i] = carrot_plant;
                Destroy(checkGameObject);
            }
        }

        for (int i = 0; i < Alive_Dirty_Pile1.Count; i++)
        {
            if (timer - (int)Alive_Dirty_Pile1[i] == 600)
            {
                checkTime = (int)Alive_Dirty_Pile1[i];
                checkGameObject = (GameObject)Alive_Dirty_Pile2[i];
                checkposition = checkGameObject.transform.position;
                GameObject carrot_fruit = (GameObject)Instantiate(Carrot_Fruit, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                Alive_Dirty_Pile2[i] = Cube;
                Destroy(checkGameObject);
            }
        }






        /*
        if (Input.GetButton("Fire2"))
        {
            GameObject dirty_pile = (GameObject)Instantiate(Dirt_Pile, new Vector3(transform.position.x+1, 1, transform.position.z), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
            Alive_Dirty_Pile.Add(timer,dirty_pile);
            Debug.Log(Alive_Dirty_Pile.Count);
        }
        
        
        for (int i = 0; i < Alive_Dirty_Pile.Count; i++) {
            var keys = new List<int>(Alive_Dirty_Pile.Keys);
            checkTime = keys[i];
            if (timer - checkTime >= 300) {
                checkGameObject = Alive_Dirty_Pile[keys[i]];
                checkposition = checkGameObject.transform.position;

                //Destroy(checkGameObject);
                GameObject carrot_plant = (GameObject)Instantiate(Carrot_Plant, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                Alive_Dirty_Pile[keys[i]] = carrot_plant;
            }
        }


        for (int i = 0; i < Alive_Dirty_Pile.Count; i++)
        {
            var keys = new List<int>(Alive_Dirty_Pile.Keys);
            checkTime = keys[i];
            if (timer - checkTime >= 600)
            {
                checkGameObject = Alive_Dirty_Pile[keys[i]];
                checkposition = checkGameObject.transform.position;
                //Destroy(checkGameObject);
                GameObject carrot_fruit = (GameObject)Instantiate(Carrot_Fruit, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                Alive_Dirty_Pile[keys[i]] = carrot_fruit;
            }
        }
        */



    }







    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                if (hitInfo.transform.tag == "Twig")
                {
                    hitInfo.transform.GetComponent<Twig>().Damage();
                }
                else if (hitInfo.transform.tag == "NPC")
                {
                    hitInfo.transform.GetComponent<Pig>().Damage(1, transform.position);
                    //currentCloseWeapon.damage
                }
                else if (hitInfo.transform.tag == "enemy")
                {
                    hitInfo.transform.GetComponent<EnemyScript>().Damage(1, transform.position);
                    //currentCloseWeapon.damage
                }

            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;
    }


}
