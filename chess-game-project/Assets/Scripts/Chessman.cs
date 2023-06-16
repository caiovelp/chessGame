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

    private string pieceName;

    // Peças de xadrez
    public Sprite whiteQueen, whiteKing, whiteBishop, whiteTower, whiteKnight, whitePawn;
    public Sprite blackQueen, blackKing, blackBishop, blackTower, blackKnight, blackPawn;

    private List<GameObject> movePlates = new List<GameObject>();
    private List<Vector2Int> avaliableMoves = new List<Vector2Int>();

    /* 
        Função responsável por "ativar" as peças, ou seja, ela define as posições das peças,
        ativa o controlador e define o player que está interagindo com aquela peça.
    */
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Ajusta as posições das peças
        SetCoordinates();

        switch (this.pieceName)
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
        x *= 0.95f;
        y *= 0.95f;

        x -= 3.32f;
        y -= 2.82f;

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

    public void SetName( string name)
    {
        pieceName = name;
    }

    public string GetName()
    {
        return pieceName;
    }

    /*
        Função do Unity que é chamada quando o usuário clica e solta o botão do mouse.
        Nesse caso, essa OnMouseUp é responsável pelo desenho dos moveplates.
    */
    private void OnMouseUp()
    {
        // Só habilita a jogada se o for a vez do jogador da peça selecionada
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            // Apaga os moveplates que estão no tabuleiro.
            DestroyMovePlates();

            // Inicia os novos moveplates depedendo da interação.
            InitiateMovePlates();
        }
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
    public List<GameObject> InitiateMovePlates() 
    {
        //Um para cada peça
        switch (this.pieceName)
        {
            case "blackPawn":
                return PawnMovePlate(xBoard, yBoard - 1);
            case "whitePawn":
                return PawnMovePlate(xBoard, yBoard + 1);
            case "blackQueen":
            case "whiteQueen": 
                List<GameObject> mp = CrossMovePlate();
                mp.AddRange(AxisMovePlate());
                return mp;
            case "whiteKnight":
            case "blackKnight":
                return LMovePlate();
            case "blackKing":
            case "whiteKing":
                return SurroundMovePlate();
            case "blackBishop":
            case "whiteBishop":
                return AxisMovePlate();
            case "blackTower":
            case "whiteTower": 
                return CrossMovePlate();
            default:
                return new List<GameObject>();
        }
    }

    // Desenha os moveplate do peão.
    public List<GameObject> PawnMovePlate(int x, int y)
    {   
        List<GameObject> moves = new List<GameObject>();
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            
            // Se for a posição inicial do peão, spawnar dois movePlate
            if (sc.GetCurrentPlayer() == "white" && y == 2)
            {
                if (sc.GetPosition(x, y) == null)
                {
                    moves.Add(MovePlateSpawn(x, y));
                    moves.Add(MovePlateSpawn(x, y + 1));
                }
            }
            else if (sc.GetCurrentPlayer() == "black" && y == 5)
            {
                if (sc.GetPosition(x, y) == null)
                {
                    moves.Add(MovePlateSpawn(x, y));
                    moves.Add(MovePlateSpawn(x, y - 1));
                }
            }
            else 
            {
                if (sc.GetPosition(x, y) == null)
                {
                    moves.Add(MovePlateSpawn(x, y));
                }
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
        return moves;
    }

    /*
        Função que cria as opções em movimentação em X de acordo com o tabuleiro
    */
    public List<GameObject> CrossMovePlate()
    {
        List<GameObject> moves = new List<GameObject>();
        GameObject[,] positions = controller.GetComponent<Game>().GetPositions();
        avaliableMoves = ChessPieceMoves("cross", ref positions, true);
        foreach (Vector2Int move in avaliableMoves)
            moves.Add(PointMovePlate(move.x, move.y));
        return moves;
    }

    /*
        Função que cria as opções em movimentação em + de acordo com os eixos do tabuleiro
    */
    public List<GameObject> AxisMovePlate()
    {
        List<GameObject> moves = new List<GameObject>();
        GameObject[,] positions = controller.GetComponent<Game>().GetPositions();
        avaliableMoves = ChessPieceMoves("axis", ref positions, true);
        foreach (Vector2Int move in avaliableMoves)
            moves.Add(PointMovePlate(move.x, move.y));
        return moves;
    }
    
    /*
        Função que cria as opções de movimentação somente em volta da peça
    */
    public List<GameObject> SurroundMovePlate()
    {
        List<GameObject> moves = new List<GameObject>();
        GameObject[,] positions = controller.GetComponent<Game>().GetPositions();
        avaliableMoves = ChessPieceMoves("surround", ref positions);
        foreach (Vector2Int move in avaliableMoves)
            moves.Add(PointMovePlate(move.x, move.y));
        return moves;
    }

    // Função resposável por ditar os movimento em "L"
    public List<GameObject> LMovePlate()
    {
        List<GameObject> moves = new List<GameObject>();
        GameObject[,] positions = controller.GetComponent<Game>().GetPositions();
        avaliableMoves = ChessPieceMoves("L", ref positions);
        foreach (Vector2Int move in avaliableMoves)
            moves.Add(PointMovePlate(move.x, move.y));
        return moves;
    }

    // Função resposável por invocar a MovePlate nas coordenadas informados
    public GameObject PointMovePlate(int x, int y)
    {
        Game game = controller.GetComponent<Game>();
        GameObject chessPiece = game.GetPosition(x, y);
        GameObject mp = null;
        if (chessPiece == null)
            mp = MovePlateSpawn(x, y);
        else if (chessPiece.GetComponent<Chessman>().player != player)
            mp =  MovePlateAttackSpawn(x, y);
    
        return mp;
    }

    // Desenha os moveplates de acordo com uma matrix 8x8
    public GameObject MovePlateSpawn(int matrixX, int matrixY)
    {
        // Recupera o valor do tabuleiro para converter em xy coordenadas
        float x = matrixX;
        float y = matrixY;

        // Ajuste do offset para ficar de acordo com uma matrix 8x8
        x *= 0.95f;
        y *= 0.95f;

        x -= 3.32f;
        y -= 2.99f;

        // Cria o gameobject do moveplate
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        // Cria uma instância do moveplate e interage com essa instância.
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoordinates(matrixX, matrixY);
        return mp;
    }

    // Desenha os moveplates de acordo com uma matrix 8x8 trocando a flag de ataque para true.
    public GameObject MovePlateAttackSpawn(int matrixX, int matrixY)
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
        return mp;
    }

    public void CallOnMouseUp()
    {
        OnMouseUp();
    }

    public List<Vector2Int> PawnMoves(ref GameObject[,] positions) 
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        Game game = controller.GetComponent<Game>();
        int[,] coords = player == "black" ? new int[2,2] {{-1,-1},{1,-1 }} : new int[2,2] {{-1,1},{1,1}};
        for (int i = 0; i < coords.GetLength(0); i++)
        {   
            int x = xBoard + coords[i, 0];
            int y = yBoard + coords[i, 1];
            if (game.PositionOnBoard(x, y))
            {
                GameObject chessPiece = positions[x, y];
                if (chessPiece != null && chessPiece.GetComponent<Chessman>().player != player )
                    moves.Add(new Vector2Int(x, y));
            }
        }
        return moves;
    }

    public List<Vector2Int> ChessPieceMoves(string movementName, ref GameObject[,] positions, bool line = false)
    {   
        int [,] coords = new int[1,1];
        List<Vector2Int> moves = new List<Vector2Int>();
        Game game = controller.GetComponent<Game>();
        switch(movementName){
            case "axis":
                coords = new int[4,2] {{1,1},{1,-1},{-1,1},{-1,-1}};
            break;
            case "cross":
                coords = new int[4,2] {{1,0},{-1,0},{0,1},{0,-1}};
            break;
            case "surround":
                coords = new int[8,2] {{1,1},{1,0},{1,-1},{-1,1},{-1,0},{-1,-1},{0,1},{0,-1}};
            break;
            case "L":
                coords = new int[8,2] {{1,2},{-1,2},{2,1},{2,-1},{1,-2},{-1,-2},{-2,1},{-2,-1}};
            break;
        }
        bool aux = line;
        for (int i = 0; i < coords.GetLength(0); i++)
        {   
            int x = xBoard + coords[i, 0];
            int y = yBoard + coords[i, 1];
            do
            {
                if (!game.PositionOnBoard(x, y)) aux = false;
                else
                {
                    GameObject chessPiece = positions[x, y];
                    if (chessPiece != null)
                    {
                        if (chessPiece.GetComponent<Chessman>().player != player)
                            moves.Add(new Vector2Int(x, y));

                        aux = false;
                    }
                    else
                    {
                        moves.Add(new Vector2Int(x, y));
                        x += coords[i, 0];
                        y += coords[i, 1];
                    }
                }
            }
            while(aux);
            aux = line;
        }
        return moves;
    }

    public List<Vector2Int> GetAvaliableMoves(ref GameObject[,] positions)
    {   
        switch (this.pieceName)
        {
            case "blackPawn":
            case "whitePawn":
                return PawnMoves(ref positions);
            case "blackQueen":
            case "whiteQueen": 
                List<Vector2Int> moves = ChessPieceMoves("axis", ref positions, true);
                moves.AddRange(ChessPieceMoves("cross", ref positions, true));
                return moves;
            case "whiteKnight":
            case "blackKnight":
                return ChessPieceMoves("L", ref positions);
            case "blackKing":
            case "whiteKing":
                return ChessPieceMoves("surround", ref positions);
            case "blackBishop":
            case "whiteBishop":
                return ChessPieceMoves("axis", ref positions, true);
            case "blackTower":
            case "whiteTower": 
                return ChessPieceMoves("cross", ref positions, true);
            default:
                return new List<Vector2Int>();
        }
    }

    public bool ContainsValidMove(ref List<Vector2Int> simMoves, Vector2Int position)
    {
        foreach (Vector2Int move in simMoves)
            if(move.x == position.x && move.y == position.y) return true;
        return false;
    }
}