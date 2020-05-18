using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{

    public List<string> _animationName = new List<string>();
    public float _speed;
    public float _attack;

    public GameObject _target;
    public GameObject _target2;


    [SerializeField] private int hp;
    public GameObject _hpBar;
    public TextMesh _HpVal;
    public GameObject _HpObj;

    public GameObject _DamEffect;
    public GameObject _DamText;

    public float _timerForLv;
    public float _timerForLvLim;

    public AudioClip _damageSnd;


    private bool isDead;


    /// <mycreate>


    /// 





    // Use this for initialization
    void Start()
    {

        GetComponent<Animation>()[_animationName[0]].layer = 0;
        GetComponent<Animation>()[_animationName[1]].layer = 1;
        GetComponent<Animation>()[_animationName[2]].layer = 3;
        GetComponent<Animation>()[_animationName[3]].layer = 4;
        GetComponent<Animation>().CrossFade(_animationName[0], 0.1f);
        GetComponent<Animation>()[_animationName[2]].speed = 2.0f;
        GetComponent<Animation>()[_animationName[3]].speed = 2.0f;
        _target = GameObject.FindWithTag("Player");
        //_target2 = GameObject.FindWithTag("Carrot");


    }

    // Update is called once per frame
    void Update()
    {

        targetting();
       // Deleting();
        //_target = GameObject.FindWithTag("Carrot");




    }

    private void Deleting()
    {
        /*
        GameObject testGameObject;


        Collider[] colliders = Physics.OverlapSphere(transform.position, 2);
        for (int i = 0; i < colliders.Length; i++)
        {

            testGameObject = colliders[i].gameObject;
            if (testGameObject.tag == "Carrot")
            {
                //colliders[i].gameObject = _target2;
                Destroy(testGameObject);
            }
        }
        */
    }



    private void targetting()
    {

        _timerForLv += Time.deltaTime;
        if (_timerForLv > _timerForLvLim)
        {
            _speed += 0.1f;
            _timerForLv = 0;
        }

        if (_target != null)
        {
            transform.position += (_target.transform.position - transform.position).normalized * _speed * Time.deltaTime;
            //transform.forward = (_target.transform.position - transform.position).normalized;
            GetComponent<Animation>().CrossFade(_animationName[1], 0.1f);
            transform.LookAt(_target.transform);

            if ((_target.transform.position - transform.position).magnitude < 10.0f)
            {
                GetComponent<Animation>().CrossFade(_animationName[2], 0.1f);
            }
            else
            {
                GetComponent<Animation>().Stop(_animationName[2]);
            }
        }




    }



    /*
	public void Damaged(float _dam)
	{
        //if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().PlayOneShot(_damageSnd);
		_hp -= _dam;
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        //GetComponent<Animation>().CrossFade(_animationName[3], 0.1f);
        //if(_DamEffect!=null) Instantiate(_DamEffect, new Vector3(transform.position.x, 2.0f, transform.position.z), Quaternion.identity);
        //if(_DamText!=null) Instantiate(_DamText, new Vector3(transform.position.x, 1.2f, transform.position.z + 0.2f), Quaternion.identity);
        if (_hp >0)
		{
            
			//if(_hpBar!=null) _hpBar.transform.localScale = new Vector3 (_hp*0.01f,1,1);
			//if(_HpVal!=null)_HpVal.text = _hp.ToString();
		}
		else if(_hp <= 0)
		{
			//if(_hpBar!=null) _hpBar.transform.localScale = new Vector3 (0,1,1);
			//if(_HpVal!=null) _HpVal.text = "0";

            //_target.GetComponent<PlayerScript>()._gameWin = true;
            //_target.GetComponent<PlayerScript>().GameOver();
			DestroyThis();
		}

	}
    */

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        Debug.Log("llllkkkkkkkkklll");

        if (!isDead)
        {

            hp -= _dmg;
            if (hp <= 0)
            {
                DestroyThis();

                //GameObject carrot_plant = (GameObject)Instantiate(Carrot_Plant, checkposition, Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
                //Destroy(this);

                return;

            }

        }



    }

    void DestroyThis()
    {
        Destroy(gameObject);
        //Destroy(gameObject);
        //if(_HpObj!=null) Destroy(_HpObj);
    }
}
