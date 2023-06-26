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
    public bool roque = false;

    // Chamada quando o moveplate é criado
    public void Start()
    {
        if (attack)
        {
            // A cor do sprite muda para vermelho
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        if (roque)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, .5f, 0.0f, 1.0f);
        }
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

    public void Movement()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        Chessman chessman = reference.GetComponent<Chessman>();
        Game game = controller.GetComponent<Game>();
        if (attack)
        {
            GameObject cp = game.GetPosition(matrixX, matrixY);
            Chessman enemyChessman = cp.GetComponent<Chessman>();
            if (enemyChessman.GetName() == "whiteKing") game.Winner("preto");
            if (enemyChessman.GetName() == "blackKing") game.Winner("branco");

            game.SearchAndDestroy(cp);
            
            game.SerPositionSpriteEmpty(matrixX, matrixY);
            game.SetPositionEmpty(matrixX, matrixY);
        }

        game.SetPositionEmpty(chessman.GetXBoard(), chessman.GetYBoard());

        chessman.SetXBoard(matrixX);
        chessman.SetYBoard(matrixY);
        chessman.SetCoordinates();
        chessman.SetMove(true);
        game.SetPosition(reference);

        //Alterna o jogador atual
        game.NextTurn();
        chessman.DestroyMovePlates();
    }

    private void RoqueMovement()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        Game game = controller.GetComponent<Game>();
        Chessman chessman = reference.GetComponent<Chessman>();

        //Esvazia a posição atual do rei
        game.SetPositionEmpty(chessman.GetXBoard(), chessman.GetYBoard());

        chessman.SetYBoard(matrixY);
        // Torre da Esquerda
        if (matrixX == 0)
        {
            chessman.SetXBoard(2);
            chessman.SetCoordinates();
            game.SetPosition(reference);  

            // SetReference(game.GetPosition(0, matrixY));
            GameObject torreReference = game.GetPosition(0, matrixY);
            Chessman torreChessman = torreReference.GetComponent<Chessman>();

            //Esvazia a posição atual da torre
            game.SetPositionEmpty(torreChessman.GetXBoard(), torreChessman.GetYBoard());

            torreChessman.SetXBoard(3);
            torreChessman.SetYBoard(matrixY);
            torreChessman.SetCoordinates();
            game.SetPosition(torreReference); 
        }
        // Torre da Direita
        else 
        {
            chessman.SetXBoard(6);
            chessman.SetCoordinates();
            game.SetPosition(reference);

            // SetReference(game.GetPosition(0, matrixY));
            GameObject torreReference = game.GetPosition(7, matrixY);
            Chessman torreChessman = torreReference.GetComponent<Chessman>();

            //Esvazia a posição atual da torre
            game.SetPositionEmpty(torreChessman.GetXBoard(), torreChessman.GetYBoard());
            
            torreChessman.SetXBoard(5); 
            torreChessman.SetYBoard(matrixY);
            torreChessman.SetCoordinates();
            game.SetPosition(torreReference);        
        }     
        
        game.NextTurn();
        chessman.DestroyMovePlates();
    }

    public int GetMatrixX()
    {
        return matrixX;
    }

    public int GetMatrixY()
    {
        return matrixY;
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
