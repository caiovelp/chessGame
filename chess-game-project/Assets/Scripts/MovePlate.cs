using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //Posições do tabuleiro
    int matrixX;
    int matrixY;

    // false: movimento, true: ataque
    public bool attack = false;

    // Chamada quando o moveplate é criado
    public void Start()
    {
        if (attack)
        {
            // A cor do sprite muda para vermelho
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    /*
        Função do Unity que é chamada quando o usuário clica e solta o botão do mouse.
        Nesse caso, essa OnMouseUp é responsável pela troca de um moveplate com a peça
        que está sofrendo a interação do usuário.
    */
    public void OnMouseUp()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");

        Chessman chessman = reference.GetComponent<Chessman>();
        Game game = controller.GetComponent<Game>();

        if(attack)
        {
            int i = 0;
            int j = 0;
            GameObject cp = game.GetPosition(matrixX, matrixY);
            Chessman enemyChessman = cp.GetComponent<Chessman>();
            if (enemyChessman.pieceName == "whiteKing") game.Winner("preto");
            if (enemyChessman.pieceName == "blackKing") game.Winner("branco");

            //TODO move to side
            controller.GetComponent<Game>().SearchAndDestroy(cp);
            
            controller.GetComponent<Game>().SerPositionSpriteEmpty(matrixX, matrixY);
            controller.GetComponent<Game>().SetPositionEmpty(matrixX, matrixY);
            
        }
        

        game.SetPositionEmpty(chessman.GetXBoard(), chessman.GetYBoard());

        chessman.SetXBoard(matrixX);
        chessman.SetYBoard(matrixY);
        chessman.SetCoordinates();

        game.SetPosition(reference);

        //Alterna o jogador atual
        game.NextTurn();

        chessman.DestroyMovePlates();
    }

    public int GetMatrixX()
    {
        return matrixX;
    }

    public int GetMatrixY()
    {
        return matrixY;
    }

    //Função para definir as coordenadas de acordo com uma matriz.
    public void SetCoordinates(int x, int y) 
    {
        matrixX = x;
        matrixY = y;
    }

    // Funções de Set e Get para um gameobject.
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
