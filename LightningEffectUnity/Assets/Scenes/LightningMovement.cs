using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMovement : MonoBehaviour
{
    public Vector3 Movement;
    public float MoveSpeed = 1f;
    private Vector3 mMoveStart;
    private Vector3 mMoveEnd;
    private float mMoveValue;
    // Start is called before the first frame update
    void Start()
    {
        mMoveStart = transform.localPosition;
        mMoveEnd = mMoveStart + Movement;
    }

    // Update is called once per frame
    void Update()
    {
        mMoveValue += Time.deltaTime * MoveSpeed;
        if (mMoveValue>1f)
        {
            mMoveValue = 0f;
        }

        transform.localPosition = Vector3.Lerp(mMoveStart, mMoveEnd, mMoveValue);
    }
}
