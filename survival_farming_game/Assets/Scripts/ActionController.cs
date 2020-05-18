using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float range;

    private bool pickupActivated = false;

    [SerializeField]
    private Animator theAnim;


    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionTxt;

    [SerializeField]
    private Inventory theInventory;

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득했습니다.");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
            else if (hitInfo.transform.tag == "Carrot")
            {
                ItemInfoAppear();
            }else if(hitInfo.transform.tag == "Box")
            {
                ItemInfoAppearBox();
            }
        }
        else
            InfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionTxt.gameObject.SetActive(true);
        actionTxt.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "(E키)" + "</color>";

    }

    private void ItemInfoAppearBox()
    {
        pickupActivated = true;
        actionTxt.gameObject.SetActive(true);
        actionTxt.text = "<color=red>" + hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "이다. 열면 안될 것 같다." + "</color>";
    }

    IEnumerator DelayCoroutine()
    {
        yield return null;
    }


    private void InfoDisappear()
    {
        pickupActivated = false;
        actionTxt.gameObject.SetActive(false);
    }
}
