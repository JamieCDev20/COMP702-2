using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayerController : MonoBehaviour
{

    private Vector3 inpVector;

    private void Update()
    {
        inpVector.x = Input.GetAxisRaw("Horizontal");
        inpVector.z = Input.GetAxisRaw("Vertical");

        if (inpVector.x == 0)
            AnimEnvironment.x.sideWalkTime = 0;
        else
            AnimEnvironment.x.sideWalkTime += inpVector.x * Time.deltaTime;
        if (inpVector.z == 0)
            AnimEnvironment.x.forwardWalkTime = 0;
        else
            AnimEnvironment.x.forwardWalkTime += inpVector.z * Time.deltaTime;

    }

}
