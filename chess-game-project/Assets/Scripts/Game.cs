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
    private bool blackIa = true;
    private bool whiteIa = false;

    // Start is called before the first frame update
    public void Start()
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

        blackPlayer = new GameObject[] 
        {
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
        chessman.SetName(name);
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

    public GameObject[,] GetPositions()
    {
        return positions;
    }

    public GameObject[] GetWhitePlayer()
    {
        return whitePlayer;
    }
    
    public GameObject[] GetBlackPlayer()
    {
        return blackPlayer;
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
    
    public void SerPositionSpriteEmpty(int x, int y)
    {
        positions[x, y].GetComponent<SpriteRenderer>().sprite = null;   
    }

    public void SearchAndDestroy(GameObject cp)
    {
        var i = 0;
        if (cp.GetComponent<Chessman>().GetPlayer() == "white")
        {
            for (i = 0; i < whitePlayer.Length; i++)
            {
                if (whitePlayer[i] == null) break;
                if (whitePlayer[i].Equals(cp))
                {
                    whitePlayer[i] = null;
                    break;
                }
            }

            while (i < whitePlayer.Length)
            {
                if (whitePlayer[i] != null)
                {
                    whitePlayer[i - 1] = whitePlayer[i];
                }

                i++;
            }
        }
        else
        {
            for (i = 0; i < blackPlayer.Length; i++)
            {
                if (whitePlayer == null) break;
                if (blackPlayer[i].Equals(cp))
                {
                    blackPlayer[i] = null;
                    break;
                }
            }
            while (i < blackPlayer.Length)
            {
                if (blackPlayer[i] != null)
                {
                    blackPlayer[i - 1] = blackPlayer[i];
                }

                i++;
            }
        }
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

    public bool IsBlackIa()
    {
        return blackIa;
    }

    public bool IsWhiteIa()
    {
        return whiteIa;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    // Função responsavel pela alternância do valor de currentPlayer
    public void NextTurn()
    {
        currentPlayer = currentPlayer == "white" ? "black" : "white";
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

        var isTest = GameObject.FindGameObjectWithTag("EndText");
        if (isTest == null)
        {
            return;
        }
        
        GameObject.FindGameObjectWithTag("EndText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("EndText").GetComponent<Text>().text = "O " + playerWinner + " venceu! Pressione o mouse para reiniciar";
    }

    public Vector2Int KingPositionOnMatrix()
    {   
        string kingName = currentPlayer == "white" ? "whiteKing" : "blackKing";
        
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j ++)
            {
                if (positions[i,j] != null && positions[i,j].GetComponent<Chessman>().GetName() == kingName)
                    return new Vector2Int(i, j);
            }
        }
        
        return new Vector2Int(-1, -1);
    }

    public void SetWhitePlayer(GameObject[] playerArray)
    {
        this.whitePlayer = playerArray;
    }
    
    public void SetBlackPlayer(GameObject[] playerArray)
    {
        this.blackPlayer = playerArray;
    }
}
