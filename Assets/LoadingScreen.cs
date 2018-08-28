using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
    public Transform spinner;
    public float spinRate = 1;
    public string sceneName;
    private AsyncOperation operation;
    private Scene loadScene;
    // Use this for initialization
    void Start () {
        SceneManager.GetActiveScene();
        operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}
	// Update is called once per frame
	void Update () {
        if (!operation.isDone)
        {
            spinner.rotation = Quaternion.Euler(spinner.rotation.x, spinner.rotation.y, spinner.rotation.z + (spinRate * Time.deltaTime));
        }
        else
        {
            SceneManager.UnloadSceneAsync(loadScene.name);//loadScene);
        }
        
	}
}
