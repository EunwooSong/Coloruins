using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//색과 동화시키기 위해 파괴가 필요
public class FootHoldCtrl : MonoBehaviour
{
    public string footHoldName;
    SpriteRenderer sr;

    Transform tr;

    float widthWorld, heightWorld;      //윌드좌표 크기   -   삭제를 위해 필요함
    int widthPixel, heightPixel;        //픽셀값
    
    Color transP;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();

        //텍스쳐 파일 로드, 생성
        Texture2D tex = (Texture2D)Resources.Load(footHoldName);
        Texture2D tex_clone = (Texture2D) Instantiate(tex);


        sr.sprite = Sprite.Create(tex_clone,
                                   new Rect(0.0f, 0.0f, tex_clone.width, tex_clone.height),
                                   new Vector2(0.5f, 0.5f), 100.0f);

        //픽셀을 지정하고자 할때 필요한 색 설정(흰색)
        transP = new Color(0f, 0f, 0f, 0f);
        InitSpriteDimensions();

        //footholdCtrl의 값을 이것으로 해줌
        PaintCtrl.footHoldCtrl = this;
    }


    //치수(크기) 초기화
    private void InitSpriteDimensions()
    {
        widthWorld = sr.bounds.size.x;
        heightWorld = sr.bounds.size.y;
        widthPixel = sr.sprite.texture.width;
        heightPixel = sr.sprite.texture.height;
    }

    //파괴시킴
    public void DestroyFootHold(CircleCollider2D pc)
    {
        Debug.Log("DestroyFootHold");

        //폭파위치의 중심 위치
        Vector2Init center = World2Pixel(pc.bounds.center.x, pc.bounds.center.y);
        //폭파사이즈?
        int r = Mathf.RoundToInt(pc.bounds.size.x * widthPixel / widthWorld);

        int x, y, px, nx, py, ny, d;

        for(x = 0; x <= r; x++)
        {
            d = (int)Mathf.RoundToInt(Mathf.Sqrt(r * r - x * x));

            for (y = 0; y <= d; y++)
            {
                px = center.x + x;
                nx = center.x - x;
                py = center.y + y;
                ny = center.y - y;

                sr.sprite.texture.SetPixel(px, py, transP);
                sr.sprite.texture.SetPixel(nx, py, transP);

                sr.sprite.texture.SetPixel(px, ny, transP);
                sr.sprite.texture.SetPixel(nx, ny, transP);
                Debug.Log("Check Error");
            }
        }

        sr.sprite.texture.Apply();
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    Vector2Init World2Pixel(float x, float y)
    {
        Vector2Init v2Init = new Vector2Init();

        float dx = x - tr.position.x;
        v2Init.x = Mathf.RoundToInt(0.5f*widthPixel + dx*widthPixel/widthWorld);

        float dy = y - tr.position.y;
        v2Init.y = Mathf.RoundToInt(0.5f * heightPixel + dy * heightPixel / heightWorld);

        return v2Init;
    }
}
