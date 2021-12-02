using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{

    public int[][] gameState = new int[][]
    {
        new int[] {-1, -1, -1},
        new int[] {-1, -1, -1},
        new int[] {-1, -1, -1}
    };

    public (int x, int y)[][] terminalChecks = new (int x, int y)[][]
    {
        new (int x, int y)[] {(0, 0), (0, 1), (0, 2)},
        new (int x, int y)[] {(1, 0), (1, 1), (1, 2)},
        new (int x, int y)[] {(2, 0), (2, 1), (2, 2)},
        new (int x, int y)[] {(0, 0), (1, 0), (2, 0)},
        new (int x, int y)[] {(0, 1), (1, 1), (2, 1)},
        new (int x, int y)[] {(0, 2), (1, 2), (2, 2)},
        new (int x, int y)[] {(0, 0), (1, 1), (2, 2)},
        new (int x, int y)[] {(2, 0), (1, 1), (0, 2)},
    };

    public bool terminal = false;
    public int winPlayer = -1;

    public static State GetStateFromWorld()
    {
        State currentWorldState = new State();
        currentWorldState.SetState(XOManager.x.GetState());
        return currentWorldState;
    }

    public void SetState(int[][] curState)
    {
        gameState = curState;
        terminal = CheckStateIsTerminal();
    }

    private bool CheckStateIsTerminal()
    {
        bool hasWinner = false;
        for (int i = 0; i < terminalChecks.Length; i++)
        {
            (int x, int y)[] curCheck = terminalChecks[i];
            int refPlayer = gameState[curCheck[0].x][curCheck[0].y];
            for (int x = 1; x < curCheck.Length; x++)
            {
                if (gameState[curCheck[x].x][curCheck[x].y] != refPlayer)
                {
                    hasWinner = false;
                    break;
                }
                hasWinner = true;
            }
            if (hasWinner)
            {
                winPlayer = refPlayer;
                return true;
            }
        }

        for (int x = 0; x < gameState.Length; x++)
        {
            for (int y = 0; y < gameState[x].Length; y++)
            {
                if (gameState[x][y] == -1)
                    return false;
            }
        }

        return true;
    }

    public float GetStateValue(int player)
    {
        if (terminal)
        {
            if (winPlayer == -1)
                return 0;
            return (player == winPlayer ? 10 : -10);
        }
        return 0;
    }

    public static (int x, int y) GetActionFromStateChange(State uState, State dState)
    {
        int[][] initState = uState.gameState;
        int[][] endState = dState.gameState;
        for (int x = 0; x < initState.Length; x++)
        {
            for (int y = 0; y < initState[x].Length; y++)
            {
                if (initState[x][y] != endState[x][y])
                    return (x, y);
            }
        }

        return (Random.Range(0, 3), Random.Range(0, 3));
    }

    public (int x, int y)[] GetAvailableActions()
    {

        List<(int x, int y)> actions = new List<(int x, int y)>();
        for (int x = 0; x < gameState.Length; x++)
        {
            for (int y = 0; y < gameState[x].Length; y++)
            {
                if (gameState[x][y] == -1)
                    actions.Add((x, y));
            }
        }
        return actions.ToArray();
    }

    public State Simulate(int player, (int x, int y) action)
    {
        int[][] newGameState = gameState;
        newGameState[action.x][action.y] = player;
        
        State nextState = new State();
        nextState.SetState(newGameState);
        
        return nextState;
    }

    public static void ActOnWorld(int player, (int x, int y) action)
    {
        Debug.Log($"Player {(player == 0 ? "X" : "O")} doing action: {action.x},{action.y}");
        XOManager.x.SetSlot(player, action.x, action.y);
    }

}
