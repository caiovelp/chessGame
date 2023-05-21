using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChangeBoardSprite : MonoBehaviour
{

    [SerializeField] private SpriteRenderer rend;

    [SerializeField] private Sprite[] tabuleiros;

    private int tabuleiroID;


    void Awake()
    {
        tabuleiroID = PlayerPrefs.GetInt("tabuleiro", 5);
    }


    void Start()
    {
        SetItem("tabuleiro");
    }


    public void SelectTabuleiro(bool isForward)
    {
        if (isForward)
        {
            if (tabuleiroID == tabuleiros.Length - 1)
            {
                tabuleiroID = 0;
            }
            else
            {
                tabuleiroID++;
            }
        }
        else
        {
            if (tabuleiroID == 0)
            {
                tabuleiroID = tabuleiros.Length - 1;
            }
            else
            {
                tabuleiroID--;
            }
        }

        Debug.Log(tabuleiroID);

        PlayerPrefs.SetInt("tabuleiro", tabuleiroID);
        SetItem("tabuleiro");
    }


    void SetItem(string type)
    {
        switch (type)
        {
            case "tabuleiro":
                rend.sprite = tabuleiros[tabuleiroID];
                break;
        }
    }

}