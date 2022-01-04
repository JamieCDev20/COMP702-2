using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimState : State
{

    private Vector3[] vA_footPositions;
    public Vector3[] FootPositions { get { return vA_footPositions; } }
    private Vector3 v_centreOfGravity;
    private Vector3[] vA_handPositions;
    public Vector3[] HandPositions { get { return vA_handPositions; } }
    private Vector3 v_hipPosition;
    public Vector3 HipPos { get { return v_hipPosition; } }
    private Vector3[] vA_movements = new Vector3[7]
    {
        Vector3.zero,
        Vector3.up,
        Vector3.down,
        Vector3.forward,
        Vector3.back,
        Vector3.right,
        Vector3.left
    };

    private Vector3[] vA_lastFootPos;
    private Vector3[] vA_lastHandPos;

    public AnimState()
    {
        (Vector3[] footPos, Vector3 hipPos, Vector3[] handPos) state = AnimEnvironment.x.GetState();

        vA_footPositions = state.footPos;
        vA_lastFootPos = state.footPos;
        v_hipPosition = state.hipPos;
        vA_handPositions = state.handPos;
        vA_lastHandPos = state.handPos;
        v_centreOfGravity = v_hipPosition.FlattenY();

    }

    public void SetLimbPositions(Vector3[] foot, Vector3[] hand)
    {
        vA_footPositions = (Vector3[])foot.Clone();
        vA_handPositions = (Vector3[])hand.Clone();
    }

    public void SetLastLimbPositions(Vector3[] foot, Vector3[] hand)
    {
        vA_lastFootPos = foot;
        vA_lastHandPos = hand;
    }

    public override float GetStateValue(int player)
    {
        float v = 0;

        Vector3 footAvg = Vector3.zero;
        for (int i = 0; i < vA_footPositions.Length; i++)
        {
            int dir = 1 - (2 * i);

            Vector3 thisFoot = vA_footPositions[i];
            Vector3 footTargetPos = Vector3.Scale(
                v_hipPosition + 
                Vector3.right * (v_hipPosition.x + dir * (1.1f + Mathl.NormalisedSin(AnimEnvironment.x.sideWalkTime, 1f, 1, 0, 0))) + 
                (Vector3.forward * Mathl.NormalisedSin(AnimEnvironment.x.forwardWalkTime, 2f, 1, 0, 0) * dir), 
                Vector3.one - Vector3.up);

            footAvg += thisFoot;
            v -= Mathf.Clamp((thisFoot - footTargetPos).magnitude, 0, 1000);

        }
        Vector3 handAvg = Vector3.zero;
        for (int i = 0; i < vA_handPositions.Length; i++)
        {
            int dir = 1 - (2 * i);

            Vector3 thisHand = vA_handPositions[i];
            Vector3 handTargetPos = (v_hipPosition - Vector3.up) + 
                Vector3.right * (-1.5f * dir) + 
                (Vector3.forward * Mathl.NormalisedSin(AnimEnvironment.x.forwardWalkTime, 2f, 1f, 0, 0) * dir);

            handAvg += vA_handPositions[i];
            v -= Mathf.Clamp((handTargetPos - thisHand).magnitude, 0, 1000);

        }

        footAvg /= vA_footPositions.Length;
        handAvg /= vA_handPositions.Length;

        v -= (footAvg - v_centreOfGravity).sqrMagnitude;
        v -= (handAvg - (v_hipPosition - Vector3.up)).sqrMagnitude;

        return v;

    }

    private void MovePlayer(int _player, Vector3 dir)
    {
        bool hand = false;
        //Debug.Log(_player + " | " + vA_footPositions.Length);
        if (_player >= vA_footPositions.Length)
        {
            _player -= vA_footPositions.Length;
            hand = true;
        }

        if (hand)
        {
            vA_handPositions[_player] += dir * AnimEnvironment.x.moveSpeed * Time.deltaTime;
        }
        else
        {
            vA_footPositions[_player] += dir * AnimEnvironment.x.moveSpeed * Time.deltaTime;
        }

    }

    public override object GetAvailableActions()
    {
        return vA_movements;
    }

    public override State Simulate(int playerCount, object action)
    {

        AnimState newState = (AnimState)this.MemberwiseClone();
        newState.SetLimbPositions(FootPositions, HandPositions);

        newState.MovePlayer(playerCount, (Vector3)action);

        return newState;
    }

}
