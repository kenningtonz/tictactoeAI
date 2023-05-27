//Kennedy Adams 100632983 A2
using UnityEngine;
public class Minimax : MonoBehaviour
{
    private char mPlayer;
    private char mAi;
    // int score;
    private int high = 100;
    private int low = -100;
    public int runs = 0;

    //basicly just a function where input is X, O, or T and the output is -1,1, or 0, depending on player
    int whatScore(char win)
    {
        if (win == 'X')
        {
            if (mPlayer == 'X')
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        if (win == 'O')
        {
            if (mPlayer == 'O')
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        if (win == 'T')
        {
            return 0;
        }
        return 0;
    }

    //determines the ai's move
    public int findTheBestMove()
    {
        int move = 0;

        //setting the player and ai as respective peices
        mPlayer = GetComponent<GameManager>().player;
        mAi = GetComponent<GameManager>().ai;

        int bestscore = low;
        for (int i = 0; i < 9; i++)
        {
            //  if empty spot
            if (GetComponent<GameManager>().board[i] == 'E')
            {
                //  input ai
                GetComponent<GameManager>().board[i] = mAi;
                //  evaulate score
                int score = miniMax(GetComponent<GameManager>().board, 0, false, low, high);
                //   set back to empty
                GetComponent<GameManager>().board[i] = 'E';
                //  if score is better set bestscore and the move
                if (score > bestscore)
                {
                    bestscore = score;
                    move = i;
                }


            }
        }
        return move;
    }


    int miniMax(char[] board, int depth, bool isMaxing, int alpha, int beta)
    {
        runs++;
        //checks if node is a leaf node(terminal)
        char result = GetComponent<Evaluate>().evaluateBoard(board);
        if (result != 'E')
        {
            return whatScore(result);
        }

        //for each child node
        if (isMaxing) //ai is max
        {
            int bestscore = low;

            //check all spots
            for (int i = 0; i < 9; i++)
            {
                //if avalable
                if (board[i] == 'E')
                {
                    //set that position to the AI
                    board[i] = mAi;
                    //evaulate score
                    int score = miniMax(board, depth + 1, false, alpha, beta);

                    //change back to empty
                    board[i] = 'E';

                    //the bestscore is now the better of the 2
                    bestscore = Mathf.Max(score, bestscore);

                    //alpha beta pruning
                    //check if the branches best score is worse then the best option of a past search, if it is skip it
                    alpha = Mathf.Max(alpha, bestscore);

                    if (beta <= alpha)
                    {
                        Debug.Log("pruned");
                        break;
                    }

                }
            }
            return bestscore;
        }
        else //human is max
        {
            int bestscore = high;

            //check all spots
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == 'E')
                {
                    //set that position to the player
                    board[i] = mPlayer;
                    //evaulate score
                    int score = miniMax(board, depth + 1, true, alpha, beta);
                    //change back to empty
                    board[i] = 'E';
                    //the bestscore is now the better of the 2

                    bestscore = Mathf.Min(score, bestscore);

                    //alpha beta pruning
                    //check if the branches best score is worse then the best option of a past search, if it is skip it
                    beta = Mathf.Min(beta, bestscore);
                    if (beta < alpha)
                    {
                        Debug.Log("pruned");
                        break;

                    }
                }
            }
            return bestscore;
        }
    }

}


