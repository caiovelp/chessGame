using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Chessman : MonoBehaviour
{

    // GameObjects
    public GameObject controller;
    public GameObject movePlate;

    // Posições
    private int xBoard = -1;
    private int yBoard = -1;

    // Jogador
    private string player;

    // Peças de xadrez - as pretas vão aqui também
    public Sprite whiteQueen, whiteKing, whiteBishop, whiteTower, whiteKnight, whitePawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Ajusta as posições das peças
        SetCoordinates();

        switch (this.name)
        {
            case "whiteQueen": this.GetComponent<SpriteRenderer>().sprite = whiteQueen; break;
            case "whiteKing": this.GetComponent<SpriteRenderer>().sprite = whiteKing; break;
            case "whiteBishop": this.GetComponent<SpriteRenderer>().sprite = whiteBishop; break;
            case "whiteTower": this.GetComponent<SpriteRenderer>().sprite = whiteTower; break;
            case "whiteKnight": this.GetComponent<SpriteRenderer>().sprite = whiteKnight; break;
            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn; player = "white"; break;
        }
    }

    public void SetCoordinates() 
    {
        float x = xBoard;
        float y = yBoard;

        //Provavelmente esses valores deverão ser ajustados
        x *= 1.15f;
        y *= 1.15f;

        //Provavelmente esses valores deverão ser ajustados
        x -= 4f;
        y -= 3.2f;

        this.transform.position = new Vector3(x, y, -1);
    }

    public int GetXBoard() 
    {
        return xBoard;
    }

    public int GetYBoard() 
    {
        return yBoard;
    }

    public void SetXBoard(int x) 
    {
        xBoard = x;
    }

    public void SetYBoard(int y) 
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        foreach (var movePlate in movePlates)
        {
            Destroy(movePlate);
        }
    }

    public void InitiateMovePlates() 
    {
        //Um para cada peça
        switch (this.name)
        {
            case "whitePawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

        public void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Recupera o valor do tabuleiro para converter em xy coordenadas
        float x = matrixX;
        float y = matrixY;

        //Ajuste do offset em SetCoordinates
        x *= 1.15f;
        y *= 1.15f;

        //Ajuste do offset em SetCoordinates
        x -= 4f;
        y -= 3.2f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoordinates(matrixX, matrixY);
    }

        public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Recupera o valor do tabuleiro para converter em xy coordenadas
        float x = matrixX;
        float y = matrixY;

        //Ajuste do offset em SetCoordinates
        x *= 1.15f;
        y *= 1.15f;

        //Ajuste do offset em SetCoordinates
        x -= 4f;
        y -= 3.2f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoordinates(matrixX, matrixY);
    }
}
