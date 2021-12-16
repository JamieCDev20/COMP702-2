using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPAEnvironment : MonoBehaviour
{

    public static TPAEnvironment x;

    public float moveSpeed;
    public int playerCount;

    [SerializeField] private Transform[] players;
    [SerializeField] private Transform average;
    [SerializeField] private Transform target;

    private void Start()
    {
        x = this;
    }

    private void Update()
    {
        Vector3 avg = Vector3.zero;
        for (int i = 0; i < players.Length; i++)
        {
            avg += players[i].position;
        }
        avg /= players.Length;
        average.position = avg;
    }

    public void ActOnWorld(Vector3[] targetPositions)
    {
        Vector3 avg = Vector3.zero;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].position = targetPositions[i];
            avg += targetPositions[i];
        }
        avg /= players.Length;
        average.position = avg;
    }

    public (Vector3[] playerPos, Vector3 target) GetState()
    {
        Vector3[] v = new Vector3[players.Length];
        for (int i = 0; i < v.Length; i++)
        {
            v[i] = players[i].position;
        }
        return (v, target.position);
    }

}
