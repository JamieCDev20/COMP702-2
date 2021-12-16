using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPAState : State
{

    public Vector3[] vA_playerPositions;
    private Vector3 v_target;
    private Vector3[] vA_movements = new Vector3[6] 
    { 
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, -1, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
    };

    public TPAState()
    {
        (Vector3[] playerPos, Vector3 target) state = TPAEnvironment.x.GetState();
        vA_playerPositions = state.playerPos;
        v_target = state.target;
    }

    public Vector3[] GetPlayerPositions()
    {
        return vA_playerPositions;
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

    public void MovePlayer(int _player, Vector3 _dir)
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

        newState.MovePlayer(playerCount, (Vector3)action);

        return newState;

    }

}
