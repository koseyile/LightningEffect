using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float RandomXRange = 500f;
    public float RandomYRange = 100f;
    public float MoveSpeed = 5000f;
    public float UpdateTargetTime = 0.5f;
    private Vector3 mCenterPos;
    private Vector3 mTargetPos;
    // Start is called before the first frame update
    void Start()
    {
        mCenterPos = transform.localPosition;
        mTargetPos = mCenterPos;
        StartCoroutine(RandomUpdateTarget());
    }

    private IEnumerator RandomUpdateTarget()
    {
        while (true)
        {
            mTargetPos = mCenterPos + Vector3.right * Random.Range(-RandomXRange, RandomXRange) + Vector3.up * Random.Range(-RandomYRange, RandomYRange);
            yield return new WaitForSeconds(UpdateTargetTime);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = mTargetPos - transform.localPosition;
        moveDir.Normalize();
        transform.localPosition += moveDir * Time.deltaTime * MoveSpeed;
    }
}
