using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    public static TagManager x;
    private Dictionary<GameObject, string[]> gosaD_allTaggedObjects = new Dictionary<GameObject, string[]>();


    private void Awake()
    {
        x = this;
    }

    /// <summary>
    /// Adds a GameObject to the tagged object pool. If the pool already contains the object, updates the tags.
    /// </summary>
    /// <param name="_go"></param>
    /// <param name="_sA"></param>
    internal void SetObjectTagsInPool(GameObject _go, string[] _sA)
    {
        if (gosaD_allTaggedObjects.ContainsKey(_go))
            gosaD_allTaggedObjects[_go] = _sA;
        else
            gosaD_allTaggedObjects.Add(_go, _sA);
    }
}