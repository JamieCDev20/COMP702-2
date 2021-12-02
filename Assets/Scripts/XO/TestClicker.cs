using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClicker : MonoBehaviour
{

    private int x = 0;
    private int y = 0;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            XOManager.x.SetSlot(0, x, y);
            x += 1;
            if(x > 2)
            {
                x = 0;
                y += 1;
            }
            if (y > 2)
                y = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            XOManager.x.SetSlot(1, x, y);
            x += 1;
            if (x > 2)
            {
                x = 0;
                y += 1;
            }
            if (y > 2)
                y = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            x = 0;
            y = 0;
            XOManager.x.ResetBoard();
        }
    }

}
