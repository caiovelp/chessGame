using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move {
    public int x;
    public int y;
    public int destX;
    public int destY;
    public int score;
    public void init(int _x, int _y, int _destX, int _destY){
        x = _x;
        y = _y;
        destX = _destX;
        destY = _destY;
    }
}
public class Piece {
    public int type;
    public int team;
    public void init(int _type, int _team){
        type = _type;
        team = _team;
    }
}
public class Board {
    public Piece[] pieces = new Piece[8*8];
    public void add_piece(int type, int team, int x, int y){
        Piece p = new Piece();
        p.init(type, team);
        pieces[y*8 + x] = p;
    }

    public Piece get_piece(int x, int y){
        return pieces[y*8 + x];
    }
}

public class AI : MonoBehaviour
{
    public Board simplify(){
        // Recebe o estado atual do jogo e transforma na classe Board

        // Board board = new Board();
        // return board;
    }

    public Move bestChoice(Board board){
        // Recebe um Board simplificado para decidir qual o melhor movimento. Retorna a posição da peça, seguida de seu destino

        // Move best_move;
        // Pra cada peça
        // - Pegar todas as opções de movimento, e dar uma pontuação pra cada.

        // Retorna a movimentação de melhor pontuação
    }

    // Pode ser uma má ideia
    // public Move bestChoice(Piece piece, Board board){
    //     // Pra esta peça, ver o melhor moviemnto e retornar só ele
    // }
}