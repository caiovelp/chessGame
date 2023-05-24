using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    public Sprite whiteQueen, blackQueen;

    //Posições do tabuleiro
    int matrixX;
    int matrixY;

    // false: movimento, true: ataque
    public bool attack = false;
    public bool promote = false;

    // Chamada quando o moveplate é criado
    public void Start()
    {
        if (attack)
        {
            // A cor do sprite muda para vermelho
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        
            if (promote)
            {
                // a cor da sprite muda para roxo
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.2f, 0.6f, 1.0f);
            }
        }
        else if (promote)
        {
            // A cor da sprite muda para azul
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }

    /*
        Função do Unity que é chamada quando o usuário clica e solta o botão do mouse.
        Nesse caso, essa OnMouseUp é responsável pela troca de um moveplate com a peça
        que está sofrendo a interação do usuário.
    */
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if(attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX,matrixY);

            if (cp.name == "whiteKing") controller.GetComponent<Game>().Winner("preto");
            if (cp.name == "blackKing") controller.GetComponent<Game>().Winner("branco");
            
            //controller.GetComponent<Game>().AppendDestroyedPieces(cp);
            
            //TODO move to side
            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoordinates();
        controller.GetComponent<Game>().SetPosition(reference);
        
        if (promote)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            if (matrixY == 7)
            {
                cp.name = "whiteQueen";
                cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteQueen();   
            }
            else
            {
                cp.name = "blackQueen";
                cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackQueen();
            }
        }
        //Alterna o jogador atual
        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Chessman>().DestroyMovePlates();
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
