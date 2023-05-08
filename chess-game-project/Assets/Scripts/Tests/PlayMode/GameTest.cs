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

        // Assert
        Assert.AreEqual(pieceName, chessman.name);
        Assert.AreEqual(x, chessman.GetXBoard());
        Assert.AreEqual(y, chessman.GetYBoard());
    }


}
