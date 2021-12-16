using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCTS : MonoBehaviour
{

    private Node no_rootNode;
    private Node no_currentNode;

    private int i_playerNumber = 0;
    [SerializeField] private int i_mctsIterations = 150;
    [SerializeField] private int i_rolloutDepth = 6;

    private void Start()
    {
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
        TPAEnvironment.x.ActOnWorld(((TPAState)nextState).vA_playerPositions);

        //print("Acted in world");
        i_playerNumber = (i_playerNumber + 1) % TPAEnvironment.x.playerCount;
        //Debug.Log(i_playerNumber);

    }

    public void UpdateState()
    {

        //print("Updated State");
        no_rootNode = new Node(null, new TPAState(), i_playerNumber, 0, 0);

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
        State curState = no_currentNode.GetState();
        for (int i = 0; i < i_rolloutDepth - no_currentNode.GetDepth; i++)
        {
            curState = curState.Simulate((_player + i) % TPAEnvironment.x.playerCount, ((Vector3[])curState.GetAvailableActions()).ChooseRandom());
        }

        return ((TPAState)curState).GetStateValue(_player);
    }

    private void BackProp(float delta)
    {
        Node curNode = no_currentNode;
        curNode.Propagate(delta);
    }

}
