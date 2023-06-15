using UnityEngine;
using NUnit.Framework;

public class MovementTest
{
    private Game game;

    [SetUp]
    public void Setup()
    {   
        GameObject chessPiecePrefab = new GameObject();
        chessPiecePrefab.AddComponent<Chessman>();
        chessPiecePrefab.AddComponent<SpriteRenderer>();

        var chessman = chessPiecePrefab.GetComponent<Chessman>();

        var spriteBlackKnight = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/CavaloPreto-PLastico.png");
        Sprite[] mockBlackKnight = new[] { spriteBlackKnight, spriteBlackKnight, spriteBlackKnight, spriteBlackKnight };
        chessman.blackKnight = mockBlackKnight;

        var spriteBlackKing = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/ReiPreto-PLastico.png");
        Sprite[] mockBlackKing= new[] { spriteBlackKing, spriteBlackKing, spriteBlackKing, spriteBlackKing };
        chessman.blackKing = mockBlackKing;

        var spriteBlackQueen = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/RainhaPreto-PLastico.png");
        Sprite[] mockBlackQueen = new[] { spriteBlackQueen, spriteBlackQueen, spriteBlackQueen, spriteBlackQueen };
        chessman.blackQueen = mockBlackQueen;

        var spriteBlackTower = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/TorrePreto-PLastico.png");
        Sprite[] mockBlackTower = new[] { spriteBlackTower, spriteBlackTower, spriteBlackTower, spriteBlackTower };
        chessman.blackTower = mockBlackTower;

        var spriteBlackBishop = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/BispoPreto-PLastico.png");
        Sprite[] mockBlackBishop = new[] { spriteBlackBishop, spriteBlackBishop, spriteBlackBishop, spriteBlackBishop };
        chessman.blackBishop = mockBlackBishop;

        var spriteBlackPawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/PeaoPreto-PLastico.png");
        Sprite[] mockBlackPawn = new[] { spriteBlackPawn, spriteBlackPawn, spriteBlackPawn, spriteBlackPawn };
        chessman.blackPawn = mockBlackPawn;

        var spriteWhiteKnight = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/CavaloWhite-PLastico.png");
        Sprite[] mockWhiteKnight = new[] { spriteWhiteKnight, spriteWhiteKnight, spriteWhiteKnight, spriteWhiteKnight };
        chessman.whiteKnight = mockWhiteKnight;

        var spriteWhiteKing = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/ReiWhite-PLastico.png");
        Sprite[] mockWhiteKing= new[] { spriteWhiteKing, spriteWhiteKing, spriteWhiteKing, spriteWhiteKing };
        chessman.whiteKing = mockWhiteKing;

        var spriteWhiteQueen = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/RainhaWhite-PLastico.png");
        Sprite[] mockWhiteQueen = new[] { spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen };
        chessman.whiteQueen = mockWhiteQueen;

        var spriteWhiteTower = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/TorreWhite-PLastico.png");
        Sprite[] mockWhiteTower = new[] { spriteWhiteTower, spriteWhiteTower, spriteWhiteTower, spriteWhiteTower };
        chessman.whiteTower = mockWhiteTower;

        var spriteWhiteBishop = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/BispoWhite-PLastico.png");
        Sprite[] mockWhiteBishop = new[] { spriteWhiteBishop, spriteWhiteBishop, spriteWhiteBishop, spriteWhiteBishop };
        chessman.whiteBishop = mockWhiteBishop;

        var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
        chessman.whitePawn = mockWhitePawn;

        
        GameObject controller = new GameObject();
        controller.tag = "GameController";
        controller.AddComponent<Game>();
        Game game = controller.GetComponent<Game>();
        game.chesspiece = chessPiecePrefab;
        
    }

    [Test]
    public void Move_KnightToSelectedPosition() //NOTE 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("blackKnight", 3, 3);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        float x = 1 * 1.15f - 4f;
        float y = 4 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(1, 4);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(3, 3));
        Assert.AreSame(game.GetPosition(1, 4), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(1, 4));
        
        Assert.AreEqual(chessman.GetXBoard(), 1);
        Assert.AreEqual(chessman.GetYBoard(), 4);
    }

    [Test]
    public void Move_PawnToSelectedPosition() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whitePawn", 0, 1);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        float x = 0 * 1.15f - 4f;
        float y = 2 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(0, 2);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(0, 1));
        Assert.AreSame(game.GetPosition(0, 2), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(0, 2));
        
        Assert.AreEqual(chessman.GetXBoard(), 0);
        Assert.AreEqual(chessman.GetYBoard(), 2);
    }

    [Test]
    public void Move_TowerToSelectedPosition() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whiteTower", 0, 0);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        float x = 0 * 1.15f - 4f;
        float y = 1 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(0, 1);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(0, 0));
        Assert.AreSame(game.GetPosition(0, 1), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(0, 1));
        
        Assert.AreEqual(chessman.GetXBoard(), 0);
        Assert.AreEqual(chessman.GetYBoard(), 1);
    }

    [Test]
    public void Move_BishopToSelectedPosition() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whiteBishop", 2, 0);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        float x = 3 * 1.15f - 4f;
        float y = 1 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(3, 1);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(0, 2));
        Assert.AreSame(game.GetPosition(3, 1), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(3, 1));
        
        Assert.AreEqual(chessman.GetXBoard(), 3);
        Assert.AreEqual(chessman.GetYBoard(), 1);
    }

    [Test]
    public void Move_QueenKingToSelectedPositionUP() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whiteQueen", 3, 0);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        // Cima
        float x = 3 * 1.15f - 4f;
        float y = 1 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(3, 1);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(3, 0));
        Assert.AreSame(game.GetPosition(3, 1), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(3, 1));
        
        Assert.AreEqual(chessman.GetXBoard(), 3);
        Assert.AreEqual(chessman.GetYBoard(), 1);
    }

    [Test]
    public void Move_QueenKingToSelectedPositionDown() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whiteQueen", 3, 1);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        // Cima
        float x = 3 * 1.15f - 4f;
        float y = 0 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(3, 0);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(3, 1));
        Assert.AreSame(game.GetPosition(3, 0), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(3, 0));
        
        Assert.AreEqual(chessman.GetXBoard(), 3);
        Assert.AreEqual(chessman.GetYBoard(), 0);
    }

    [Test]
    public void Move_QueenKingToSelectedPositionRight() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whiteQueen", 3, 1);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        // Cima
        float x = 4 * 1.15f - 4f;
        float y = 1 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(4, 1);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(3, 1));
        Assert.AreSame(game.GetPosition(4, 1), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(4, 1));
        
        Assert.AreEqual(chessman.GetXBoard(), 4);
        Assert.AreEqual(chessman.GetYBoard(), 1);
    }

    [Test]
    public void Move_QueenKingToSelectedPositionLeft() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whiteQueen", 3, 1);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        // Cima
        float x = 2 * 1.15f - 4f;
        float y = 1 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(2, 1);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(3, 1));
        Assert.AreSame(game.GetPosition(2, 1), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(2, 1));
        
        Assert.AreEqual(chessman.GetXBoard(), 2);
        Assert.AreEqual(chessman.GetYBoard(), 1);
    }

    [Test]
    public void Move_QueenKingToSelectedPositionAxis() 
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create("whiteQueen", 3, 1);
        game.SetPosition(chessPieceInstance);

        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        GameObject movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.AddComponent<SpriteRenderer>();
        chessman.movePlate = movePlatePrefab;

        // Cima
        float x = 4 * 1.15f - 4f;
        float y = 2 * 1.15f - 3.6f;

        GameObject movePlateInstance = Chessman.Instantiate(chessman.movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate movePlate = movePlateInstance.GetComponent<MovePlate>();
        movePlate.SetReference(chessPieceInstance);
        movePlate.SetCoordinates(4, 2);
        movePlate.attack = false;
        
        movePlate.OnMouseUp();

        Assert.IsNull(game.GetPosition(3, 1));
        Assert.AreSame(game.GetPosition(4, 2), chessPieceInstance);
        Assert.IsInstanceOf(typeof(GameObject), game.GetPosition(4, 2));
        
        Assert.AreEqual(chessman.GetXBoard(), 4);
        Assert.AreEqual(chessman.GetYBoard(), 2);
    }

}
