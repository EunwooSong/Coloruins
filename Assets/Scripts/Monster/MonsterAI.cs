using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [Header("-=Monster=-")]
    [SerializeField] BoxCollider2D boxColl;
    [SerializeField] Transform tr;
    public float moveSpeed;
    public GameObject gameOverUI;
    [SerializeField] public bool isRight;
    [SerializeField] Vector2 MoveDir;

    [SerializeField]float time = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        boxColl = GetComponent<BoxCollider2D>();
        gameOverUI.SetActive(false);
        StartCoroutine(AIMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
            MoveDir = Vector2.right;
        else
            MoveDir = Vector2.left;

        tr.localScale = MoveDir + Vector2.up;

        tr.Translate(MoveDir * moveSpeed * Time.deltaTime);
    }

    IEnumerator AIMovement()
    {
        while(true)
        {
            yield return null;
            yield return new WaitForSeconds(time);
            Debug.Log("Change");
            isRight = !isRight;
            time = Random.Range(0.0f, 2.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!boxColl.isTrigger && collision.gameObject.CompareTag("Player"))
            gameOverUI.SetActive(true);
    }
}
