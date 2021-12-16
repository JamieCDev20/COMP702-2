using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public string nodeName = "00";
    private int depth;
    private int width;
    private Node no_parent;
    private Node[] noA_children = new Node[0];
    private State st_nodeState;
    private const float explorationValue = 0.5f;

    private float f_value = 0;
    private int i_visits = 0;
    private int i_player;

    public int GetDepth { get { return depth; } }

    public int GetVisits { get { return i_visits; } }

    public float GetAvgVal { get { return (float)(f_value / i_visits); } }

    public float GetValue { get { return f_value; } }

    /* UCB(S) = V + 2 * sqrt(ln(N)/n)
     * V = average value of the child node
     * N = number of visits of the parent node
     * n = number of visits of the child node
     */

    public Node(Node _parent, int _player, int d, int w)
    {
        //st_nodeState = new State();
        no_parent = _parent;
        i_player = _player;
        depth = d;
        width = w;
        nodeName = $"{depth}{(char)(65 + w)}";
    }

    public Node(Node _parent, State nodeState, int _player, int d, int w)
    {
        st_nodeState = nodeState;
        no_parent = _parent;
        i_player = _player;
        depth = d;
        width = w;
        nodeName = $"{depth}{(char)(65 + w)}";
    }

    public void SetChildren(Node[] newChildren)
    {
        noA_children = newChildren;
    }

    public Node GetMaxUCBChild()
    {
        int maxIndex = -1;
        float maxV = -Mathf.Infinity;

        for (int i = 0; i < noA_children.Length; i++)
        {
            int vis = noA_children[i].GetVisits;
            if (vis == 0)
                return noA_children[i];

            if (Mathl.IfMoreThenSet(ref maxV, CalculateChildUCB(noA_children[i])))
                maxIndex = i;
        }

        if (maxIndex == -1)
            throw new System.Exception("UCB value error");

        return noA_children[maxIndex];
    }

    public Node GetMaxVisitedChild()
    {
        float maxV = -Mathf.Infinity;
        int maxI = -1;

        for (int i = 0; i < noA_children.Length; i++)
        {
            //Debug.Log($"Node: {noA_children[i].nodeName} | visits: {noA_children[i].GetVisits} | value: {noA_children[i].GetAvgVal}");

            if (Mathl.IfMoreThenSet(ref maxV, noA_children[i].GetVisits))
                maxI = i;
        }

        if (maxI == -1)
            throw new System.Exception("Max visit error");

        //Debug.Log($"Chosen Node: {noA_children[maxI].nodeName}");

        return noA_children[maxI];

    }

    private float CalculateChildUCB(Node child)
    {
        //return 0;
        return child.GetAvgVal + explorationValue * Mathf.Sqrt(Mathf.Log(i_visits) / child.GetVisits);
    }

    public Node[] GenerateChildren()
    {

        Vector3[] actions = (Vector3[])st_nodeState.GetAvailableActions();
        List<Node> newChildren = new List<Node>();

        for (int i = 0; i < actions.Length; i++)
        {
            newChildren.Add(new Node(this, (TPAState)st_nodeState.Simulate(i_player, actions[i]), (i_player + 1)%TPAEnvironment.x.playerCount, depth+1, i));
        }

        noA_children = newChildren.ToArray();

        return noA_children;
    }

    public int GetChildCount()
    {
        return noA_children.Length;
    }

    public State GetState()
    {
        return st_nodeState;
    }

    public Node GetParent()
    {
        return no_parent;
    }

    public void Propagate(float delta)
    {
        //Debug.Log("Propagating " + delta + "at node: " + nodeName);
        f_value += delta;
        i_visits++;
        //Debug.Log(num);
        if (no_parent == null)
            return;
        no_parent.Propagate(delta);
    }

    private void SetVisits(int val)
    {
        i_visits = val;
    }

}
