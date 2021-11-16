using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    [SerializeField] private string[] sA_tags;

    private void Start()
    {
        TagManager.x.SetObjectTagsInPool(gameObject, sA_tags);
    }

    internal void UpdateTags(string[] _sA)
    {
        sA_tags = _sA;
        TagManager.x.SetObjectTagsInPool(gameObject, sA_tags);
    }
}