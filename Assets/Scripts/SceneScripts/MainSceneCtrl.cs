using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneCtrl : MonoBehaviour
{
    public void LoadGameScene(string gameScene)
    {
        SceneManager.LoadScene("LoadingScene");
        SceneInfo.sceneName = gameScene;
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}