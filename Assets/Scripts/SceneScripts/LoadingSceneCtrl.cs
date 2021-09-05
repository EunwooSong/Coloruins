using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneCtrl : MonoBehaviour
{
    [SerializeField]
    Image loadingBar;   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());    
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation oper = SceneManager.LoadSceneAsync(SceneInfo.sceneName);

        oper.allowSceneActivation = false;

        float timer = 0.0f;

        while(!oper.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if(oper.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timer);

                if (loadingBar.fillAmount == 1.0f)
                    oper.allowSceneActivation = true;
            }

            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, oper.progress, timer);

                if (loadingBar.fillAmount >= oper.progress)
                    timer = 0f;
            }
        }
    }
}
