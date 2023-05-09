using UnityEngine;
using NUnit.Framework;

public class GameTests
{
    private Game game;

    [SetUp]
    public void Setup()
    {   
        GameObject chessPiecePrefab = new GameObject();
        chessPiecePrefab.AddComponent<Chessman>();
        chessPiecePrefab.AddComponent<SpriteRenderer>();

        GameObject controller = new GameObject();
        controller.tag = "GameController";
        controller.AddComponent<Game>();
        Game game = controller.GetComponent<Game>();
        game.chesspiece = chessPiecePrefab;
    }

    [Test]
    public void PositionOnBoard_PositionWithinBounds_ReturnsTrue()
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        bool result = game.PositionOnBoard(4, 6);

        Assert.IsTrue(result);
    }

    [Test]
    public void PositionOnBoard_PositionOutOfBounds_ReturnsFalse()
    {
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
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

        // Act
        // Assign the chess piece prefab to the game object
        Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        GameObject chessPieceInstance = game.Create(pieceName, x, y);
        Chessman chessman = chessPieceInstance.GetComponent<Chessman>();

        // Assert
        Assert.AreEqual(pieceName, chessman.name);
        Assert.AreEqual(x, chessman.GetXBoard());
        Assert.AreEqual(y, chessman.GetYBoard());
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

}
