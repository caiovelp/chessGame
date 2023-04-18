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
            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn; break;
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
}
