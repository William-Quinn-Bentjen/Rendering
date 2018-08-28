using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
    public static List<string> LoadedScenes = new List<string>();
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        LoadedScenes.Add(SceneManager.GetActiveScene().name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void LoadScene(string sceneName)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);

    }
    public static void UnloadScene(string sceneName)
    {
        if (LoadedScenes.Contains(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
