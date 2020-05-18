using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private GameObject go_hit_effect_prefab;

    [SerializeField]
    private GameObject go_grass_prefab;

    [SerializeField]
    private GameObject go_leaf_prefab;

    private Vector3 originRot;
    private Vector3 wantedRot;
    private Vector3 currentRot;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effect_sound;

    [SerializeField]
    private int count;

    [SerializeField]
    private string hit_Sound;
    [SerializeField]
    private string broken_Sound;

    // Start is called before the first frame update
    void Start()
    {
        originRot = transform.rotation.eulerAngles;
        currentRot = originRot;
    }

    public void Damage(Transform _playerTf)
    {
        hp--;

        Hit();

        StartCoroutine(HitSwayCoroutine(_playerTf));

        if (hp <= 0)
        {
            Destruction();
        }
    }

    private void Hit()
    {
        audioSource.clip = effect_sound;
        audioSource.Play();

        GameObject clone = Instantiate(go_hit_effect_prefab, gameObject.GetComponent<BoxCollider>().bounds.center, Quaternion.identity);

        Destroy(clone, destroyTime);
    }

    IEnumerator HitSwayCoroutine(Transform _target)
    {
        Vector3 direction = (_target.position - transform.position).normalized;

        Vector3 rotationDir = Quaternion.LookRotation(direction).eulerAngles;

        CheckDirection(rotationDir);

        yield return null;
    }

    private void CheckDirection(Vector3 _rotationDir)
    {
        Debug.Log(_rotationDir);
    }

    private void Destruction()
    {
        audioSource.clip = effect_sound;
        audioSource.Play();

        for (int i = 0; i < count; i++)
        {
            Instantiate(go_leaf_prefab, go_grass_prefab.transform.position, Quaternion.identity);

        }

        Destroy(go_grass_prefab, destroyTime);

        go_leaf_prefab.SetActive(true);
    }
}
