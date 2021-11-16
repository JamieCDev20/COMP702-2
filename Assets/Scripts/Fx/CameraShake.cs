using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private Transform cam;

    [SerializeField] private float magnitude, duration;
    [SerializeField] private ShakeType _type;

    private void Start()
    {
        if (!cam)
            cam = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartShake(magnitude, duration, _type);
    }

    public void StartShake(float magnitude, float duration, ShakeType _type)
    {
        StartCoroutine(ShakeCamera(magnitude, duration, _type));
    }
    private float RandomOffset()
    {
        return Random.Range(-1f, 1f);
    }
    IEnumerator ShakeCamera(float magnitude, float duration, ShakeType type)
    {
        //add two values. magnitude and duration
        switch (type)
        {
            case ShakeType.earthquake:
                for (float t = 0; t < duration; t += Time.deltaTime)
                {
                    yield return null;
                    cam.localEulerAngles = Vector3.Lerp(Vector3.zero,
                        Vector3.up * (magnitude * Mathf.Sin(Time.realtimeSinceStartup * 80)) + Vector3.right * (magnitude/2 * Mathf.Sin(Time.realtimeSinceStartup * 50)),
                        (duration + 0.5f) * Mathf.Sin((t) * Mathf.PI * (1 / duration)));
                }
                cam.localEulerAngles = Vector3.zero;
                break;
            case ShakeType.impact:
                for (float t = 0; t < duration; t+= Time.deltaTime)
                {
                    yield return null;
                    transform.localPosition = new Vector3(RandomOffset(), RandomOffset(), RandomOffset()) * magnitude * (1 - (t/duration));
                }
                break;
            default:
                break;
        }

    }

}

public enum ShakeType
{
    earthquake,
    impact
}
