using UnityEngine;
using NUnit.Framework;
using System;
using System.Collections;

public class ChessmanTests
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        game = new Game();
    }

    [Test, Order(1)]
    public void ShouldInitializeMovePlate_WhitePawn()
    {   
        // Arrange
        string pieceName = "whitePawn";
        int x = 0;
        int y = 1;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
        chessmanMaster.whitePawn = mockWhitePawn;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        chessman.InitiateMovePlates();

        // Assert
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        // Desconsidera o primeiro movePlate pois não é um dos spawnados, é o básico que é clonado.
        Assert.AreEqual(movePlates.Length - 1, 2);
    }

    [Test, Order(2)]
    public void ShouldInitializeMovePlate_BlackPawn()
    {   
        // Arrange
        string pieceName = "blackPawn";
        int x = 0;
        int y = 6;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackPawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/PeaoPreto-PLastico.png");
        Sprite[] mockBlackPawn = new[] { spriteBlackPawn, spriteBlackPawn, spriteBlackPawn, spriteBlackPawn };
        chessmanMaster.blackPawn = mockBlackPawn;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        gameController.NextTurn();

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 2);
    }

    [Test, Order(3)]
    public void ShouldInitializeMovePlate_WhiteBishop()
    {   
        // Arrange
        string pieceName = "whiteBishop";
        int x = 2;
        int y = 0;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhiteBishop = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/BispoWhite-PLastico.png");
        Sprite[] mockWhiteBishop = new[] { spriteWhiteBishop, spriteWhiteBishop, spriteWhiteBishop, spriteWhiteBishop };
        chessmanMaster.whiteBishop = mockWhiteBishop;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        gameController.NextTurn();

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 7);
    }

    [Test, Order(4)]
    public void ShouldInitializeMovePlate_WhiteTower()
    {   
        // Arrange
        string pieceName = "whiteTower";
        int x = 0;
        int y = 0;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhiteTower = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/TorreWhite-PLastico.png");
        Sprite[] mockWhiteTower = new[] { spriteWhiteTower, spriteWhiteTower, spriteWhiteTower, spriteWhiteTower };
        chessmanMaster.whiteTower = mockWhiteTower;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 14);
    }

    [Test, Order(5)]
    public void ShouldInitializeMovePlate_WhiteKnight()
    {   
        // Arrange
        string pieceName = "whiteKnight";
        int x = 1;
        int y = 0;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhiteKnight = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/CavaloWhite-PLastico.png");
        Sprite[] mockWhiteKnight = new[] { spriteWhiteKnight, spriteWhiteKnight, spriteWhiteKnight, spriteWhiteKnight };
        chessmanMaster.whiteKnight = mockWhiteKnight;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 3);
    }

    [Test, Order(6)]
    public void ShouldInitializeMovePlate_WhiteQueen()
    {   
        // Arrange
        string pieceName = "whiteQueen";
        int x = 3;
        int y = 0;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhiteQueen = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/RainhaWhite-PLastico.png");
        Sprite[] mockWhiteQueen = new[] { spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen };
        chessmanMaster.whiteQueen = mockWhiteQueen;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 21);
    }

    [Test, Order(7)]
    public void ShouldInitializeMovePlate_WhiteKing()
    {   
        // Arrange
        string pieceName = "whiteKing";
        int x = 4;
        int y = 0;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhiteKing = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/ReiWhite-PLastico.png");
        Sprite[] mockWhiteKing= new[] { spriteWhiteKing, spriteWhiteKing, spriteWhiteKing, spriteWhiteKing };
        chessmanMaster.whiteKing = mockWhiteKing;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        gameController.NextTurn();

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 5);
    }

    [Test, Order(8)]
    public void ShouldInitializeMovePlate_BlackBishop()
    {   
        // Arrange
        string pieceName = "blackBishop";
        int x = 2;
        int y = 7;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackBishop = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/BispoPreto-PLastico.png");
        Sprite[] mockBlackBishop = new[] { spriteBlackBishop, spriteBlackBishop, spriteBlackBishop, spriteBlackBishop };
        chessmanMaster.blackBishop = mockBlackBishop;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        gameController.NextTurn();

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 7);
    }

    [Test, Order(9)]
    public void ShouldInitializeMovePlate_BlackTower()
    {   
        // Arrange
        string pieceName = "blackTower";
        int x = 0;
        int y = 7;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackTower = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/TorrePreto-PLastico.png");
        Sprite[] mockBlackTower = new[] { spriteBlackTower, spriteBlackTower, spriteBlackTower, spriteBlackTower };
        chessmanMaster.blackTower = mockBlackTower;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 14);
    }

    [Test, Order(10)]
    public void ShouldInitializeMovePlate_BlackKnight()
    {   
        // Arrange
        string pieceName = "blackKnight";
        int x = 1;
        int y = 7;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackKnight = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/CavaloPreto-PLastico.png");
        Sprite[] mockBlackKnight = new[] { spriteBlackKnight, spriteBlackKnight, spriteBlackKnight, spriteBlackKnight };
        chessmanMaster.blackKnight = mockBlackKnight;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 3);
    }

    [Test, Order(11)]
    public void ShouldInitializeMovePlate_BlackQueen()
    {   
        // Arrange
        string pieceName = "blackQueen";
        int x = 3;
        int y = 7;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackQueen = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/RainhaPreto-PLastico.png");
        Sprite[] mockBlackQueen = new[] { spriteBlackQueen, spriteBlackQueen, spriteBlackQueen, spriteBlackQueen };
        chessmanMaster.blackQueen = mockBlackQueen;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 21);
    }

    [Test, Order(12)]
    public void ShouldInitializeMovePlate_BlackKing()
    {   
        // Arrange
        string pieceName = "blackKing";
        int x = 4;
        int y = 7;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackKing = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/ReiPreto-PLastico.png");
        Sprite[] mockBlackKing= new[] { spriteBlackKing, spriteBlackKing, spriteBlackKing, spriteBlackKing };
        chessmanMaster.blackKing = mockBlackKing;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 5);

        // Destroy
        chessman.DestroyMovePlates();
    }

    [Test, Order(13)]
    public void ShouldInitializeAttackMovePlate_Right_Pawn()
    {   
        // Arrange Black Pawn
        string pieceNameBlack = "blackPawn";
        int xBlack = 1;
        int yBlack = 2;

        // Arrange White Pawn
        string pieceNameWhite = "whitePawn";
        int xWhite = 0;
        int yWhite = 1;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackPawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockBlackPawn = new[] { spriteBlackPawn, spriteBlackPawn, spriteBlackPawn, spriteBlackPawn };
        chessmanMaster.blackPawn = mockBlackPawn;
        var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
        chessmanMaster.whitePawn = mockWhitePawn;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject pieceBlack = game.Create(pieceNameBlack, xBlack, yBlack);
        GameObject pieceWhite = game.Create(pieceNameWhite, xWhite, yWhite);
        Chessman chessman = pieceWhite.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(pieceBlack);
        gameController.SetPosition(pieceWhite);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 3);

        // Destroy
        chessman.DestroyMovePlates();
    }

    [Test, Order(14)]
    public void ShouldInitializeOneMovePlate_Pawn()
    {   
        // Arrange 
        string pieceName = "whitePawn";
        int x = 3;
        int y = 3;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
        chessmanMaster.whitePawn = mockWhitePawn;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 1);

        // Destroy
        chessman.DestroyMovePlates();
    }

    [Test, Order(15)]
    public void ShouldCallOnMouseUp()
    {
        // Arrange 
        string pieceName = "whitePawn";
        int x = 3;
        int y = 3;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
        chessmanMaster.whitePawn = mockWhitePawn;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(piece);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.CallOnMouseUp();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 1);
    }

    [Test, Order(16)]
    public void ShouldInitializeAttackMovePlate_Left_Pawn()
    {   
        // Arrange Black Pawn
        string pieceNameBlack = "blackPawn";
        int xBlack = 0;
        int yBlack = 2;

        // Arrange White Pawn
        string pieceNameWhite = "whitePawn";
        int xWhite = 1;
        int yWhite = 1;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackPawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockBlackPawn = new[] { spriteBlackPawn, spriteBlackPawn, spriteBlackPawn, spriteBlackPawn };
        chessmanMaster.blackPawn = mockBlackPawn;
        var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
        chessmanMaster.whitePawn = mockWhitePawn;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject pieceBlack = game.Create(pieceNameBlack, xBlack, yBlack);
        GameObject pieceWhite = game.Create(pieceNameWhite, xWhite, yWhite);
        Chessman chessman = pieceWhite.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(pieceBlack);
        gameController.SetPosition(pieceWhite);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 3);

        // Destroy
        chessman.DestroyMovePlates();
    }

    [Test, Order(17)]
    public void ShouldInitializeAttackMovePlate_Queen_Up()
    {   
        // Arrange Black Pawn
        string pieceNameBlack = "blackPawn";
        int xBlack = 3;
        int yBlack = 1;

        // Arrange White Pawn
        string pieceNameWhite = "whiteQueen";
        int xWhite = 3;
        int yWhite = 0;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackPawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/PeaoPreto-PLastico.png");
        Sprite[] mockBlackPawn = new[] { spriteBlackPawn, spriteBlackPawn, spriteBlackPawn, spriteBlackPawn };
        chessmanMaster.blackPawn = mockBlackPawn;
        var spriteWhiteQueen = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/RainhaWhite-PLastico.png");
        Sprite[] mockWhiteQueen = new[] { spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen };
        chessmanMaster.whiteQueen = mockWhiteQueen;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject pieceBlack = game.Create(pieceNameBlack, xBlack, yBlack);
        GameObject pieceWhite = game.Create(pieceNameWhite, xWhite, yWhite);
        Chessman chessman = pieceWhite.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(pieceBlack);
        gameController.SetPosition(pieceWhite);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 15);

        // Destroy
        chessman.DestroyMovePlates();
    }

    [Test, Order(18)]
    public void ShouldInitializeAttackMovePlate_King_Side()
    {   
        // Arrange Black Pawn
        string pieceNameBlack = "blackPawn";
        int xBlack = 5;
        int yBlack = 0;

        // Arrange White Pawn
        string pieceNameWhite = "whiteKing";
        int xWhite = 4;
        int yWhite = 0;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteBlackPawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/PeaoPreto-PLastico.png");
        Sprite[] mockBlackPawn = new[] { spriteBlackPawn, spriteBlackPawn, spriteBlackPawn, spriteBlackPawn };
        chessmanMaster.blackPawn = mockBlackPawn;

        var spriteWhiteKing = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/ReiWhite-PLastico.png");
        Sprite[] mockWhiteKing= new[] { spriteWhiteKing, spriteWhiteKing, spriteWhiteKing, spriteWhiteKing };
        chessmanMaster.whiteKing = mockWhiteKing;

        // Create a move plate prefab
        var movePlatePrefab = new GameObject();
        movePlatePrefab.AddComponent<MovePlate>();
        movePlatePrefab.tag = "MovePlate";

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject pieceBlack = game.Create(pieceNameBlack, xBlack, yBlack);
        GameObject pieceWhite = game.Create(pieceNameWhite, xWhite, yWhite);
        Chessman chessman = pieceWhite.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        // Assign the move plate prefab to the movePlate variable in the game object
        chessman.movePlate = movePlatePrefab;

        gameController.SetPosition(pieceBlack);
        gameController.SetPosition(pieceWhite);

        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        var movePlatesBefore = new ArrayList(movePlates).Count;

        chessman.InitiateMovePlates();

        // Assert
        movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        var movePlatesAfter = new ArrayList(movePlates).Count;

        var movePlatesSpawned = movePlatesAfter - movePlatesBefore;
        Assert.AreEqual(movePlatesSpawned, 5);

        // Destroy
        chessman.DestroyMovePlates();
    }
}
