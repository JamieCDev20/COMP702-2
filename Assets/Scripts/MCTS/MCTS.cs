using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCTS : MonoBehaviour
{

    private Node no_rootNode;

    private Node no_currentNode;

    private const int i_rolloutLimit = 10;
    private const int i_iterationLimit = 5;
    private const int i_iterationBatchSize = 2;
    private const float f_timeLimit = 1f;

    private int playerNumber = 0;

    private void Start()
    {
        UpdateState();
        StartCoroutine(SelfPlay());

    }

    IEnumerator SelfPlay()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 8; i++)
        {
            StartCoroutine(RunIterations());
            yield return new WaitForSeconds(0.3f);
            MakeMove();
            yield return new WaitForSeconds(0.2f);
            UpdateState();
        }
    }

    public void MakeMove()
    {
        State nextState = GetCurrentBestAction();

        State.ActOnWorld(playerNumber, State.GetActionFromStateChange(no_rootNode.GetState(), nextState));

        playerNumber = 1 - playerNumber;
        
    }

    public void UpdateState()
    {
        no_rootNode = new Node(null, State.GetStateFromWorld());
        StartCoroutine(RunIterations());
    }

    IEnumerator RunIterations()
    {
        float startTime = Time.realtimeSinceStartup;
        int its = 0;

        while (Time.realtimeSinceStartup - startTime < f_timeLimit)
        {
            for (int i = 0; i < i_iterationBatchSize; i++)
            {

                Iterate();
                its++;
                if (its >= i_iterationLimit)
                    startTime -= f_timeLimit * 2;
                    
            }
            yield return null;
        }
    }

    private void Iterate()
    {
        no_currentNode = no_rootNode;
        float rolloutValue = 0;

        //Selection - Search the tree for a node with the highest UCB value
        Select();

        //Expansion - Get to an unexplored child node by adding the next states onto a node that has already been explored
        Expand();

        //Rollout - Do a random playout from this state to a terminal state
        rolloutValue = Rollout();

        //Backprop - Propagate back up the tree and adjust the number of visits and the total value
        BackProp(rolloutValue);

    }

    private State GetCurrentBestAction()
    {
        Node bestValNode = no_rootNode.GetMaxValueChild();
        return bestValNode.GetState();
    }

    private void Select()
    {

        if(no_currentNode.GetChildCount() != 0)
        {
            no_currentNode = no_currentNode.GetMaxUCBChild();
            Select();
            return;
        }
        return;

    }

    private void Expand()
    {
        if (no_currentNode.GetVisits() != 0)
        {
            no_currentNode.GenerateChildren(playerNumber);
            no_currentNode = no_currentNode.GetMaxUCBChild();
        }
    }

    private float Rollout()
    {
        State curSimmedState = no_currentNode.GetState();

        for (int i = 0; i < i_rolloutLimit; i++)
        {
            if (curSimmedState.terminal)
                return curSimmedState.GetStateValue(playerNumber);

            curSimmedState = curSimmedState.Simulate(playerNumber, curSimmedState.GetAvailableActions().ChooseRandom());

        }

        return 0;
    }

    private void BackProp(float delta)
    {
        Node cur = no_currentNode;
        while (cur != null)
            cur = cur.Propagate(delta);
    }

}
