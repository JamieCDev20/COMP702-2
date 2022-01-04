using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEnvironment : EnvirontmentBase
{

    public static AnimEnvironment x;

    public float moveSpeed;
    internal int playerCount;
    internal float forwardWalkTime;
    internal float sideWalkTime;

    [SerializeField] private Transform[] feet;
    [SerializeField] private Transform hips;
    [SerializeField] private Transform[] hands;

    private void Start()
    {
        x = this;
        playerCount = feet.Length + hands.Length;
    }

    public (Vector3[] feet, Vector3 hips, Vector3[] hands) GetState()
    {
        Vector3[] f = new Vector3[feet.Length];
        Vector3[] h = new Vector3[hands.Length];

        for (int i = 0; i < f.Length; i++)
        {
            f[i] = feet[i].position;
        }
        for (int i = 0; i < h.Length; i++)
        {
            h[i] = hands[i].position;
        }

        return (f, hips.position, h);

    }

    public Transform GetPlayer(int _i_player)
    {
        bool hand = false;
        if(_i_player >= feet.Length)
        {
            hand = true;
            _i_player -= feet.Length;
        }

        if (hand)
            return hands[_i_player];
        return feet[_i_player];

    }

    public void ActOnWorld((Vector3[] feet, Vector3[] hands) targets)
    {
        int fl = feet.Length;
        int hl = hands.Length;
        for (int f = 0, h = 0; f < fl || h < hl; f = Mathf.Clamp(f+1, 0, fl), h = Mathf.Clamp(h+1, 0, hl))
        {
            feet[f].position = Vector3.Lerp(feet[f].position, targets.feet[f], 1f);
            hands[h].position = Vector3.Lerp(hands[h].position, targets.hands[h], 1f);
        }

    }

}
