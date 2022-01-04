using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private float f = 24;
    private int i = 5;

    private void Start()
    {
        int fl = 5;
        int hl = 10;
        for (int f = 0, h = 0; f < 5 || h < 10; f = Mathf.Clamp(f + 1, 0, fl), h = Mathf.Clamp(h + 1, 0, hl))
        {
            print(f + " " + h);
        }
    }

}
