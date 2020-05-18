using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    public string sceneName = "GameStage";
    public static Title instance;

    private SaveAndLoad theSaveAndLoad;

   [SerializeField] private GameObject thisgameobject;

    private void Awake() {

        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else {
            Destroy(this.gameObject);
        }


    }


    public void ClickStart() {

        SceneManager.LoadScene(sceneName);
        gameObject.SetActive(false);
    }


    public void ClickLoad() {

        StartCoroutine(LoadCoroutine());
        //SceneManager.LoadScene(sceneName);
       // theSaveAndLoad.LoadData();
        
        gameObject.SetActive(false);
    }



    IEnumerator LoadCoroutine() {

        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone) {
            yield return null;
        }
        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        theSaveAndLoad.LoadData();

        Destroy(gameObject);

        

    }



    public void ClickExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }


}
