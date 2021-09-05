using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    public int count;
    public int goalCount;
    public bool active;

    private void Start()
    {
        count = 0;
        StartCoroutine(ActiveCheck());

       if(GetComponent<BoxCollider2D>() != null)
            GetComponent<BoxCollider2D>().enabled = active;
       else
            GetComponent<CircleCollider2D>().enabled = active;

        GetComponent<SpriteRenderer>().enabled = active;
    }

    IEnumerator ActiveCheck()
    {
        while(true)
        {
            yield return null;

             
            if (count == goalCount)
            {
                Debug.Log("Check!");
                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D>().enabled = !active;
                else
                    GetComponent<CircleCollider2D>().enabled = !active;

                GetComponent<SpriteRenderer>().enabled = !active;

                StopCoroutine(ActiveCheck());
            }
        }
    }
}
