//Kennedy Adams 100632983 A2
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    //E is empty spot
    public char[] board = new char[9] { 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E' };
    public char ai;
    public char player;
    public char currentPlayer;
    public int aiMove;

    //gameobjects
    public GameObject mainmenu;
    public GameObject[] spots = new GameObject[9];
    public GameObject X;
    public GameObject O;
    public Text endtext;
    public GameObject endmenu;

    // Start is called before the first frame update
    void Start()
    {
        board = new char[9] { 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E' };
        currentPlayer = 'C';
        player = ' ';
        ai = ' ';
    }

    //player picking x
    public void xButton()
    {
        player = 'X';
        ai = 'O';
        mainmenu.SetActive(false);
        for (int i = 0; i < 9; i++)
        {
            spots[i].SetActive(true);
        }
        currentPlayer = player;
    }

    //player picking o
    public void oButton()
    {
        player = 'O';
        ai = 'X';
        mainmenu.SetActive(false);
        currentPlayer = ai;
        for (int i = 0; i < 9; i++)
        {
            spots[i].SetActive(true);
        }
        currentPlayer = ai;

    }

    //ai places peice
    public void placePeiceAI()
    {
        aiMove = GetComponent<Minimax>().findTheBestMove();
        board[aiMove] = ai;
        if (ai == 'X')
        {
            Instantiate(X, spots[aiMove].transform);
            spots[aiMove].tag = "X";
        }
        else
        {
            Instantiate(O, spots[aiMove].transform);
            spots[aiMove].tag = "O";
        }
        currentPlayer = player;
    }

    //player placing peice
    public void placePeice(int i)
    {
        board[i] = player;
        if (player == 'X')
        {
            Instantiate(X, spots[i].transform);
            spots[i].tag = "X";
        }
        else
        {
            Instantiate(O, spots[i].transform);
            spots[i].tag = "O";
        }
        if (GetComponent<Evaluate>().numofopenSpots(board) != 0)
        {

            currentPlayer = ai;
        }
    }

    void Update()
    {

        //X win
        if (GetComponent<Evaluate>().evaluateBoard(board) == 'X')
        {
            endtext.color = new Color32(162, 255, 165, 255);
            endtext.text = "X wins";
            endmenu.SetActive(true);
            for (int i = 0; i < 9; i++)
            {
                spots[i].GetComponent<BoxCollider>().enabled = false;
                if (spots[i].tag == "X")
                {
                    spots[i].transform.GetChild(0).gameObject.transform.Find("Particle X").gameObject.SetActive(true);

                }
            }
        }
        //O win
        else if (GetComponent<Evaluate>().evaluateBoard(board) == 'O')
        {
            endtext.color = new Color32(114, 204, 255, 255);
            endtext.text = "O wins";
            endmenu.SetActive(true);
            for (int i = 0; i < 9; i++)
            {
                spots[i].GetComponent<BoxCollider>().enabled = false;
                if (spots[i].tag == "O")
                {
                    spots[i].transform.GetChild(0).gameObject.transform.Find("Particle O").gameObject.SetActive(true);
                }
            }
        }
        //Tie
        else if (GetComponent<Evaluate>().evaluateBoard(board) == 'T')
        {
            endtext.text = "Tie!";
            endmenu.SetActive(true);
            for (int i = 0; i < 9; i++)
            {
                spots[i].GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (currentPlayer == ai)
        {
            placePeiceAI();
        }
        //if player clicks a boxcollider and its empty
        for (int i = 0; i < 9; i++)
        {
            if (spots[i].GetComponent<Places>().isClicked && currentPlayer == player)
            {
                //checks if empty
                if (board[i] == 'E')
                {
                    placePeice(i);
                }
                spots[i].GetComponent<Places>().isClicked = false;
            }
        }
    }

    public void play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void exit()
    {
        Application.Quit();
    }
}
