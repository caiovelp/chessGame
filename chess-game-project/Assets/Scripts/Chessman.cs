using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sprite white_queen, white_king, white_bishop, white_tower, white_knight, white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Ajusta as posições das peças
        SetCoodinates();

        switch (this.name)
        {
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; break;
            case "white_tower": this.GetComponent<SpriteRenderer>().sprite = white_tower; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; break;
        }
    }

    public void SetCoodinates() 
    {
        float x = xBoard;
        float y = yBoard;

        //Provavelmente esses valores deverão ser ajustados
        x *= 0.66f;
        y *= 0.66f;

        x -= 2.3f;
        y -= 2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
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
