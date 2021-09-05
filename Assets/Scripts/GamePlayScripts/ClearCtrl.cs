using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCtrl : MonoBehaviour
{
    public GameObject clear;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag.Equals("Player"))
        {

            StartCoroutine("Wait");
            
        }
    }

    IEnumerator Wait()
    {

#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
        yield return new WaitForSeconds(0.5f);
        clear.SetActive(true);
    }
}
