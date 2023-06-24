using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;


    //Posições do tabuleiro
    int matrixX;
    int matrixY;

    // false: movimento, true: ataque
    public bool attack = false;
    public bool promote = false;

    // Escolha da promoção
    public string pecaPromo = "Tower";

    public bool roque = false;

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

        if (roque)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, .5f, 0.0f, 1.0f);
        }
    }

    //Funções que mudam o nome da escolha de peça. Atrelados aos Butões do Panel
    public void PromoteToQueen()
    {
        pecaPromo = "Queen";
    }

    public void PromoteToTower()
    {
        pecaPromo = "Tower";
    }

    public void PromoteToBishop()
    {
        pecaPromo = "Bishop";
    }

    public void PromoteToKnight()
    {
        pecaPromo = "Knight";
    }


    /*
        Função do Unity que é chamada quando o usuário clica e solta o botão do mouse.
        Nesse caso, essa OnMouseUp é responsável pela troca de um moveplate com a peça
        que está sofrendo a interação do usuário.
    */
    public void OnMouseUp()
    {
        if (roque)
            RoqueMovement();
        else
            Movement();
    }

    private void RoqueMovement()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        if (matrixX == 0)
        {
                reference.GetComponent<Chessman>().SetXBoard(2);
                reference.GetComponent<Chessman>().SetCoordinates();

                controller.GetComponent<Game>().SetPosition(reference);  
                SetReference(controller.GetComponent<Game>().GetPosition(0, matrixY));
                
                reference.GetComponent<Chessman>().SetXBoard(3);
                reference.GetComponent<Chessman>().SetYBoard(matrixY);
                reference.GetComponent<Chessman>().SetCoordinates();
                
                controller.GetComponent<Game>().SetPosition(reference);
        }
        else 
        {
            reference.GetComponent<Chessman>().SetXBoard(6);
            reference.GetComponent<Chessman>().SetCoordinates();

            controller.GetComponent<Game>().SetPosition(reference);
            SetReference(controller.GetComponent<Game>().GetPosition(7, matrixY));
            
            reference.GetComponent<Chessman>().SetXBoard(5);
            reference.GetComponent<Chessman>().SetYBoard(matrixY);
            reference.GetComponent<Chessman>().SetCoordinates();
                
            controller.GetComponent<Game>().SetPosition(reference);
        }
        
        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void Movement()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            EatMovement();
        }
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoordinates();
        reference.GetComponent<Chessman>().SetMove(true);

        controller.GetComponent<Game>().SetPosition(reference);

        //Função de promoção, muda a peça para a escolhida
        if (promote)
        {

            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            if (matrixY == 7)
            {
                if (pecaPromo == "Tower")
                {
                    cp.name = "whiteTower";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteTower();
                }
                if (pecaPromo == "Queen")
                {
                    cp.name = "whiteQueen";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteQueen();
                }
                if (pecaPromo == "Bishop")
                {
                    cp.name = "whiteBishop";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteBishop();
                }
                if (pecaPromo == "Knight")
                {
                    cp.name = "whiteKnight";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteKnight();
                }
            }
            else
            {
                if (pecaPromo == "Tower")
                {
                    cp.name = "blackTower";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackTower();
                }
                if (pecaPromo == "Queen")
                {
                    cp.name = "blackQueen";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackQueen();
                }
                if (pecaPromo == "Bishop")
                {
                    cp.name = "blackBishop";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackBishop();
                }
                if (pecaPromo == "Knight")
                {
                    cp.name = "blackKnight";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackKnight();
                }
            }
        }
        //Alterna o jogador atual
        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Chessman>().DestroyMovePlates();
    }
    
    

    public void EatMovement()
    {
        GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX,matrixY);
        
        if (cp.name == "whiteKing") controller.GetComponent<Game>().Winner("preto");
        if (cp.name == "blackKing") controller.GetComponent<Game>().Winner("branco");

        controller.GetComponent<Game>().SearchAndDestroy(cp);
        controller.GetComponent<Game>().SerPositionSpriteEmpty(matrixX, matrixY);
        controller.GetComponent<Game>().SetPositionEmpty(matrixX, matrixY);
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
