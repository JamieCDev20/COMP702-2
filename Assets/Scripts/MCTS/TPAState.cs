using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPAState : State
{

    private Vector3[] vA_playerPositions;
    public Vector3[] PlayerPositions { get { return vA_playerPositions; } }
    private Vector3 v_target;
    private Vector3[] vA_movements = new Vector3[7]
    {
        Vector3.up,
        Vector3.down,
        Vector3.forward,
        Vector3.back,
        Vector3.right,
        Vector3.left,
        Vector3.zero
    };

    public TPAState()
    {
        (Vector3[] playerPos, Vector3 target) state = TPAEnvironment.x.GetState();
        vA_playerPositions = state.playerPos;
        v_target = state.target;
    }

    public override float GetStateValue(int player)
    {
        Vector3 calculatedAverage = Vector3.zero;

        for (int i = 0; i < vA_playerPositions.Length; i++)
        {
            calculatedAverage += vA_playerPositions[i];
        }

        calculatedAverage /= vA_playerPositions.Length;

        return -(calculatedAverage - v_target).sqrMagnitude;
    }

    private void MovePlayer(int _player, Vector3 _dir)
    {
        vA_playerPositions[_player] += _dir * Time.deltaTime * TPAEnvironment.x.moveSpeed;
    }

    public override object GetAvailableActions()
    {
        return vA_movements;
    }

    public override State Simulate(int playerCount, object action)
    {

        TPAState newState = (TPAState)this.MemberwiseClone();
        newState.vA_playerPositions = (Vector3[])vA_playerPositions.Clone();

        newState.MovePlayer(playerCount, (Vector3)action);

        return newState;

    }

}
