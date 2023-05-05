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
        controller = GameObject.FindGameObjectWithTag("GameController");

        if(attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX,matrixY);

            //TODO move to side
            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoordinates();

        controller.GetComponent<Game>().SetPosition(reference);

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
