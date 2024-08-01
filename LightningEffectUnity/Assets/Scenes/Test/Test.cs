using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Transform[] CycleTransforms;
    private Vector3 mLastMousePos;
    private int mMoveIndex = 0;
    private int[] mCycleColors;
    private float mHoldPageTime = 0f;
    private bool mIsBusy = false;
    // Start is called before the first frame update
    void Start()
    {
        CycleTransforms[1].localScale = Vector3.one * 2f;
        mLastMousePos = Input.mousePosition;
        mMoveIndex = 1;
        Cursor.visible = false;

        mCycleColors = new int[CycleTransforms.Length];
        for (int i = 0; i < mCycleColors.Length; i++)
        {
            mCycleColors[i] = 0;
        }
    }

    private void MouseMoveEnd()
    {
        mIsBusy = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse Y") != 0)
        {
            float yMove = Input.GetAxis("Mouse Y");
            float speed = yMove;
            //Debug.Log(speed);

            if (Mathf.Abs(speed) > 0.05f && mIsBusy == false)
            {
                //Debug.Log(speed);
                int s = (int)Mathf.Sign(speed);
                Move(-s);
                mIsBusy = true;
                this.Invoke("MouseMoveEnd", 0.2f);
            }
        }
        

        //Vector3 mousePos = Input.mousePosition;
        //float yMove = mousePos.y - mLastMousePos.y;
        //float speed = yMove / Time.deltaTime;
        
        //mLastMousePos = mousePos;
        //if (Mathf.Abs(speed)>1f && mIsBusy==false)
        //{
        //    //Debug.Log(speed);
        //    int s = (int)Mathf.Sign(speed);
        //    Move(-s);
        //    mIsBusy = true;
        //    this.Invoke("MouseMoveEnd", 0.2f);
        //}

        //if ( Mathf.Abs(yMove)>400f )
        //{
        //    mLastMousePos = mousePos;
        //    //Debug.Log("yMove:" + yMove);
        //    int s = (int)Mathf.Sign(yMove);
        //    Move(-s);
        //    //Debug.Log(s);
        //}

        float holdTime = 0f;

        if ( Input.GetKeyDown(KeyCode.PageDown) || Input.GetKeyDown(KeyCode.PageUp) || Input.GetMouseButtonDown(0) )
        {
            

            //if (mHoldPageTime < holdTime)
            {
                mHoldPageTime += Time.deltaTime;
                //if (mHoldPageTime >= holdTime)
                {
                    //Debug.Log("test");
                    ChangeColor();
                }
            }
        }
        else
        {
            mHoldPageTime = 0f;
        }
    }

    private void Move(int moveDir)
    {
        mMoveIndex += moveDir;
        mMoveIndex = Mathf.Clamp(mMoveIndex, 0, CycleTransforms.Length - 1);

        for (int i = 0; i < CycleTransforms.Length; i++)
        {
            CycleTransforms[i].localScale = Vector3.one;
        }

        CycleTransforms[mMoveIndex].localScale = Vector3.one * 2f;

    }

    private void ChangeColor()
    {
        mCycleColors[mMoveIndex]++;
        if (mCycleColors[mMoveIndex]%2==0)
        {
            CycleTransforms[mMoveIndex].GetComponent<Image>().color = Color.white;
        }
        else
        {
            CycleTransforms[mMoveIndex].GetComponent<Image>().color = Color.green;
        }
    }
}
