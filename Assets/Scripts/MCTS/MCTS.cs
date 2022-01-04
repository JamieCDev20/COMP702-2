using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCTS : MonoBehaviour
{

    private Node no_rootNode;
    private Node no_currentNode;

    private Node no_bestValueNode;

    private int i_playerNumber = 0;
    [SerializeField] private int i_mctsIterations = 150;
    [SerializeField] private int i_rolloutDepth = 6;

    private void Start()
    {
        no_bestValueNode = new Node(null, new AnimState(), i_playerNumber, 0, 0);
        StartCoroutine(GoForIt());
    }

    IEnumerator GoForIt()
    {
        while (true)
        {
            UpdateState();
            for (int i = 0; i < i_mctsIterations; i++)
            {
                //print("performing iteration: " + i);
                Iterate();

            }
            //print("Finished iterations");
            yield return null;
            MakeMove();
        }
    }

    public void MakeMove()
    {
        //print("making move");

        State nextState = GetCurrentBestAction();

        //print("Got best action");
        AnimState state = (AnimState)nextState;
        AnimEnvironment.x.ActOnWorld((state.FootPositions, state.HandPositions));

        //print("Acted in world");
        i_playerNumber = (i_playerNumber + 1) % AnimEnvironment.x.playerCount;
        //Debug.Log(i_playerNumber);

    }

    public void UpdateState()
    {

        //print("Updated State");
        no_rootNode = new Node(null, new AnimState(), i_playerNumber, 0, 0);
        //no_rootNode = no_bestValueNode;
        //no_rootNode.ClearParent();
        //Debug.Log("New root: " + no_rootNode.nodeName);
        //StartCoroutine(RunIterations());
    }

    private void Iterate()
    {
        no_currentNode = no_rootNode;
        float rolloutValue = 0;
        //print("Select");
        //Selection - Search the tree for a node with the highest UCB value
        Select();

        //print("Expand");
        //Expansion - Get to an unexplored child node by adding the next states onto a node that has already been explored
        Expand();

        //Rollout - Do a random playout from this state to a terminal state
        //print("Rollout");
        rolloutValue = Rollout(i_playerNumber);

        //Backprop - Propagate back up the tree and adjust the number of visits and the total value
        //print("Backprop");
        BackProp(rolloutValue);

        //Debug.Log($"Iteration Completed. \n Root node stats: \nvisits: {no_rootNode.GetVisits()} Value: {no_rootNode.GetAvgVal}");

    }

    private State GetCurrentBestAction()
    {
        Node bestValNode = no_rootNode.GetMaxVisitedChild();
        no_bestValueNode = bestValNode;
        return bestValNode.GetState();
    }

    private void Select()
    {
        Node curNode = no_rootNode;

        while (curNode.GetChildCount() > 0)
        {
            curNode = curNode.GetMaxUCBChild();
        }
        no_currentNode = curNode;
    }

    private void Expand()
    {
        if (no_currentNode.GetVisits > 0)
        {
            no_currentNode.GenerateChildren();
            no_currentNode = no_currentNode.GetMaxUCBChild();
        }
    }

    private float Rollout(int _player)
    {
        AnimState curState = (AnimState)no_currentNode.GetState();

        for (int i = 0; i < i_rolloutDepth - no_currentNode.GetDepth; i++)
        {
            //Debug.Log((_player + i) % AnimEnvironment.x.playerCount);
            (Vector3[] f, Vector3[] h) lastPos = (curState.FootPositions, curState.HandPositions);
            curState = (AnimState)curState.Simulate((_player + i) % AnimEnvironment.x.playerCount, ((Vector3[])curState.GetAvailableActions()).ChooseRandom());
            curState.SetLastLimbPositions(lastPos.f, lastPos.h);
        }

        return ((AnimState)curState).GetStateValue(_player);
    }

    private void BackProp(float delta)
    {
        Node curNode = no_currentNode;
        curNode.Propagate(delta);
    }

}
