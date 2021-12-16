using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private float f = 24;
    private int i = 5;

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            print((char)(65+ (i%6)));
        }
    }

}
