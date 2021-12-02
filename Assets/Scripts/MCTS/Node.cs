using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    private Node no_parent;
    private Node[] noA_children = new Node[0];
    private State st_nodeState;

    private float f_value = 0;
    private int i_visits = 0;
    private int myVar;

    public float GetAvgVal { get { return (float)(f_value / i_visits); } }

    public float GetValue { get { return f_value; } }

    /* UCB(S) = V + 2 * sqrt(ln(N)/n)
     * V = average value of the child node
     * N = number of visits of the parent node
     * n = number of visits of the child node
     */

    public Node(Node _parent)
    {
        st_nodeState = new State();
        no_parent = _parent;
    }

    public Node(Node _parent, State nodeState)
    {
        st_nodeState = nodeState;
        no_parent = _parent;
    }

    public void SetChildren(Node[] newChildren)
    {
        noA_children = newChildren;
    }

    public int GetVisits()
    {
        return i_visits;
    }

    public Node GetMaxUCBChild()
    {
        int maxIndex = -1;
        float maxV = -1;

        for (int i = 0; i < noA_children.Length; i++)
        {
            int vis = noA_children[i].GetVisits();
            if (vis == 0)
                return noA_children[i];

            if(Mathl.IfMoreThenSet(ref maxV, CalculateChildUCB(noA_children[i])))
                maxIndex = i;
        }

        if (maxIndex == -1)
            return this;

        return noA_children[maxIndex];
    }

    public Node GetMaxValueChild()
    {
        float maxV = -1000;
        int maxI = -1;

        for (int i = 0; i < noA_children.Length; i++)
        {
            if (Mathl.IfMoreThenSet(ref maxV, noA_children[i].GetValue))
                maxI = i;
        }

        if (maxI == -1)
            return this;
        return noA_children[maxI];

    }

    private float CalculateChildUCB(Node child)
    {
        return (child.GetAvgVal + (2 * Mathf.Sqrt(Mathf.Log(i_visits) / child.GetVisits())));
    }

    public Node[] GenerateChildren(int _player)
    {

        (int x, int y)[] actions = st_nodeState.GetAvailableActions();
        List<Node> newChildren = new List<Node>();

        for (int i = 0; i < actions.Length; i++)
        {
            newChildren.Add(new Node(this, st_nodeState.Simulate(_player, actions[i])));
        }

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

    public Node Propagate(float delta)
    {
        f_value += delta;
        i_visits += 1;
        return no_parent;
    }

}
