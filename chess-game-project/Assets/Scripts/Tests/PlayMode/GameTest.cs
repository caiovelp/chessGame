using UnityEngine;
using NUnit.Framework;

public class GameTests
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        game = new Game();
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
    public void Create_PieceInstantiatedWithCorrectProperties()
    {
        // Arrange
        string pieceName = "whitePawn";
        int x = 3;
        int y = 2;

        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Act
        game.chesspiece = gameObject; // Assign the chess piece prefab to the game object
        GameObject piece = game.Create(pieceName, x, y);
        Chessman chessman = piece.GetComponent<Chessman>();

        game.SetPosition(piece);

        // Assert
        Assert.AreEqual(piece, game.GetPosition(x, y));
        Assert.AreEqual(pieceName, chessman.name);
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
    public void ShouldInitializeAllPìeces_Test() {
        // Create a chess piece prefab as a placeholder for testing
        var gameObject = new GameObject();
        gameObject.AddComponent<Chessman>();
        gameObject.AddComponent<SpriteRenderer>();

        // Act
        game.chesspiece = gameObject; 
        game.Start();

        Assert.AreEqual("whitePawn", game.GetPosition(0,1).name);
        Assert.AreEqual("whitePawn", game.GetPosition(1,1).name);
        Assert.AreEqual("whitePawn", game.GetPosition(2,1).name);
        Assert.AreEqual("whitePawn", game.GetPosition(3,1).name);
        Assert.AreEqual("whitePawn", game.GetPosition(4,1).name);
        Assert.AreEqual("whitePawn", game.GetPosition(5,1).name);
        Assert.AreEqual("whitePawn", game.GetPosition(6,1).name);
        Assert.AreEqual("whitePawn", game.GetPosition(7,1).name);
        Assert.AreEqual("whiteTower", game.GetPosition(0,0).name);
        Assert.AreEqual("whiteTower", game.GetPosition(7,0).name);
        Assert.AreEqual("whiteBishop", game.GetPosition(2,0).name);
        Assert.AreEqual("whiteBishop", game.GetPosition(5,0).name);
        Assert.AreEqual("whiteQueen", game.GetPosition(3,0).name);
        Assert.AreEqual("whiteKing", game.GetPosition(4,0).name);

        Assert.AreEqual("blackPawn", game.GetPosition(0,6).name);
        Assert.AreEqual("blackPawn", game.GetPosition(1,6).name);
        Assert.AreEqual("blackPawn", game.GetPosition(2,6).name);
        Assert.AreEqual("blackPawn", game.GetPosition(3,6).name);
        Assert.AreEqual("blackPawn", game.GetPosition(4,6).name);
        Assert.AreEqual("blackPawn", game.GetPosition(5,6).name);
        Assert.AreEqual("blackPawn", game.GetPosition(6,6).name);
        Assert.AreEqual("blackPawn", game.GetPosition(7,6).name);
        Assert.AreEqual("blackTower", game.GetPosition(0,7).name);
        Assert.AreEqual("blackTower", game.GetPosition(7,7).name);
        Assert.AreEqual("blackBishop", game.GetPosition(2,7).name);
        Assert.AreEqual("blackBishop", game.GetPosition(5,7).name);
        Assert.AreEqual("blackQueen", game.GetPosition(3,7).name);
        Assert.AreEqual("blackKing", game.GetPosition(4,7).name);

        
    }

    [Test]
    public void ShoudStartGame_Test() {
        Assert.AreEqual(false, game.IsGameOver());
    }

}
