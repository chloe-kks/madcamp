using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{

    [SerializeField] private float secondPerRealTimeSecond;

    private bool isNight=false;

    [SerializeField] private float nightFogDensity;
    private float dayFogDensity;

    [SerializeField]
    private GameObject robot;
    [SerializeField]
    private GameObject pig;
    [SerializeField]
    private GameObject Cube;

    private Vector3 checkposition;

    private int a = 0;

    // Update is called once per frame
    void Update()
    {
        a = a + 1;
        

        Checking();

        if (a % 1800 == 0)
        {
            if (isNight)
            {
                checkposition = Cube.transform.position;
                GameObject robot1 = (GameObject)Instantiate(robot, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.

                GameObject pig1 = (GameObject)Instantiate(pig, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.



            }
        }
        

    }

    private void Checking() {

        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        if (transform.eulerAngles.x >= 170 )
        {
            isNight = true;
            Debug.Log("밤");

        }
        else if (transform.eulerAngles.x >= 0)
        {
            Debug.Log("낮");
            isNight = false;
        }
        else
        {
            isNight = true;
        }
    }




}
