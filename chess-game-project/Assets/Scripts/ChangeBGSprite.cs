using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGSprite : MonoBehaviour
{

    [SerializeField] private SpriteRenderer rendBG;

    [SerializeField] private Sprite[] fundos;

    private int fundoID;


    void Awake()
    {
        fundoID = PlayerPrefs.GetInt("fundo", 5);
    }


    void Start()
    {
        SetItem("fundo");
    }


    public void SelectFundo(bool isForward)
    {
        if (isForward)
        {
            if (fundoID == fundos.Length - 1)
            {
                fundoID = 0;
            }
            else
            {
                fundoID++;
            }
        }
        else
        {
            if (fundoID == 0)
            {
                fundoID = fundos.Length - 1;
            }
            else
            {
                fundoID--;
            }
        }

        Debug.Log(fundoID);

        PlayerPrefs.SetInt("fundo", fundoID);
        SetItem("fundo");
    }


    void SetItem(string type)
    {
        switch (type)
        {
            case "fundo":
                rendBG.sprite = fundos[fundoID];
                break;
        }
    }

}
