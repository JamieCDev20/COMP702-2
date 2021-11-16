using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigIK : MonoBehaviour
{
    [SerializeField] private bool left;
    [SerializeField] private Transform parent;

    [SerializeField] private Transform root;
    [SerializeField] private Transform mid;
    [SerializeField] private Transform end;

    [SerializeField] private Transform target;

    private float upperL, lowerL;

    private void Start()
    {
        upperL = Vector3.Distance(root.position, mid.position);
        lowerL = Vector3.Distance(end.position, mid.position);
    }

    private void Update()
    {
        IKIteration();
    }

    private void IKIteration()
    {
        if (Vector3.Distance(root.position, target.position) > upperL + lowerL)
        {
            root.transform.LookAt(target);
            mid.transform.LookAt(target);
            return;
        }

        float mag = Vector3.Distance(target.position, root.position);
        float a = mag * 0.5f;
        Vector3 dir = (target.position - root.position).normalized;

        Vector3 tmid = root.position + (dir * a) + (Vector3.Cross(dir, parent.right * (left ? -1 : 1)).normalized * O(upperL, a));

        //Debug.DrawRay(root.position, Vector3.Cross(dir, parent.right).normalized * 3, Color.red);
        //Debug.DrawRay(root.position, (dir * a) + (Vector3.Cross(dir, parent.right).normalized * O(upperL, a)), Color.blue);
        //Debug.DrawRay(tmid, (target.position - tmid).normalized * lowerL, Color.magenta);

        root.position = parent.position;
        root.rotation = Quaternion.LookRotation(tmid - root.position, Vector3.up);
        //print("########");
        //print(mid.position);
        mid.position = tmid;
        mid.rotation = Quaternion.LookRotation(target.position - mid.position, Vector3.up);
        //print(mid.position);
        //print("########");
        end.position = target.position;


    }

    private float X1(float a, float h)
    {
        return Mathf.Acos((a / h)) * Mathf.Rad2Deg;
    }

    private float X2(float x1)
    {
        return 180 - (90 - x1) - (180 - x1);
    }

    private float O(float h, float a)
    {
        return Mathf.Sqrt((h * h) - (a * a));
    }

}
