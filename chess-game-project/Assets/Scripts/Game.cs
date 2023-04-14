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
            Create("white_queen", 0, 0)
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
}
