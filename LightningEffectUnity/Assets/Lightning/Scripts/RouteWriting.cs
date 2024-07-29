using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteWriting : MonoBehaviour
{
    private Image routeImage;
    private Texture2D routeTexture;
    private Color32[] routeTexturePixels;
    // Start is called before the first frame update
    void Start()
    {
        routeImage = GetComponent<Image>();
        //routeImage.mainTexture

        routeTexture = (Texture2D)routeImage.mainTexture;
        routeTexturePixels = routeTexture.GetPixels32();

        
        for (int y = 0; y < routeTexture.height; y++)
        {
            for (int x = 0; x < routeTexture.width; x++)
            {
                Color color = routeTexture.GetPixel(x, y);
                if ( color.g>=0.7f && color.a>0.9f )
                {
                    color = Color.white;
                }
                color.a = 0f;
                routeTexture.SetPixel(x, y, color);
            }
        }
        routeTexture.Apply();

        StartCoroutine(RouteAnimation());
        
    }

    IEnumerator RouteAnimation()
    {
        Vector2 start = Vector2.zero;
        while(true)
        {

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    private bool getNearestWhitePos(Texture2D texture, Vector2Int p, out Vector2Int outPos)
    {
        int R = texture.width > texture.height ? texture.width : texture.height;
        for (int r = 0; r < R; r++)
        {
            for (int x = -r; x < r; x++)
            {
                for (int y = -r; y < r; y++)
                {
                    int px = p.x + x;
                    int py = p.x + y;

                    if ( px>=0 && px<texture.width &&
                         py>=0 && py<texture.height)
                    {
                        Color color = texture.GetPixel(px, py);
                        if ( color.r>0.9f && color.g>0.9f && color.b>0.9f )
                        {
                            outPos = new Vector2Int(px, py);
                            return true;
                        }

                    }

                    
                }
            }
        }

        //Color color = texture.GetPixel(p.x, p.y);
        outPos = Vector2Int.zero;
        return false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int y = 0; y < routeTexture.height; y++)
            {
                for (int x = 0; x < routeTexture.width; x++)
                {
                    Color color = routeTexturePixels[y*routeTexture.width+x];
                    //color.a = 0f;
                    routeTexture.SetPixel(x, y, color);
                }
            }
            routeTexture.Apply();
        }
    }
}
