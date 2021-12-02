using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The L stands for Ludophoria :-P
/// </summary>
public static class Mathl
{
    /// <summary>
    /// Its Mathf.Sin
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Sin(float x)
    {
        return Mathf.Sin(x);
    }
    /// <summary>
    /// Mathf.Sin With variables to tweak
    /// </summary>
    /// <param name="x"></param>
    /// <param name="amplitude"></param>
    /// <param name="frequency"></param>
    /// <param name="xOffset"></param>
    /// <param name="yOffset"></param>
    /// <returns></returns>
    public static float Sin(float x, float amplitude, float frequency, float xOffset, float yOffset)
    {
        return amplitude * Sin(frequency * x + xOffset) + yOffset;
    }
    /// <summary>
    /// Mathf.Sin but with one peak rooted at 0, 1
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float NormalisedSin(float x)
    {
        return Sin(x * Mathf.PI);
    }
    /// <summary>
    /// Sin rooted at 0, 1 with tweakable variables
    /// </summary>
    /// <param name="x"></param>
    /// <param name="amplitude"></param>
    /// <param name="frequency"></param>
    /// <param name="xOffset"></param>
    /// <param name="yOffset"></param>
    /// <returns></returns>
    public static float NormalisedSin(float x, float amplitude, float frequency, float xOffset, float yOffset)
    {
        return amplitude * Sin(frequency * x * Mathf.PI + xOffset) + yOffset;
    }
    /// <summary>
    /// Sin with directional skew for varied attack and decay
    /// </summary>
    /// <param name="x"></param>
    /// <param name="skew"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static float SkewedSin(float x, float skew, int dir)
    {
        return NormalisedSin(Mathf.PI * x + (Sin(x) * (skew / dir))) * dir;
    }
    /// <summary>
    /// A nice Sin wave for a bit of wobble at either end
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float WobbleSin(float x)
    {
        return 0.33f * Sin(2.621f * Mathf.PI * x + -Mathf.PI) + (1.314f * x);
    }

    /// <summary>
    /// Will set checkAgainst equal to toCheck if toCheck is less and then return
    /// </summary>
    /// <param name="checkAgainst"></param>
    /// <param name="toCheck"></param>
    /// <returns></returns>
    public static bool IfLessThenSet(ref float checkAgainst, float toCheck)
    {
        if (checkAgainst > toCheck)
        {
            checkAgainst = toCheck;
            return true;
        }
        return false;
    }
    public static bool IfLessThenSet(ref float checkAgainst, Vector3 toCheck)
    {
        if (checkAgainst * checkAgainst > toCheck.sqrMagnitude)
        {
            checkAgainst = toCheck.magnitude;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Will set checkAgainst equal to toCheck if toCheck is more and then return
    /// </summary>
    /// <param name="checkAgainst"></param>
    /// <param name="toCheck"></param>
    /// <returns></returns>
    public static bool IfMoreThenSet(ref float checkAgainst, float toCheck)
    {
        if (checkAgainst < toCheck)
        {
            checkAgainst = toCheck;
            return true;
        }
        return false;
    }
    public static bool IfMoreThenSet(ref float checkAgainst, Vector3 toCheck)
    {
        if (checkAgainst * checkAgainst < toCheck.sqrMagnitude)
        {
            checkAgainst = toCheck.magnitude;
            return true;
        }
        return false;
    }

}
