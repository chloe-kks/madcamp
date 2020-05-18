using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twig : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private BoxCollider col;

    [SerializeField]
    private GameObject go_hit_effect_prefab;

    [SerializeField]
    private GameObject go_twig;

    [SerializeField]
    private GameObject go_twig_item_prefab;

    private Vector3 originRot;
    private Vector3 wantedRot;
    private Vector3 currentRot;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effect_sound;

   public void Damage()
    {
        audioSource.clip = effect_sound;
        audioSource.Play();

        var clone = Instantiate(go_hit_effect_prefab, col.bounds.center, Quaternion.identity);

        Destroy(clone, destroyTime);
        hp--;
        if(hp <= 0)
        {
            Destruction();
        }
    }

    /*
    IEnumerator HitSwayCoroutine(Transform _target)
    {
        Vector3 direction = (_target.position - transform.position).normalized;

        Vector3 rotationDir = Quaternion.LookRotation(direction).eulerAngles;

        yield return null;
    }
    */


    private void Destruction()
    {
        audioSource.clip = effect_sound;
        audioSource.Play();

       Instantiate(go_twig_item_prefab, go_twig.transform.position, Quaternion.identity);

       go_twig_item_prefab.SetActive(true);
    }
}
