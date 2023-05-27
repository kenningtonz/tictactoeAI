//Kennedy Adams 100632983 A2
using UnityEngine;

public class Evaluate : MonoBehaviour
{

    //T is tie, X is xwin, O is o win
    private char winner;
    // public int openSpots = 0;
    bool equals(char a, char b, char c)
    {
        return a == b && b == c && a != 'E';
    }


    //evaulate the board
    public char evaluateBoard(char[] board)
    {
        winner = 'E';

        // horizontal
        if (equals(board[0], board[1], board[2]))
        {
            winner = board[0];
        }
        if (equals(board[3], board[4], board[5]))
        {
            winner = board[3];
        }
        if (equals(board[6], board[7], board[8]))
        {
            winner = board[6];
        }

        // verticle
        if (equals(board[0], board[3], board[6]))
        {
            winner = board[0];
        }
        if (equals(board[1], board[4], board[7]))
        {
            winner = board[1];
        }
        if (equals(board[2], board[5], board[8]))
        {
            winner = board[2];
        }

        //diag
        if (equals(board[0], board[4], board[8]))
        {
            winner = board[0];
        }
        if (equals(board[2], board[4], board[6]))
        {
            winner = board[2];
        }

        numofopenSpots(board);

        if (winner == 'E' && numofopenSpots(board) == 0)
        {
            return 'T';
        }
        else
        {
            return winner;
        }

    }


    public int numofopenSpots(char[] board)
    {
        int openSpots = 0;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 'E')
            {
                openSpots++;
            }

        }
        return openSpots;
    }
}
