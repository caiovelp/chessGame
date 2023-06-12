using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;
using UnityEngine.TestTools.Utils;

public class MenuControllerTests
{
    private GameObject testGameObject;
    private MenuController menuController;

    [SetUp]
    public void Setup()
    {
        testGameObject = new GameObject();
        menuController = testGameObject.AddComponent<MenuController>();
    }

    [UnityTest]
    public IEnumerator StartGame_LoadsMainScene()
    {
        menuController.mainSceneName = "Game";

        menuController.StartGame();

        yield return new WaitForSeconds(1f); // Aguarda 1 segundo para permitir a transição de cena

        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
    }

    // [Test]
    // public void QuitGame_QuitsApplication()
    // {
    //     // Chama a função QuitGame()
    //     // menuController.QuitGame();

    //     // Verifica se o aplicativo está sendo encerrado
    //     Assert.IsTrue(menuController.quit);
    // }
}
