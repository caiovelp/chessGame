using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move {
    public int x;
    public int y;
    public int destX;
    public int destY;
    public int score;
    public Move(int _x, int _y, int _destX, int _destY){
        x = _x;
        y = _y;
        destX = _destX;
        destY = _destY;
        score = 0;
    }
    public Move(int _x, int _y, int _destX, int _destY, int _score){
        x = _x;
        y = _y;
        destX = _destX;
        destY = _destY;
        score = _score;
    }
}
public class Piece {
    public int type;
    public int team;
    public int x;
    public int y;
    public Piece(int _type, int _team, int _x, int _y){
        type = _type;
        team = _team;
        x = _x;
        y = _y;
    }
	public int typeToScore(){
    	return this.type;
	}
    // 4 straight lines
    public Move[] plus(Board board){
		Piece piece = this;
        List<Move> moves = new List<Move>();
        int x = piece.x;
        int y = piece.y;
        for(int i = x + 1; i < 8; i++){
            if(board.getPiece(i, y) == null){
                moves.Add(new Move(x,y, i,y, 0));
            }else{
                if(board.getPiece(i, y).team != piece.team){
                    moves.Add(new Move(x,y, i,y, (board.getPiece(i, y)).typeToScore()));
                }
                break;
            }
        }

        for(int i = x - 1; i >= 0; i--){
            if(board.getPiece(i, y) == null){
                moves.Add(new Move(x,y, i,y, 0));
            }else{
                if(board.getPiece(i, y).team != piece.team){
                    moves.Add(new Move(x,y, i,y, (board.getPiece(i, y)).typeToScore()));
                }
                break;
            }
        }

        for(int i = y + 1; i < 8; i++){
            if(board.getPiece(x, i) == null){
                moves.Add(new Move(x,y, x,i, 0));
            }else{
                if(board.getPiece(x, i).team != piece.team){
                    moves.Add(new Move(x,y, x,i, (board.getPiece(x, i)).typeToScore()));
                }
                break;
            }
        }

        for(int i = y - 1; i >= 0; i--){
            if(board.getPiece(x, i) == null){
                moves.Add(new Move(x,y, x,i, 0));
            }else{
                if(board.getPiece(x, i).team != piece.team){
                    moves.Add(new Move(x,y, x,i, (board.getPiece(x, i)).typeToScore()));
                }
                break;
            }
        }

        return moves.ToArray();
    }
    // 4 diagonals
    public Move[] cross(Board board){
		Piece piece = this;
        List<Move> moves = new List<Move>();
        int x = piece.x;
        int y = piece.y;
        for(int i = 1; (i+x < 8) && (i+y < 8); i++){
            if(board.getPiece(x+i, y+i) == null){
                moves.Add(new Move(x,y, x+i, y+i, 0));
            }else{
                if(board.getPiece(x+i, y+i).team != piece.team){
                    moves.Add(new Move(x,y, x+i,y+i, (board.getPiece(x+i,y+i)).typeToScore()));
                }
                break;
            }
        }

        for(int i = -1; (i+x >= 0) && (i+y >= 0); i--){
            if(board.getPiece(x+i, y+i) == null){
                moves.Add(new Move(x,y, x+i, y+i, 0));
            }else{
                if(board.getPiece(x+i, y+i).team != piece.team){
                    moves.Add(new Move(x,y, x+i,y+i, (board.getPiece(x+i,y+i)).typeToScore()));
                }
                break;
            }
        }

        for(int i = 1; (i+x < 8) && (i-y >= 0); i++){
            if(board.getPiece(x+i, i-y) == null){
                moves.Add(new Move(x,y, x+i, i-y, 0));
            }else{
                if(board.getPiece(x+i, i-y).team != piece.team){
                    moves.Add(new Move(x,y, x+i,i-y, (board.getPiece(x+i,i-y)).typeToScore()));
                }
                break;
            }
        }

        for(int i = 1; (i-x >= 0) && (i+y < 8); i++){
            if(board.getPiece(i-x, y+i) == null){
                moves.Add(new Move(x,y, i-x, y+i, 0));
            }else{
                if(board.getPiece(i-x, y+i).team != piece.team){
                    moves.Add(new Move(x,y, i-x,y+i, (board.getPiece(i-x,y+i)).typeToScore()));
                }
                break;
            }
        }

        return moves.ToArray();
    }
    // Adjacent squares
    public Move[] adj(Board board){
		Piece piece = this;
		List<Move> moves = new List<Move>();
		int x;
		int y;
		for(int i = -1; i <= 1; i+=2){
			if(board.getPiece(x+i, y) == null){
				moves.Add(new Move(x,y, x+i, y));
			}else if(target.team != piece.team){
				moves.Add(new Move(x,y, x+i,y, target.typeToScore()));
			}
		}
		for(int y = -1; y <= 1; y+=2){
			if(board.getPiece(x, y+i) == null){
				moves.Add(new Move(x,y, x,y+i));
			}else if(target.team != piece.team){
				moves.Add(new Move(x,y, x,y+i, target.typeToScore()));
			}
		}
		
		for(int i = -1; i <= 1; i+=2){
			for(int z = -1; z <= 1; z+=2){
				if(board.getPiece(x+i, y+z) == null){
					moves.Add(new Move(x,y, x+i, y+z));
				}else if(target.team != piece.team){
					moves.Add(new Move(x,y, x+i, y+z, target.typeToScore()));
				}
			}
		}
	}
    // Adjacent squares
    public Move[] pawn(Board board){
		Piece piece = this;
		List<Move> moves = new List<Move>();

		int firstMove = 0;
		int delta = 0;
		if(piece.team = 0){delta = 1;if(piece.y == 1){firstMove = 1;}}
		if(piece.team = 1){delta = -1;if(piece.y == 6){firstMove = 1;}}


		if(board.getPiece(piece.x, piece.y + delta) == null){ // Na teoria, o peão nunca estaria no topo
			moves.Add(new Move(piece.x, piece.y + delta));
			if(firstMove){
				if(board.getPiece(piece.x, piece.y + delta*2) == null){ // Só vale pra casa inicial
					moves.Add(new Move(piece.x, piece.y + delta));
				}
			}
		}
	}
    public Move[] L(Board board){
		Piece piece = this;
        List<Move> moves = new List<Move>();
        Piece target;
        int x;
        int y;
        for(int i = -1; i <= 0; i++){ // Invert Y
            for(int z = 0; z <= 1; z++){ // Change x and y sizes
                for(int w = -1; w <= 0; w++){ // Invert x
                    x = (1+z)*w;
                    y = (2-z)*i;
                    target = board.getPiece(piece.x + x,piece.y + y);
                    if(target == null){
                        moves.Add(new Move(x,y, x+i, y+i));
                    }else if(target.team != piece.team){
                        moves.Add(new Move(x,y, x+i,y+i, target.typeToScore()));
                    }
                }
            }
        }
        return moves.ToArray();
    }
}
public class Board {
    public Piece[] pieces = new Piece[8*8];
    public void addPiece(int type, int team, int x, int y){
        pieces[y*8 + x] = new Piece(type, team, x, y);
    }

    public Piece[] getPieces() {
        List<Piece> resp = new List<Piece>();
        foreach (var p in pieces)
        {
            if(p != null){resp.Add(p);}
        }
        return resp.ToArray();
    }
    public Piece getPiece(int x, int y){
        return pieces[y*8 + x];
    }
    public void _move(Move _move){
        move(_move.x, _move.y, _move.destX, _move.destY);
    }
    public void _rMove(Move _move){
        move(_move.destX, _move.destY, _move.x, _move.y);
    }
    public void move(int x, int y, int xd, int yd){
        pieces[y*8 + x].x = xd;
        pieces[y*8 + x].y = yd;
        pieces[yd*8 + xd] = this.pieces[y*8 + x];
        pieces[y*8 + x] = null;
    }
}

public class AI{
    // public Board simplify(){
    //     // Recebe o estado atual do jogo e transforma na classe Board

    //     // Board board = new Board();
    //     // return board;
    //     return new Board();
    // }

    // public Move bestChoice(Board board, Move[] pastMoves, int turn){
    //     // Recebe um Board simplificado para decidir qual o melhor movimento. Retorna a posição da peça, seguida de seu destino

    //     Move[] best_moves = new Move[];
    //     // Pra cada peça
    //     // - Pegar todas as opções de movimento, e dar uma pontuação pra cada.
    //     foreach (var p in board.getPieces()){
    //         possibleMoves(p, board);
    //     }

    //     // Retorna a movimentação de melhor pontuação
    // }

    // public Move[] possibleMoves(Piece piece, Board board){
    //     int t = piece.type;
    //     switch (t){
    //         case 0: break; // Peao
    //         case 1: break; // Cavalo
    //         case 2: break; // Bispo
    //         case 3: break; // Castelo
    //         case 4: break; // Rainha
    //         case 5: break; // Rei
    //     }
    // }

}