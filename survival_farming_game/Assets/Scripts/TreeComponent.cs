using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeComponent : MonoBehaviour
{

    [SerializeField]
    private GameObject[] go_treePieces;

    [SerializeField]
    private GameObject go_treeCenter;

    [SerializeField]
    private GameObject go_hit_effect_prefab;

    [SerializeField]
    private GameObject go_Log_prefabs;

    [SerializeField]
    private CapsuleCollider parentCol;
    [SerializeField]
    private CapsuleCollider childCol;

    [SerializeField]
    private Rigidbody childRigid;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effect_sound;
    [SerializeField]
    private AudioClip effect_sound2;
    [SerializeField]
    private AudioClip effect_sound3;

    [SerializeField]
    private float force;
    [SerializeField]
    private GameObject go_ChildTree;

    [SerializeField]
    private float debrisDestroyTime;

    [SerializeField]
    private float destroyTime;

    public void Chop(Vector3 _pos, float angleY)
    {
        Hit(_pos);

        AngleCalc(angleY);

        if (CheckTreePieces()) return;

        FallDownTree();
    }

    private void Hit(Vector3 _pos)
    {
        audioSource.clip = effect_sound;
        audioSource.Play();

        
        GameObject clone = Instantiate(go_hit_effect_prefab, _pos, Quaternion.Euler(Vector3.zero));
        Destroy(clone, debrisDestroyTime);
        
    }

    private void AngleCalc(float _angleY)
    {
        Debug.Log(_angleY);
        if (0 <= _angleY && _angleY <= 70)
            DestroyPiece(2);
        else if (0 <= _angleY && _angleY <= 140)
            DestroyPiece(3);
        else if (0 <= _angleY && _angleY <= 210)
            DestroyPiece(4);
        else if (0 <= _angleY && _angleY <= 280)
            DestroyPiece(0);
        else if (0 <= _angleY && _angleY <= 360)
            DestroyPiece(1);

    }

    private void DestroyPiece(int _num)
    {
        if (go_treePieces[_num].gameObject != null)
        {
            GameObject clone = Instantiate(go_hit_effect_prefab, go_treePieces[_num].transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(clone, debrisDestroyTime);
            Destroy(go_treePieces[_num].gameObject);
        }
    }

    private bool CheckTreePieces()
    {
        for (int i = 0; i < go_treePieces.Length; i++)
        {
            if (go_treePieces[i].gameObject != null) return true;
        }
        return false;
    }

    private void FallDownTree()
    {
        audioSource.clip = effect_sound2;
        audioSource.Play();

        Destroy(go_treeCenter);

        parentCol.enabled = false;
        childCol.enabled = true;
        childRigid.useGravity = true;



        childRigid.AddForce(Random.Range(-force,force), 0f, Random.Range(-force, force));

        StartCoroutine(LogCoroutine());
    }

    IEnumerator LogCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);

        audioSource.clip = effect_sound3;
        audioSource.Play();

        Instantiate(go_Log_prefabs, go_ChildTree.transform.position + (go_ChildTree.transform.up * 2.5f), Quaternion.LookRotation(go_ChildTree.transform.up));
        Instantiate(go_Log_prefabs, go_ChildTree.transform.position + (go_ChildTree.transform.up * 5f), Quaternion.LookRotation(go_ChildTree.transform.up));
        Instantiate(go_Log_prefabs, go_ChildTree.transform.position + (go_ChildTree.transform.up * 7.5f), Quaternion.LookRotation(go_ChildTree.transform.up));

        Destroy(go_ChildTree.gameObject);

    }

    public Vector3 GetTreeCenterPosition()
    {
        return go_treeCenter.transform.position;
    }
}
