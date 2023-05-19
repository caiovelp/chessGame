using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] whitePlayer = new GameObject[16];
    private GameObject[] blackPlayer = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        whitePlayer = new GameObject[] 
        {
            Create("whitePawn", 0, 1), Create("whitePawn", 1, 1), Create("whitePawn", 2, 1), 
            Create("whitePawn", 3, 1), Create("whitePawn", 4, 1), Create("whitePawn", 5, 1), 
            Create("whitePawn", 6, 1), Create("whitePawn", 7, 1),
            Create("whiteTower", 0, 0), Create("whiteKnight", 1, 0), Create("whiteBishop", 2, 0), 
            Create("whiteQueen", 3, 0), Create("whiteKing", 4, 0), 
            Create("whiteBishop", 5, 0), Create("whiteKnight", 6, 0), Create("whiteTower", 7, 0),
        };

        blackPlayer = new GameObject[] {
        
            Create("blackPawn", 0, 6), Create("blackPawn", 1, 6), Create("blackPawn", 2, 6),
            Create("blackPawn", 3, 6), Create("blackPawn", 4, 6), Create("blackPawn", 5, 6),
            Create("blackPawn", 6, 6), Create("blackPawn", 7, 6),
            Create("blackTower", 0, 7), Create("blackKnight", 1, 7), Create("blackBishop", 2, 7),
            Create("blackQueen", 3, 7), Create("blackKing", 4, 7),
            Create("blackBishop", 5, 7), Create("blackKnight", 6, 7), Create("blackTower", 7, 7)
        };

        // Coloca as peças no tabuleiro
        for (int i = 0; i < whitePlayer.Length; i++) 
        {
            SetPosition(whitePlayer[i]);
        }

        for (int i = 0; i < blackPlayer.Length; i++) 
        {
            SetPosition(blackPlayer[i]);
        }
    }

    // Função responsável por criar as peças no tabuleiro.
    public GameObject Create(string name, int x, int y) 
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman chessman = obj.GetComponent<Chessman>();
        chessman.name = name;
        chessman.SetXBoard(x);
        chessman.SetYBoard(y);
        chessman.Activate();

        return obj;
    }

    // Funções de Set e Get da posição de um GameObject.
    public void SetPosition(GameObject obj) 
    {
        Chessman chessman = obj.GetComponent<Chessman>();

        positions[chessman.GetXBoard(), chessman.GetYBoard()] = obj;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    // Função de define que uma posição (x, y) fique vazia.
    public void SetPositionEmpty(int x, int y) 
    {
        positions[x, y] = null;
    }


    // Função verifica se dado um valor (x, y), esse par está dentro do tabuleiro 8x8.
    public bool PositionOnBoard(int x, int y) 
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

     public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    // Função responsavel pela alternância do valor de currentPlayer
    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        
        GameObject.FindGameObjectWithTag("EndText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("EndText").GetComponent<Text>().text = "O " + playerWinner + " venceu! Pressione o mouse para reiniciar";
    }
}
