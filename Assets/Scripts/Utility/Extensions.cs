using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    #region Number

    /// <summary>
    /// Is int x between the min and max values
    /// </summary>
    /// <param name="x"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool Between(this int x, int min, int max)
    {
        return ((x - min) | (max - x)) >= 0;
    }
    /// <summary>
    /// Is float x between the min and max values
    /// </summary>
    /// <param name="x"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool Between(this float x, float min, float max)
    {
        return (x >= min) && (x <= max);
    }

    #endregion

    #region Vector

    /// <summary>
    /// Returns the vector v with the X value set to zero
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 FlattenX(this Vector3 v)
    {
        v.x = 0;
        return v;
    }
    /// <summary>
    /// Returns the vector v with the Y value set to zero
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 FlattenY(this Vector3 v)
    {
        v.y = 0;
        return v;
    }
    /// <summary>
    /// Returns the vector v with the Z value set to zero
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 FlattenZ(this Vector3 v)
    {
        v.z = 0;
        return v;
    }

    /// <summary>
    /// Returns vector a pointwise multiplied by vector b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector3 Times(this Vector3 a, Vector3 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        a.z *= b.z;
        return a;
    }

    /// <summary>
    /// Casts the vector2 a to a vector3 with the x, y mapped to x, z
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 a)
    {
        return new Vector3(a.x, 0, a.y);
    }

    /// <summary>
    /// Returns the vector3 a as a vector2 with x, y mapped to x, y
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Vector2 ToVector2XY(this Vector3 a)
    {
        return new Vector2(a.x, a.y);
    }

    /// <summary>
    /// Returns the vector3 a as a vector2 with x, z mapped to x, y
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Vector2 ToVector2XZ(this Vector3 a)
    {
        return new Vector2(a.x, a.z);
    }

    /// <summary>
    /// Returns a random unit Vector3
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Vector3 Randomise(this Vector3 a)
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    /// <summary>
    /// Returns a random unit Vector3 scaled by a magnitude
    /// </summary>
    /// <param name="a"></param>
    /// <param name="_magnitude"></param>
    /// <returns></returns>
    public static Vector3 Randomise(this Vector3 a, float _magnitude)
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _magnitude;
    }

    #endregion

    #region Quaternion

    /// <summary>
    /// Returns a random quaternion
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static Quaternion Randomise(this Quaternion q)
    {
        return new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    #endregion

}
