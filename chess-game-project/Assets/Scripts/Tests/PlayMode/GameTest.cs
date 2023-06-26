using UnityEngine;
using NUnit.Framework;

public class GameTests
{
    private Game game;

    public Chessman GetChessman(GameObject gameObject)
    {
        return gameObject.GetComponent<Chessman>();
    }

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<Game>();
        game = gameObject.GetComponent<Game>();
    }

    [Test]
    public void PositionOnBoard_PositionWithinBounds_ReturnsTrue()
    {
        bool result = game.PositionOnBoard(4, 6);

        Assert.IsTrue(result);
    }

    [Test]
    public void PositionOnBoard_PositionOutOfBounds_ReturnsFalse()
    {
        bool result = game.PositionOnBoard(10, 3);

        Assert.IsFalse(result);
    }

    [Test]
    public void Create_PieceInstantiatedWithCorrectProperties() // REVIEW
    {
        // Arrange
        string pieceName = "whitePawn";
        int x = 3;
        int y = 2;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Mock Sprites
        var chessmanMaster = gameObject.GetComponent<Chessman>();
        var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
        Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
        chessmanMaster.whitePawn = mockWhitePawn;

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = GetChessman(piece);

        game.SetPosition(piece);

        // Assert
        Assert.AreEqual(piece, game.GetPosition(x, y));
        Assert.AreEqual(pieceName, chessman.GetName()); // REVIEW
        Assert.AreEqual(x, chessman.GetXBoard());
        Assert.AreEqual(y, chessman.GetYBoard());

        //SetPositionEmpty
        game.SetPositionEmpty(x,y);
        Assert.AreEqual(null, game.GetPosition(x, y));
    }

    [Test]
    public void PlayerTurns_Tests() {
        Assert.AreEqual("white", game.GetCurrentPlayer());
        game.NextTurn();
        Assert.AreEqual("black", game.GetCurrentPlayer());
        game.NextTurn();
        Assert.AreEqual("white", game.GetCurrentPlayer());
    }

    [Test]
    public void ShouldInitializeAllPìeces_Test() { // REVIEW
        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        var chessman = gameObject.GetComponent<Chessman>();

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

        // Act
        game.chesspiece = gameObject; 
        game.Start();

        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(0,1)).GetName());
        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(1,1)).GetName());
        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(2,1)).GetName());
        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(3,1)).GetName());
        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(4,1)).GetName());
        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(5,1)).GetName());
        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(6,1)).GetName());
        Assert.AreEqual("whitePawn", GetChessman(game.GetPosition(7,1)).GetName());
        Assert.AreEqual("whiteTower", GetChessman(game.GetPosition(0,0)).GetName());
        Assert.AreEqual("whiteTower", GetChessman(game.GetPosition(7,0)).GetName());
        Assert.AreEqual("whiteBishop", GetChessman(game.GetPosition(2,0)).GetName());
        Assert.AreEqual("whiteBishop", GetChessman(game.GetPosition(5,0)).GetName());
        Assert.AreEqual("whiteQueen", GetChessman(game.GetPosition(3,0)).GetName());
        Assert.AreEqual("whiteKing", GetChessman(game.GetPosition(4,0)).GetName());

        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(0,6)).GetName());
        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(1,6)).GetName());
        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(2,6)).GetName());
        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(3,6)).GetName());
        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(4,6)).GetName());
        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(5,6)).GetName());
        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(6,6)).GetName());
        Assert.AreEqual("blackPawn", GetChessman(game.GetPosition(7,6)).GetName());
        Assert.AreEqual("blackTower", GetChessman(game.GetPosition(0,7)).GetName());
        Assert.AreEqual("blackTower", GetChessman(game.GetPosition(7,7)).GetName());
        Assert.AreEqual("blackBishop", GetChessman(game.GetPosition(2,7)).GetName());
        Assert.AreEqual("blackBishop", GetChessman(game.GetPosition(5,7)).GetName());
        Assert.AreEqual("blackQueen", GetChessman(game.GetPosition(3,7)).GetName());
        Assert.AreEqual("blackKing", GetChessman(game.GetPosition(4,7)).GetName());
    }

    [Test]
    public void ShoudStartGame_Test() {
        Assert.AreEqual(false, game.IsGameOver());
    }

}