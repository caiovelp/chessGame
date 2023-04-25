using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Chessman : MonoBehaviour
{

    // GameObjects
    public GameObject controller;
    public GameObject movePlate;

    // Posições
    private int xBoard = -1;
    private int yBoard = -1;

    // Jogador
    private string player;

    // Peças de xadrez
    public Sprite whiteQueen, whiteKing, whiteBishop, whiteTower, whiteKnight, whitePawn;
    public Sprite blackQueen, blackKing, blackBishop, blackTower, blackKnight, blackPawn;

    /* 
        Função responsável por "ativar" as peças, ou seja, ela define as posições das peças,
        ativa o controlador e define o player que está interagindo com aquela peça.
    */
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Ajusta as posições das peças
        SetCoordinates();

        switch (this.name)
        {
            case "whiteQueen": this.GetComponent<SpriteRenderer>().sprite = whiteQueen; player = "white"; break;
            case "whiteKing": this.GetComponent<SpriteRenderer>().sprite = whiteKing; player = "white"; break;
            case "whiteBishop": this.GetComponent<SpriteRenderer>().sprite = whiteBishop; player = "white"; break;
            case "whiteTower": this.GetComponent<SpriteRenderer>().sprite = whiteTower; player = "white"; break;
            case "whiteKnight": this.GetComponent<SpriteRenderer>().sprite = whiteKnight; player = "white"; break;
            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn; player = "white"; break;

            case "blackQueen": this.GetComponent<SpriteRenderer>().sprite = blackQueen; player = "black"; break;
            case "blackKing": this.GetComponent<SpriteRenderer>().sprite = blackKing; player = "black"; break;
            case "blackBishop": this.GetComponent<SpriteRenderer>().sprite = blackBishop; player = "black"; break;
            case "blackTower": this.GetComponent<SpriteRenderer>().sprite = blackTower; player = "black"; break;
            case "blackKnight": this.GetComponent<SpriteRenderer>().sprite = blackKnight; player = "black"; break;
            case "blackPawn": this.GetComponent<SpriteRenderer>().sprite = blackPawn; player = "black"; break;
        }
    }

    // Função responsável por ajustar a posição das peças.
    public void SetCoordinates() 
    {
        float x = xBoard;
        float y = yBoard;

        /* 
            Os valores abaixam fazem um fit da posição das peças de maneira que elas fiquem
            encaixadas no tabuleiro, ou seja, elas encaixaram em uma matriz 8x8, de maneira
            que possam ser descritas como pos[i][j], onde i e j são posições da matriz aka
            tabuleiro de xadrez.
        */
        x *= 1.15f;
        y *= 1.15f;

        x -= 4f;
        y -= 3.4f;

        this.transform.position = new Vector3(x, y, -2 + y/100);
    }

    /* 
        Funções GET e SET necessárias para recuperar e alterar valores de x e y, que definem
        a posição da matriz como matriz[x][y]
    */
    public int GetXBoard() 
    {
        return xBoard;
    }

    public int GetYBoard() 
    {
        return yBoard;
    }

    public void SetXBoard(int x) 
    {
        xBoard = x;
    }

    public void SetYBoard(int y) 
    {
        yBoard = y;
    }

    /*
        Função do Unity que é chamada quando o usuário clica e solta o botão do mouse.
        Nesse caso, essa OnMouseUp é responsável pelo desenho dos moveplates.
    */
    private void OnMouseUp()
    {
        // Apaga os moveplates que estão no tabuleiro.
        DestroyMovePlates();

        // Inicia os novos moveplates depedendo da interação.
        InitiateMovePlates();
    }

    // Função responsável por apagar os moveplates que estão desenhadas no tabuleiro
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        foreach (var movePlate in movePlates)
        {
            Destroy(movePlate);
        }
    }

    // Função responsável por desenhar os moveplates no tabuleiro de acordo com a peça clicada.
    public void InitiateMovePlates() 
    {
        //Um para cada peça
        switch (this.name)
        {
            case "blackPawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "whitePawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "blackQueen":
            case "whiteQueen": 
                CrossMovePlate();
                AxisMovePlate();
                break;
            case "whiteKnight":
            case "blackKnight":
                LMovePlate();
                break;
            case "blackKing":
            case "whiteKing":
                SurroundMovePlate();
                break;
            case "blackBishop":
            case "whiteBishop":
                CrossMovePlate();
                break;
            case "blackTower":
            case "whiteTower": 
                AxisMovePlate();
                break;
        }
    }

    public void CrossMovePlate()
    {
        LineMovePlate(1,1);
        LineMovePlate(1,-1);
        LineMovePlate(-1,1);
        LineMovePlate(-1,-1);
    }

    public void AxisMovePlate()
    {
        LineMovePlate(1,0);
        LineMovePlate(-1,0);
        LineMovePlate(0,1);
        LineMovePlate(0,-1);
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null){
            MovePlateSpawn(x,y);
            x += xIncrement;
            y += yIncrement;
        }

        if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<Chessman>().player != player){
            MovePlateAttackSpawn(x,y);
        }
    }

    // Desenha os moveplate do peão.
    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }
    
    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game gameScript = controller.GetComponent<Game>();
        if (gameScript.PositionOnBoard(x, y))
        {
            GameObject chessPiece = gameScript.GetPosition(x, y);
            // Verifica se posição da jogada tem uma peça
            // Se sim, invoca a MovePlate na posição
            // Caso contrário, se o player da peça é diferente do atual invoca a MovePlate de ataque na posição
            if (chessPiece == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (chessPiece.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    // Desenha os moveplates de acordo com uma matrix 8x8
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        // Recupera o valor do tabuleiro para converter em xy coordenadas
        float x = matrixX;
        float y = matrixY;

        // Ajuste do offset para ficar de acordo com uma matrix 8x8
        x *= 1.15f;
        y *= 1.15f;

        x -= 4f;
        y -= 3.6f;

        // Cria o gameobject do moveplate
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        // Cria uma instância do moveplate e interage com essa instância.
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoordinates(matrixX, matrixY);
    }

    // Desenha os moveplates de acordo com uma matrix 8x8 trocando a flag de ataque para true.
    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        // Recupera o valor do tabuleiro para converter em xy coordenadas
        float x = matrixX;
        float y = matrixY;

        // Ajuste do offset para ficar de acordo com uma matrix 8x8
        x *= 1.15f;
        y *= 1.15f;

        x -= 4f;
        y -= 3.6f;

        // Cria o gameobject do moveplate
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        // Cria uma instância do moveplate e interage com essa instância, flag attack = true.
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoordinates(matrixX, matrixY);
    }
}
