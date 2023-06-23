using UnityEngine;
using NUnit.Framework;
using Mocks;

public class AITest
{
    private Game game;
    
    [SetUp]
    public void Setup()
    {
        game = new Game();
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

        Assert.IsTrue(IA);
    }
    
    [Test]
    public void AIReturnBlackIA_Test()
    {
        bool IA = game.IsBlackIa();

        Assert.IsTrue(IA);
    }

    [Test]
    public void AIMove_Test()
    {
        
        MocksTest mocksTest = new MocksTest();
        mocksTest.game = game;
        
        var gameTeste = mocksTest.StartMock();

        gameTeste.Start();
        
        var hgt = gameTeste.GetPosition(0, 1);

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

}