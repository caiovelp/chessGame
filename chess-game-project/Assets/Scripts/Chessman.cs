using System.Collections.Generic;
using UnityEngine;

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
    public Sprite[] whiteQueen, whiteKing, whiteBishop, whiteTower, whiteKnight, whitePawn;
    public Sprite[] blackQueen, blackKing, blackBishop, blackTower, blackKnight, blackPawn;
    private int setID;
    private bool move = false;
    

    //Função que seleciona a skin das peças.
    public void SelectPeca(bool isForward)
    {
        if (isForward)
        {
            if (setID == whitePawn.Length - 1)
            {
                setID = 0;
            }
            else
            {
                setID++;
            }
        }
        else
        {
            if (setID == 0)
            {
                setID = whitePawn.Length - 1;
            }
            else
            {
                setID--;
            }
        }
        PlayerPrefs.SetInt("set", setID);

        switch (this.name)
        {
            case "SptPecaBranca": this.GetComponent<SpriteRenderer>().sprite = whiteKing[setID]; Debug.Log(this.name); break;
            case "SptPecaPreta": this.GetComponent<SpriteRenderer>().sprite = blackKing[setID]; Debug.Log(this.name); break;
        }
    }

    /* 
        Função responsável por "ativar" as peças, ou seja, ela define as posições das peças,
        ativa o controlador e define o player que está interagindo com aquela peça.
    */
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        setID = PlayerPrefs.GetInt("set", 0);

        // Ajusta as posições das peças
        SetCoordinates();

        switch (this.name)
        {
            case "whiteQueen": this.GetComponent<SpriteRenderer>().sprite = whiteQueen[setID]; player = "white"; break;
            case "whiteKing": this.GetComponent<SpriteRenderer>().sprite = whiteKing[setID]; player = "white"; break;
            case "whiteBishop": this.GetComponent<SpriteRenderer>().sprite = whiteBishop[setID]; player = "white"; break;
            case "whiteTower": this.GetComponent<SpriteRenderer>().sprite = whiteTower[setID]; player = "white"; break;
            case "whiteKnight": this.GetComponent<SpriteRenderer>().sprite = whiteKnight[setID]; player = "white"; break;
            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn[setID]; player = "white"; break;

            case "blackQueen": this.GetComponent<SpriteRenderer>().sprite = blackQueen[setID]; player = "black"; break;
            case "blackKing": this.GetComponent<SpriteRenderer>().sprite = blackKing[setID]; player = "black"; break;
            case "blackBishop": this.GetComponent<SpriteRenderer>().sprite = blackBishop[setID]; player = "black"; break;
            case "blackTower": this.GetComponent<SpriteRenderer>().sprite = blackTower[setID]; player = "black"; break;
            case "blackKnight": this.GetComponent<SpriteRenderer>().sprite = blackKnight[setID]; player = "black"; break;
            case "blackPawn": this.GetComponent<SpriteRenderer>().sprite = blackPawn[setID]; player = "black"; break;
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
        y -= 2.72f;

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

    public string GetPlayer()
    {
        return this.player;
    }

    public string GetName()
    {
        return this.name;
    }

    public Sprite GetWhiteQueen()
    {
        return whiteQueen[setID];
    }

    public Sprite GetWhiteTower()
    {
        return whiteTower[setID];
    }

    public Sprite GetWhiteBishop()
    {
        return whiteBishop[setID];
    }

    public Sprite GetWhiteKnight()
    {
        return whiteKnight[setID];
    }

    public Sprite GetBlackQueen()
    {
        return blackQueen[setID];
    }

    public Sprite GetBlackTower()
    {
        return blackTower[setID];
    }

    public Sprite GetBlackBishop()
    {
        return blackBishop[setID];
    }

    public Sprite GetBlackKnight()
    {
        return blackKnight[setID];
    }

    public bool GetMove()
    {
        return move;
    }
    
    public void SetMove(bool m = false)
    {
        this.move = m;
    }

    /*
        Função do Unity que é chamada quando o usuário clica e solta o botão do mouse.
        Nesse caso, essa OnMouseUp é responsável pelo desenho dos moveplates.
    */
    private void OnMouseUp()
    {
        if(controller.GetComponent<Game>().GetCurrentPlayer() == "white" &&  controller.GetComponent<Game>().IsWhiteIa())
        {
            AIMove();
            return;
        }

        if(controller.GetComponent<Game>().GetCurrentPlayer() == "black" &&  controller.GetComponent<Game>().IsBlackIa())
        {
            AIMove();
            return;
        }

        // Só habilita a jogada se o for a vez do jogador da peça selecionada e se o player não for IA
        
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player && !controller.GetComponent<Game>().IsBlackIa())
        {
            // Apaga os moveplates que estão no tabuleiro.
            DestroyMovePlates();

            // Inicia os novos moveplates depedendo da interação.
            InitiateMovePlates();
        }
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player && !controller.GetComponent<Game>().IsWhiteIa())
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

    /*
        Função que cria as opções em movimentação em X de acordo com o tabuleiro
    */
    public void CrossMovePlate()
    {
        LineMovePlate(1,1);
        LineMovePlate(1,-1);
        LineMovePlate(-1,1);
        LineMovePlate(-1,-1);
    }

    /*
        Função que cria as opções em movimentação em + de acordo com os eixos do tabuleiro
    */
    public void AxisMovePlate()
    {
        LineMovePlate(1,0);
        LineMovePlate(-1,0);
        LineMovePlate(0,1);
        LineMovePlate(0,-1);
    }

    /*
        Função que cria uma linha de movimentação a partir da peça em questão de acordo com um vetor (x,y)
        @param xIncrement - valor x do vetor
        @param yIncrement - valor y do vetor
    */
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
            MovePlateSpawn(x,y, attack: true);
        }
    }

    // Desenha os moveplate do peão.
    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        bool promote = false;
        
        if (sc.PositionOnBoard(x, y))
        {
            // Se for a posição inicial do peão, spawnar dois movePlate
            if (sc.GetCurrentPlayer() == "white" && y == 2)
            {
                if (sc.GetPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                    MovePlateSpawn(x, y + 1);
                }
            }
            else if (sc.GetCurrentPlayer() == "black" && y == 5)
            {
                if (sc.GetPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                    MovePlateSpawn(x, y - 1);
                }
            }
            else if (sc.GetCurrentPlayer() == "white")
            {
                if (sc.GetPosition(x, y) == null)
                {
                    if (y == 7)
                        promote = true;
                    MovePlateSpawn(x, y, promote: promote);
                }
            }
            else if (sc.GetCurrentPlayer() == "black")
            {
                if (sc.GetPosition(x, y) == null)
                {
                    if (y == 0)
                        promote = true;
                    MovePlateSpawn(x, y, promote: promote);
                }
            }
    
            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                if (y == 7 && sc.GetCurrentPlayer() == "white")
                    promote = true;
                else if (y == 0 && sc.GetCurrentPlayer() == "black")
                    promote = true;
                
                MovePlateSpawn(x + 1, y, attack: true, promote: promote);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                if (y == 7 && sc.GetCurrentPlayer() == "white")
                    promote = true;
                else if (y == 0 && sc.GetCurrentPlayer() == "black")
                    promote = true;
                
                MovePlateSpawn(x - 1, y, attack: true, promote: promote);
            }
        }
    }
    
    /*
        Função que cria as opções de movimentação somente em volta da peça
    */
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
        RoqueMovePlate();
    }

    //Essa função verifica se é possível fazer roque e spawna o moveplate
    private void RoqueMovePlate()
    {
        string currentPlayer = controller.GetComponent<Game>().GetCurrentPlayer();
        if (!move)
        {
            if(currentPlayer == "white")
            {
                if (controller.GetComponent<Game>().GetPosition(0, 0) != null && !controller.GetComponent<Game>().GetPosition(0, 0).GetComponent<Chessman>().GetMove() && VerificaRoque(0))
                {
                    MovePlateSpawn(0, 0, roque: true);
                }

                if (controller.GetComponent<Game>().GetPosition(7, 0) != null &&!controller.GetComponent<Game>().GetPosition(7, 0).GetComponent<Chessman>().GetMove() && VerificaRoque(7))
                {
                    MovePlateSpawn(7, 0, roque: true);
                }
            }
            else
            {
                if (controller.GetComponent<Game>().GetPosition(0, 7) != null && !controller.GetComponent<Game>().GetPosition(0, 7).GetComponent<Chessman>().GetMove() && VerificaRoque(0) )
                {
                    MovePlateSpawn(0, 7, roque: true);
                }

                if (controller.GetComponent<Game>().GetPosition(7, 7) != null &&!controller.GetComponent<Game>().GetPosition(7, 7).GetComponent<Chessman>().GetMove() && VerificaRoque(7))
                {
                    MovePlateSpawn(7, 7, roque: true);
                }
            }
        }
    }

    public bool VerificaRoque(int x)
    {
        if (xBoard > x)
        {
            for (int i = xBoard-1; i > x; i--)
                if (controller.GetComponent<Game>().GetPosition(i, yBoard) != null)
                    return false;
            return true;
        }

        for (int i = xBoard+1; i < x; i++)
            if (controller.GetComponent<Game>().GetPosition(i, yBoard) != null)
                return false;
        return true;
    }

    // Função resposável por ditar os movimento em "L"
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

    // Função resposável por invocar a MovePlate nas coordenadas informados
    public void PointMovePlate(int x, int y)
    {
        Game gameScript = controller.GetComponent<Game>();
        if (gameScript.PositionOnBoard(x, y))
        {
            GameObject chessPiece = gameScript.GetPosition(x, y);
            /*  
                Verifica se posição da jogada tem uma peça.
                Se sim, invoca a MovePlate na posição.
                Caso contrário, se o player da peça é diferente do atual invoca
                a MovePlate de ataque na posição.
            */ 
            if (chessPiece == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (chessPiece.GetComponent<Chessman>().player != player)
            {
                MovePlateSpawn(x, y, attack: true);
            }
        }
    }

    // Desenha os moveplates de acordo com uma matrix 8x8 caso seja uma ação de ataque xor promoção de peão
    //                          passe os respectivos atributos como true
    public void MovePlateSpawn(int matrixX, int matrixY, bool roque = false, bool attack = false, bool promote = false)
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
        mpScript.promote = promote;
        mpScript.attack = attack;
        mpScript.roque = roque;
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
        x *= 0.95f;
        y *= 0.95f;

        x -= 3.32f;
        y -= 2.99f;

        // Cria o gameobject do moveplate
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        // Cria uma instância do moveplate e interage com essa instância, flag attack = true.
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoordinates(matrixX, matrixY);
    }

    public void MovePlateIaSpawn(int matrixX, int matrixY, GameObject gc, bool attack)
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
        // Cria uma instância do moveplate e interage com essa instância, flag attack = true.
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = attack;
        mpScript.SetReference(gc);
        mpScript.SetCoordinates(matrixX, matrixY); ;
        mpScript.OnMouseUp();
    }

    private Piece[] SetWhitePieces(GameObject[] whitePieces)
    {
        Piece[] wps = new Piece[16];
        int i = 0;
        foreach (var whitePiece in whitePieces)
        {
            if (whitePiece == null) continue;
            
            Chessman gc = whitePiece.GetComponent<Chessman>();
            int pieceTeam = 0;
            int pieceType = 0;
            switch (gc.GetName())
            {
                case "whitePawn":
                    pieceType = 0;
                    break;
                case "whiteKnight":
                    pieceType = 1;
                    break;
                case "whiteBishop":
                    pieceType = 2;
                    break;
                case "whiteTower":
                    pieceType = 3;
                    break;
                case "whiteQueen":
                    pieceType = 4;
                    break;
                case "whiteKing":
                    pieceType = 5;
                    break;
            }
                
            wps[i] = new Piece(pieceType, pieceTeam, gc.GetXBoard(), gc.GetYBoard());
            i++;
        }
        

        return wps;
    }

    private Piece[] SetBlackPieces(GameObject[] blackPieces)
    {
        Piece[] bps = new Piece[16];
        int i = 0;
        foreach (var blackPiece in blackPieces)
        {
            if (blackPiece == null) continue;
            Chessman gc = blackPiece.GetComponent<Chessman>();
            int pieceTeam = 1;
            int pieceType = 0;
            switch (gc.GetName())
            {
                case "blackPawn":
                    pieceType = 0;
                    break;
                case "blackKnight":
                    pieceType = 1;
                    break;
                case "blackBishop":
                    pieceType = 2;
                    break;
                case "blackTower":
                    pieceType = 3;
                    break;
                case "blackQueen":
                    pieceType = 4;
                    break;
                case "blackKing":
                    pieceType = 5;
                    break;
            }
                
            bps[i] = new Piece(pieceType, pieceTeam, gc.GetXBoard(), gc.GetYBoard());
            i++;
        }
        

        return bps;
    }

    private Piece[,] SetPiecesPosition(GameObject[] whitePieces, GameObject[] blackPieces)
    {
        Piece[,] pieces = new Piece[8,8];
        foreach (var whitePiece in whitePieces)
        {
            if (whitePiece == null) continue;
            Chessman gc = whitePiece.GetComponent<Chessman>();
            int pieceTeam = 0;
            int pieceType = 0;
            switch (gc.GetName())
            {
                case "whitePawn":
                    pieceType = 0;
                    break;
                case "whiteKnight":
                    pieceType = 1;
                    break;
                case "whiteBishop":
                    pieceType = 2;
                    break;
                case "whiteTower":
                    pieceType = 3;
                    break;
                case "whiteQueen":
                    pieceType = 4;
                    break;
                case "whiteKing":
                    pieceType = 5;
                    break;
            }

            pieces[gc.GetXBoard(), gc.GetYBoard()] = new Piece(pieceType, pieceTeam, gc.GetXBoard(), gc.GetYBoard());
        }
        foreach (var blackPiece in blackPieces)
        {
            if (blackPiece == null) continue;
            Chessman gc = blackPiece.GetComponent<Chessman>();
            int pieceTeam = 1;
            int pieceType = 0;
            switch (gc.GetName())
            {
                case "blackPawn":
                    pieceType = 0;
                    break;
                case "blackKnight":
                    pieceType = 1;
                    break;
                case "blackBishop":
                    pieceType = 2;
                    break;
                case "blackTower":
                    pieceType = 3;
                    break;
                case "blackQueen":
                    pieceType = 4;
                    break;
                case "blackKing":
                    pieceType = 5;
                    break;
            }

            pieces[gc.GetXBoard(), gc.GetYBoard()] = new Piece(pieceType, pieceTeam, gc.GetXBoard(), gc.GetYBoard());
        }

        return pieces;
    }

    private Board SetBoard(Piece[,] pieces, Piece[] whitePieces, Piece[] blackPieces)
    {
        Board board = new Board();

        board.positions = pieces;
        board.wPieces = new List<Piece>(whitePieces);
        board.bPieces = new List<Piece>(blackPieces);
        return board;
    }

    public void AIMove()
    {
        GameObject[] whitePieces = controller.GetComponent<Game>().GetWhitePlayer();
        GameObject[] blackPieces = controller.GetComponent<Game>().GetBlackPlayer();
        
        Piece[,] pieces = SetPiecesPosition(whitePieces, blackPieces);
        Piece[] wPs = SetWhitePieces(whitePieces);
        Piece[] bPs = SetBlackPieces(blackPieces);
        Board board = SetBoard(pieces, wPs, bPs);
        int currentPlayer = 0;
        
        if (controller.GetComponent<Game>().GetCurrentPlayer() == "white")
            currentPlayer = 0;
        else
            currentPlayer = 1;
        
        Move move = AI.BestChoice(board, currentPlayer, 2);

        int xAtual = move.x;
        int yAtual = move.y;
        int xDest = move.destX;
        int yDest = move.destY;
        bool attack = move.attack;

        GameObject gc = controller.GetComponent<Game>().GetPosition(xAtual, yAtual);
        MovePlateIaSpawn(xDest, yDest, gc, attack);
    }

    public void CallOnMouseUp()
    {
        OnMouseUp();
    }
}