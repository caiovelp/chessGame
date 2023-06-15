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
    }

    public static PieceType ShowPromotionMenu()
    {
        PieceType selectedPiece = PieceType.Queen;
        bool promote = true;

        if (promote)
        {
            bool queen = EditorUtility.DisplayDialog("Promoção", "Escolha para qual peça será promovida:", "Rainha", "Torre");
            if (queen)
            {
                selectedPiece = PieceType.Queen;
            }
            else
            {
                bool tower = EditorUtility.DisplayDialog("Promoção", "Escolha para qual peça será promovida:", "Torre", "Bispo");
                if (tower)
                {
                    selectedPiece = PieceType.Tower;
                }
                else
                {
                    bool bishop = EditorUtility.DisplayDialog("Promoção", "Escolha para qual peça será promovida:", "Bispo", "Cavalo");
                    if (bishop)
                    {
                        selectedPiece = PieceType.Bishop;
                    }
                    else
                    {
                        selectedPiece = PieceType.Knight;
                    }
                }
            }
        }

        return selectedPiece;
    }


    public enum PieceType
    {
        Queen,
        Tower,
        Bishop,
        Knight
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

            int i = 0;
            int j = 0;

            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX,matrixY);

            if (cp.name == "whiteKing") controller.GetComponent<Game>().Winner("preto");
            if (cp.name == "blackKing") controller.GetComponent<Game>().Winner("branco");
            
            controller.GetComponent<Game>().AppendDestroyedPieces(cp);
            (i,j) = controller.GetComponent<Game>().SearchDestroyedPieces(cp);
            if (cp.GetComponent<Chessman>().GetPlayer() == "black")
                controller.GetComponent<Game>().Create(cp.name, i + 6, j);
            else
                controller.GetComponent<Game>().Create(cp.name, i - 3, j);
            
            controller.GetComponent<Game>().SerPositionSpriteEmpty(matrixX, matrixY);
            controller.GetComponent<Game>().SetPositionEmpty(matrixX, matrixY);   
            //TODO move to side

            controller.GetComponent<Game>().SearchAndDestroy(cp);
            
            controller.GetComponent<Game>().SerPositionSpriteEmpty(matrixX, matrixY);
            controller.GetComponent<Game>().SetPositionEmpty(matrixX, matrixY);

        }
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoordinates();
        controller.GetComponent<Game>().SetPosition(reference);
        
        if (promote)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            PieceType promocao = ShowPromotionMenu();
            if (matrixY == 7)
            {
                if(promocao == PieceType.Tower)
                {
                    cp.name = "whiteTower";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteTower();
                }
                if(promocao == PieceType.Queen)
                {
                    cp.name = "whiteQueen";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteQueen();
                }
                if(promocao == PieceType.Bishop)
                {
                    cp.name = "whiteBishop";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteBishop();
                }
                if(promocao == PieceType.Knight)
                {
                    cp.name = "whiteKnight";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetWhiteKnight();
                }
            }
            else
            {
                if (promocao == PieceType.Tower)
                {
                    cp.name = "blackTower";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackTower();
                }
                if (promocao == PieceType.Queen)
                {
                    cp.name = "blackQueen";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackQueen();
                }
                if (promocao == PieceType.Bishop)
                {
                    cp.name = "blackBishop";
                    cp.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<Chessman>().GetBlackBishop();
                }
                if (promocao == PieceType.Knight)
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
