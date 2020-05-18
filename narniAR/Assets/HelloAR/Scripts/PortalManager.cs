using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{
    public GameObject MainCamera;

    public GameObject Sponza;
    public GameObject Skydome;

    public GameObject DoorOpen;
    public GameObject DoorClosed;

    private Material[] SponzaMaterials;
    private Material[] SkydomeMaterials;

    // private Material PortalPlaneMaterial;
    // Start is called before the first frame update
    void Start()
    {
        SponzaMaterials = Sponza.GetComponent<Renderer>().sharedMaterials;
        SkydomeMaterials = Skydome.GetComponent<Renderer>().sharedMaterials;

        //   PortalPlaneMaterial = GetComponent<Renderer>().sharedMaterial;
        DoorClosed.SetActive(true);
        Skydome.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider collider)
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(MainCamera.transform.position);

        //        if(camPositionInPortalSpace.y <=0.0f)
        //        {
        //            for (int i = 0; i < SponzaMaterials.Length; ++i)
        //            {
        //               SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.NotEqual);
        //           }
        //
        //           PortalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Front);
        //       }
        //     else 
        if (camPositionInPortalSpace.y < 1.0f)
        {
            //Disable
            for (int i = 0; i < SponzaMaterials.Length; ++i)
            {
                SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
            }
            Skydome.SetActive(true);
            DoorOpen.SetActive(true);
            DoorClosed.SetActive(false);
            // PortalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Off);

        }
        else
        {
            //Enable
            for (int i = 0; i < SponzaMaterials.Length; ++i)
            {
                SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);

            }
            DoorOpen.SetActive(false);
            DoorClosed.SetActive(true);
            // PortalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Back);

        }
    }
}

