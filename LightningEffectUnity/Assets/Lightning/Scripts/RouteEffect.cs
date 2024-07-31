using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoutePoint
{
    public Vector2 position;
}

public class RouteEffect : MonoBehaviour
{
    public Image RouteImage;
    public Sprite[] RouteSprites;
    public int RouteIndex = 0;
    public RectTransform[] WeldPoints;
    public GameObject RouteCube;
    private Texture2D mRouteTexture;
    private Color[] mRoutePixels;
    private float mScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        RouteImage.sprite = RouteSprites[RouteIndex];
        RouteImage.SetNativeSize();


        for (int i = 0; i < WeldPoints.Length; i++)
        {
            Vector3 p = WeldPoints[i].position;
            WeldPoints[i].anchorMin = Vector2.zero;
            WeldPoints[i].anchorMax = Vector2.zero;
            WeldPoints[i].position = p;
        }

        mRouteTexture = (Texture2D)RouteImage.mainTexture;
        mRoutePixels = mRouteTexture.GetPixels();
        mScale = RouteImage.rectTransform.sizeDelta.x / mRouteTexture.width;
        //Debug.Log("flexibleWidth:" + RouteImage.rectTransform.sizeDelta.x);

        //StartCoroutine(GetRoutePath(Color.red, mRoutePixels, mRouteTexture.width, mRouteTexture.height, WeldPoints[0]));
        //List<RoutePoint> routePathTop = GetRoutePath(Color.red, mRoutePixels, mRouteTexture.width, mRouteTexture.height, true);
        //StartCoroutine(ShowRoutePath(routePathTop, WeldPoints[0]));

        //List<RoutePoint> routePathBottom = GetRoutePath(Color.red, mRoutePixels, mRouteTexture.width, mRouteTexture.height, false);
        //StartCoroutine(ShowRoutePath(routePathBottom, WeldPoints[1]));

        StartCoroutine(CreateRoutePath(true, WeldPoints[0]));
        StartCoroutine(CreateRoutePath(false, WeldPoints[1]));
    }

    private IEnumerator CreateRoutePath(bool isTop, RectTransform weldPoint)
    {
        List<RoutePoint> routePathTop1 = GetRoutePath(Color.red, mRoutePixels, mRouteTexture.width, mRouteTexture.height, isTop);
        yield return ShowRoutePath(routePathTop1, weldPoint);

        List<RoutePoint> routePathTop2 = GetRoutePath(Color.green, mRoutePixels, mRouteTexture.width, mRouteTexture.height, isTop);
        yield return ShowRoutePath(routePathTop2, weldPoint);

        List<RoutePoint> routePathTop3 = GetRoutePath(Color.blue, mRoutePixels, mRouteTexture.width, mRouteTexture.height, isTop);
        yield return ShowRoutePath(routePathTop3, weldPoint);
    }

    private IEnumerator ShowRoutePath(List<RoutePoint> routePath, RectTransform weldPoint)
    {
        foreach (var item in routePath)
        {
            weldPoint.anchoredPosition = item.position;
            GameObject go = GameObject.Instantiate(RouteCube);
            go.transform.position = weldPoint.position;
            yield return new WaitForSeconds(0.05f);
        }
    }

    //private IEnumerator GetRoutePath(Color color, Color[] pixels, int width, int height, RectTransform weldPoint)
    private List<RoutePoint> GetRoutePath(Color color, Color[] pixels, int width, int height, bool isTop)
    {
        List<RoutePoint> routePath = new List<RoutePoint>();
        //Debug.Log("GetRoutePath: w:" + width + " h:" + height);
        RoutePoint start = null;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color c = pixels[y * width + x];

                if ( Mathf.Abs(c.r - color.r) < 0.1f &&
                     Mathf.Abs(c.g - color.g) < 0.1f &&
                     Mathf.Abs(c.b - color.b) < 0.1f &&
                     c.a > 0.5f
                    )
                {
                    if ( (isTop && y>=height/2) || (!isTop && y<height/2) )
                    {
                        RoutePoint newRoutePoint = new RoutePoint();
                        newRoutePoint.position = new Vector2(x * mScale, y * mScale);
                        routePath.Add(newRoutePoint);
                    }
                }
            }
        }

        return routePath;
        //yield return null;
    }

    //private void addRoutePoint(RoutePoint start, RoutePoint newRoutePoint)
    //{
    //    //get the nearest point
    //    RoutePoint currentRoutePoint = start;
    //    RoutePoint nearestRoutePoint = null;
    //    float nearestDistance = float.MaxValue;
    //    while (currentRoutePoint!=null)
    //    {
    //        float d = Vector2.Distance(currentRoutePoint.position, newRoutePoint.position);
    //        //float d = Vector2.Distance(start.position, newRoutePoint.position);
    //        if ( d<nearestDistance && newRoutePoint.position.y>=start.position.y )
    //        {
    //            nearestRoutePoint = currentRoutePoint;
    //            nearestDistance = d;
    //        }

    //        currentRoutePoint = currentRoutePoint.next;
    //    }

    //    if (nearestRoutePoint!=null)
    //    {
    //        RoutePoint next = nearestRoutePoint.next;
    //        nearestRoutePoint.next = newRoutePoint;
    //        newRoutePoint.next = next;
    //        newRoutePoint.pre = nearestRoutePoint;
    //        if (next != null)
    //        {
    //            next.pre = newRoutePoint;
    //        }
    //    }

    //}
}
