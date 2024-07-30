using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteWriting : MonoBehaviour
{
    public RectTransform TestImage;
    private Image routeImage;
    private Texture2D routeTexture;
    private Color[] routeTexturePixels;
    private Color[] routeTextureTargetPixels;
    // Start is called before the first frame update
    void Start()
    {
        routeImage = GetComponent<Image>();
        //routeImage.mainTexture

        routeTexture = (Texture2D)routeImage.mainTexture;
        routeTexturePixels = routeTexture.GetPixels();
        routeTextureTargetPixels = routeTexture.GetPixels();


        for (int y = 0; y < routeTexture.height; y++)
        {
            for (int x = 0; x < routeTexture.width; x++)
            {
                Color color = routeTexture.GetPixel(x, y);
                if ( color.g>=0.7f && color.a>0.9f )
                {
                    color = Color.white;
                }
                routeTextureTargetPixels[y * routeTexture.width + x] = color;
                color.a = 0f;
                routeTexture.SetPixel(x, y, color);
            }
        }
        routeTexture.Apply();

        StartCoroutine(RouteAnimation());
        
    }

    IEnumerator RouteAnimation()
    {
        Vector2Int start = new Vector2Int(0, routeTexture.height);
        int R = routeTexture.width > routeTexture.height ? routeTexture.width : routeTexture.height;
        getNearestWhitePos(routeTexture, start, R, out start);
        while (true)
        {
            //Debug.Log("getNearestWhitePos begin");
            bool isFound = getNearestWhitePos(routeTexture, start, 512, out start);
            TestImage.anchoredPosition = start;
            //Debug.Log(start);
            //Debug.Log("getNearestWhitePos end");
            if (isFound==false)
            {
                break;
            }

            yield return recoverColor(start, 4, true);
            yield return recoverColor(start, 50, false);
            //yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("RouteAnimation finished!");

        yield return null;
    }

    private IEnumerator recoverColor(Vector2Int pos, int R, bool isTrunk)
    {
        //for (int r = 0; r < R; r++)
        {
            for (int x = -R; x < R; x++)
            {
                for (int y = -R; y < R; y++)
                {
                    int px = pos.x + x;
                    int py = pos.y + y;

                    Vector2Int p2 = new Vector2Int(px, py);
                    float d = Vector2Int.Distance(pos, p2);
                    if (d>R)
                    {
                        continue;
                    }

                    if (px >= 0 && px < routeTexture.width &&
                        py >= 0 && py < routeTexture.height)
                    {
                        Color trunkColor = routeTexture.GetPixel(px, py);

                        if (isTrunk)
                        {
                            if (trunkColor.r > 0.9f && trunkColor.g > 0.9f && trunkColor.b > 0.9f)
                            {
                                Color color = routeTexturePixels[py * routeTexture.width + px];
                                routeTexture.SetPixel(px, py, color);
                                routeTextureTargetPixels[py * routeTexture.width + px] = color;
                            }
                        }
                        else
                        {
                            if ( !(trunkColor.r > 0.9f && trunkColor.g > 0.9f && trunkColor.b > 0.9f) )
                            {
                                Color color = routeTexturePixels[py * routeTexture.width + px];
                                routeTexture.SetPixel(px, py, color);
                                routeTextureTargetPixels[py * routeTexture.width + px] = color;
                            }
                        }

                        //Debug.Log("recoverColor x:" + px + " y:" + py);
                        //yield return null;
                    }
                }
            }
        }

        routeTexture.Apply();
        yield return null;
        //Color color = routeTexturePixels[pos.y * routeTexture.width + pos.x];
        //routeTexture.SetPixel(pos.x, pos.y, color);
    }

    private bool getNearestWhitePos(Texture2D texture, Vector2Int p, int R, out Vector2Int outPos)
    {
        float nearestDistance = float.MaxValue;
        Vector2Int nearestPos = Vector2Int.zero;
        //int R = texture.width > texture.height ? texture.width : texture.height;
        //for (int r = 0; r < R; r++)
        {
            for (int x = -R; x < R; x++)
            {
                for (int y = -R; y < R; y++)
                {
                    int px = p.x + x;
                    int py = p.x + y;

                    Vector2Int p2 = new Vector2Int(px, py);
                    float d = Vector2Int.Distance(p, p2);
                    if ( d > R )
                    {
                        continue;
                    }

                    if ( px>=0 && px<texture.width &&
                         py>=0 && py<texture.height )
                    {
                        if ( d < nearestDistance )
                        {
                            //Color color = texture.GetPixel(px, py);
                            Color color = routeTextureTargetPixels[py * texture.width + px];


                            if (color.r > 0.9f && color.g > 0.9f && color.b > 0.9f)
                            {
                                //Debug.Log("nearestDistance=" + nearestDistance + " d=" + d);
                                //Debug.Log("getNearestWhitePos" + "p1=" + p + "p2=" + p2 + ":" + color);

                                nearestPos = new Vector2Int(px, py);
                                nearestDistance = d;
                            }
                        }

                    }
                }
            }
        }

        if (nearestDistance<=R)
        {
            outPos = nearestPos;
            return true;
        }

        //Debug.Log("nearestDistance=" + nearestDistance + " p=" + p + " R=" + R);
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
