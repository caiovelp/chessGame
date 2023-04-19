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

    //Chamada quando o moveplate é criado
    public void Start()
    {
        if (attack)
        {
            //A cor do sprite muda para vermelho
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    //Quando alguém clica no moveplate
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if(attack)
        {
            //Quando uma peça é eliminada, ela vai para o canto esq/dir do tabuleiro.
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetXBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoordinates();

        controller.GetComponent<Game>().SetPosition(reference);

        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoordinates(int x, int y) 
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
