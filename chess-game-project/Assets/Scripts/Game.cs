using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8,8];
    //Terá de ter outro para as peças pretas
    private GameObject[] whitePlayer = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        whitePlayer = new GameObject[] 
        {
            //Colocar as outras peças aqui
            Create("whitePawn", 0, 1), Create("whitePawn", 1, 1), Create("whitePawn", 2, 1), Create("whitePawn", 3, 1), Create("whitePawn", 4, 1), Create("whitePawn", 5, 1), Create("whitePawn", 6, 1), Create("whitePawn", 7, 1),
            Create("whiteTower", 0, 0), Create("whiteKnight", 1, 0), Create("whiteBishop", 2, 0), Create("whiteQueen", 3, 0), Create("whiteKing", 4, 0), Create("whiteBishop", 5, 0), Create("whiteKnight", 6, 0), Create("whiteTower", 7, 0)
        };

        //Coloca as peças no tabuleiro
        for (int i = 0; i < whitePlayer.Length; i++) 
        {
            SetPosition(whitePlayer[i]);
        }
    }

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

    public void SetPosition(GameObject obj) 
    {
        Chessman chessman = obj.GetComponent<Chessman>();

        positions[chessman.GetXBoard(), chessman.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y) 
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y) 
    {
        //verifica se uma posição está no está no tabuleiro
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }
}
