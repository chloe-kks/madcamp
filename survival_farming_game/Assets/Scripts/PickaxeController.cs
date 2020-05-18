using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : CloseWeaponController
{
    public static bool isActivate = false;


    // Update is called once per frame
    void Update()
    {
        if (isActivate)
            TryAttack();

    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if(hitInfo.transform.tag == "Rock")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining();
                }
                else if (hitInfo.transform.tag == "Twig")
                {
                    hitInfo.transform.GetComponent<Twig>().Damage();
                }
                else if (hitInfo.transform.tag == "NPC")
                {
                    hitInfo.transform.GetComponent<Pig>().Damage(2, transform.position);
                }

               
                else if (hitInfo.transform.tag == "enemy")
                {
                    hitInfo.transform.GetComponent<EnemyScript>().Damage(2, transform.position);
                    //currentCloseWeapon.damage
                }


                isSwing = false;
                // collide
                Debug.Log(hitInfo.transform.name);
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
