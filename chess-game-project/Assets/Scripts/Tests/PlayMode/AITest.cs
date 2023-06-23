using UnityEngine;
using NUnit.Framework;
using Mocks;

public class AITest
{
    private Game game;
    private Board boardBot;
    
    [SetUp]
    public void Setup()
    {
        game = new Game();
        boardBot = new Board();
    }
    
    [Test]
    public void AIGetPositions_Test()
    {
        GameObject[,] positions = game.GetPositions();

        Assert.IsNotNull(positions);
    }

    [Test]
    public void AIDetectPlayerWhite_Test()
    {
        GameObject[] player_mock = new GameObject[16];
        
        GameObject[] player = game.GetWhitePlayer();

        Assert.AreEqual(player, player_mock);
    }
    
    [Test]
    public void AIDetectPlayerBlack_Test()
    {
        GameObject[] player_mock = new GameObject[16];
        
        GameObject[] player = game.GetBlackPlayer();

        Assert.AreEqual(player, player_mock);
    }
    
    [Test]
    public void AIReturnWhiteIA_Test()
    {
        bool IA = game.IsWhiteIa();

        Assert.IsFalse(IA);
    }
    
    [Test]
    public void AIReturnBlackIA_Test()
    {
        bool IA = game.IsBlackIa();

        Assert.IsTrue(IA);
    }

    [Test]
    public void AIMoveBestChoice_Test()
    {
        
        MocksTest mocksTest = new MocksTest();
        mocksTest.game = game;
        
        var gameTeste = mocksTest.StartMock();

        gameTeste.Start();

        var chessman = gameTeste.chesspiece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        var gameComponentController = chessman.controller.GetComponent<Game>();
        gameComponentController.SetBlackPlayer(gameTeste.GetBlackPlayer());
        gameComponentController.SetWhitePlayer(gameTeste.GetWhitePlayer());

        chessman.AIMove();

        Assert.Pass();
    }

    [Test]
    public void AIMoveRandomChoice_Test()
    {
        
        MocksTest mocksTest = new MocksTest();
        mocksTest.game = game;
        
        var gameTeste = mocksTest.StartMock();

        gameTeste.Start();

        var chessman = gameTeste.chesspiece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        var gameComponentController = chessman.controller.GetComponent<Game>();
        gameComponentController.SetBlackPlayer(gameTeste.GetBlackPlayer());
        gameComponentController.SetWhitePlayer(gameTeste.GetWhitePlayer());

        gameComponentController.gameObject.name = "Random";

        chessman.AIMove();

        Assert.Pass();
    }

    [Test]
    public void AIAddPiece_RemovePiece_Test()
    {
        MocksTest mocksTest = new MocksTest();
        mocksTest.game = game;
        
        var gameTeste = mocksTest.StartMock();

        gameTeste.Start();

        var chessman = gameTeste.chesspiece.GetComponent<Chessman>();

        // Create a new GameController object
        var controllerObject = new GameObject();
        var gameController = controllerObject.AddComponent<Game>();

        // Assign the GameController object to the controller variable
        chessman.controller = gameController.gameObject;

        var pieces = chessman.SetPiecesPosition(gameTeste.GetWhitePlayer(), gameTeste.GetBlackPlayer());

        boardBot.AddPiece(pieces[0, 0]);

        Assert.AreEqual(boardBot.wPieces.Count, 1);

        boardBot.RemovePiece(pieces[0, 0]);

        Assert.AreEqual(boardBot.wPieces.Count, 0);

    }

}