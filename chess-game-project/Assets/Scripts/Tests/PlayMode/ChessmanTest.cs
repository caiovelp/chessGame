using UnityEngine;
using NUnit.Framework;

public class ChessmanTests
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        game = new Game();
    }

    [Test]
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

        game.SetPosition(piece);

        chessman.InitiateMovePlates();

        // Assert
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        Assert.AreEqual(movePlates.Length, 2);
    }

    [Test]
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

        game.SetPosition(piece);

        chessman.InitiateMovePlates();

        // Assert
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        Assert.AreEqual(movePlates.Length, 2);
    }
}
