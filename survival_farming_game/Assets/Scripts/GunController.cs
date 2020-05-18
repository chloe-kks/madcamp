using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //활성화 여부
    public static bool isActivate = false;

    //현재 장착된 총
    [SerializeField]
    private Gun currentGun;

    private float currentFireRate;


    private bool isReload;
    //효과음
    private AudioSource audioSource;


    //충돌 정보 받아옴
    private RaycastHit hitInfo;

    //필요한 컴포넌트
    [SerializeField]
    private Camera theCam;

    [SerializeField]
    private GameObject hit_effect_prefab;





    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;




    }


    // Update is called once per frame
    void Update()
    {
        if (isActivate)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
        }




    }





    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }




    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }
    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                StartCoroutine(ReloadCoroutine());
            }
        }
    }
    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;//연사속도 재계산



        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();

        Hit();


        Debug.Log("총알 발사함");


    }
    private void Hit()
    {

        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, currentGun.range))
        {
            Debug.Log(hitInfo.transform.name);
            var clone = Instantiate(hit_effect_prefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2);

            if (hitInfo.transform.tag == "NPC")
            {
                hitInfo.transform.GetComponent<Pig>().Damage(3, transform.position);
                //currentCloseWeapon.damage
            }
            if (hitInfo.transform.tag == "enemy")
            {
                hitInfo.transform.GetComponent<EnemyScript>().Damage(3, transform.position);
                //currentCloseWeapon.damage
            }



        }

    }





    IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0)
        {
            currentGun.anim.SetTrigger("Reload");
            isReload = true;

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;



            yield return new WaitForSeconds(currentGun.reloadTime);

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }
            isReload = false;

        }
        else
        {
            Debug.Log("소유한 총알이 없습니다.");
        }

    }


    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;

        }


    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();


    }


    public Gun GetGun()
    {
        return currentGun;
    }

    public void GunChange(Gun _gun)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }
        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;


        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);

        isActivate = true;

    }



}
