using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    // hand type
    [SerializeField]
    protected CloseWeapon currentCloseWeapon;

    
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;

    [SerializeField]
    private PlayerController thePlayerController;


    protected void TryAttack()
    {
        if(!Inventory.inventoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    if (CheckObject())
                    {
                        if (currentCloseWeapon.isAxe && hitInfo.transform.tag == "Tree")
                        {
                            StartCoroutine(thePlayerController.TreeLookCoroutine(hitInfo.transform.GetComponent<TreeComponent>().GetTreeCenterPosition()));
                            StartCoroutine(AttackCoroutine("Chop", currentCloseWeapon.workDelayA, currentCloseWeapon.workDelayB,
                                currentCloseWeapon.workDelay - currentCloseWeapon.workDelayA - currentCloseWeapon.workDelayB));
                            return;
                        }
                    }
                    // 좌클릭 공격
                    StartCoroutine(AttackCoroutine("Attack", currentCloseWeapon.attackDelayA, currentCloseWeapon.attackDelayB,
                        currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayA - currentCloseWeapon.attackDelayB));
                }
            }
        }
        
    }

    protected IEnumerator AttackCoroutine(string swingType, float _dealyA, float _delayB, float _delayC)
    {
        isAttack = true;
        currentCloseWeapon.anim.SetTrigger(swingType);

        yield return new WaitForSeconds(_dealyA);
        isSwing = true;

        // 공격 활성화 시점
        StartCoroutine(HitCoroutine());


        yield return new WaitForSeconds(_delayB);
        isSwing = false;

        yield return new WaitForSeconds(_delayC);
        isAttack = false;
    }

    protected abstract IEnumerator HitCoroutine();
    

    

    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
        {
            return true;
        }
        return false;
    }

    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }

        currentCloseWeapon = _closeWeapon;

        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;

        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);
    }
}
