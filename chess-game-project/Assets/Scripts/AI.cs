using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// class Program{
//   public static void Main(String[] argv){
//     Board b = new Board();
//     b.addPiece(3, 0, 0, 0);
//     b.addPiece(0, 1, 0, 7);
//     b.addPiece(3, 1, 1, 7);

//     Move bestMove = AI.bestChoice(b, 0, 1);
//     for(int i=7; i >= 0;i--){
//       for(int t=0; t<8;t++){
//         Piece a = b.getPiece(t,i);
//         if(a == null){
//           Console.Write(' ');
//         }else{
//           Console.Write(a.type);
//         }
//         Console.Write("|");
//       }
//       Console.Write("\n");
//     }
    
//     Console.WriteLine("Resp: "+ bestMove.x +" "+ bestMove.y + " -> " + bestMove.destX +" "+ bestMove.destY +" $"+ bestMove.score);
//   }
// }

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
    static public Move fake(){
        return new Move(-1,-1,-1,-1,-1);
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
    	return this.type+1;
	}
    public Move[] movement(Board board){
        switch(this.type){
            case 0: return this.pawn(board); // Peao
            case 1: return this.L(board); // Cavalo
            case 2: return this.cross(board); // Bispo
            case 3: return this.plus(board); // Castelo   
            case 4: return this.queen(board); // Rainha    
        }
        return this.adj(board); // Rei
    }
    public Move[] queen(Board board){
        Move[] x = this.cross(board);
        Move[] y = this.plus(board);
        Move[] z = new Move[x.Length + y.Length];
        x.CopyTo(z, 0);
        y.CopyTo(z, x.Length);
        return z;
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
        Piece target;
		List<Move> moves = new List<Move>();
		int x = piece.x;
        int y = piece.y;
		for(int i = -1; i <= 1; i+=2){
            target = board.getPiece(x+i, y);
			if(target == null){
				moves.Add(new Move(x,y, x+i, y));
			}else if(target.team != piece.team){
				moves.Add(new Move(x,y, x+i,y, target.typeToScore()));
			}
		}
		for(int i = -1; i <= 1; i+=2){
            target = board.getPiece(x, y+i);
			if(target == null){
				moves.Add(new Move(x,y, x,y+i));
			}else if(target.team != piece.team){
				moves.Add(new Move(x,y, x,y+i, target.typeToScore()));
			}
		}
		
		for(int i = -1; i <= 1; i+=2){
			for(int z = -1; z <= 1; z+=2){
                target = board.getPiece(x, y+i);
				if(target == null){
					moves.Add(new Move(x,y, x+i, y+z));
				}else if(target.team != piece.team){
					moves.Add(new Move(x,y, x+i, y+z, target.typeToScore()));
				}
			}
		}
        return moves.ToArray();
	}
    // Adjacent squares
    public Move[] pawn(Board board){
		Piece piece = this;
        Piece target;
		List<Move> moves = new List<Move>();

		int firstMove = 0;
		int delta = 0;
		if(piece.team == 0){delta = 1;if(piece.y == 1){firstMove = 1;}}
		if(piece.team == 1){delta = -1;if(piece.y == 6){firstMove = 1;}}


		if(board.getPiece(piece.x, piece.y + delta) == null){ // Na teoria, o peão nunca estaria no topo
			moves.Add(new Move(piece.x, piece.y, piece.x, piece.y + delta));
			if(firstMove == 1){
				if(board.getPiece(piece.x, piece.y + delta*2) == null){ // Só vale pra casa inicial
					moves.Add(new Move(piece.x, piece.y, piece.x, piece.y + delta));
				}
			}
		}
        for(int ii = -1; ii <= 1; ii+=2){
            target = board.getPiece(piece.x + ii, piece.y + delta);
            if(target != null && target.team != this.team){
                moves.Add(new Move(piece.x, piece.y, piece.x + ii, piece.y + delta, target.typeToScore()));
            }
        }
        return moves.ToArray();
	}
    public Move[] L(Board board){
		Piece piece = this;
        List<Move> moves = new List<Move>();
        Piece target;
        int x = piece.x;
        int y = piece.y;
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
    public Piece[] positions = new Piece[64];
    private int w = 0;
    public Piece[] w_pieces = new Piece[16];
    private int b = 0;
    public Piece[] b_pieces = new Piece[16];
    public void addPiece(int type, int team, int x, int y){
        Piece p = new Piece(type, team, x, y);
        positions[y*8 + x] = p;
        if(team == 0){
          w_pieces[w] = p;
          w+=1;
        }else{
          b_pieces[b] = p;
          b+=1;
        }
    }

    public Piece[] getPieces(int turn) {
        List<Piece> resp = new List<Piece>();
        if(turn == 0){
            foreach (var p in w_pieces)
            {
                if(p != null){resp.Add(p);}
            }
            return resp.ToArray();
        }else{
            foreach (var p in b_pieces)
            {
                if(p != null){resp.Add(p);}
            }
            return resp.ToArray();
        }
    }
    public Piece getPiece(int x, int y){
        return this.positions[y*8 + x];
    }
    public void setPiece(int x, int y, Piece p){
        this.positions[y*8 + x] = p;
    }
    public void _move(Move _move){
        move(_move.x, _move.y, _move.destX, _move.destY);
    }
    public void _rMove(Move _move){
        move(_move.destX, _move.destY, _move.x, _move.y);
    }
    public void move(int x, int y, int xd, int yd){
        positions[y*8 + x].x = xd;
        positions[y*8 + x].y = yd;
        positions[yd*8 + xd] = this.positions[y*8 + x];
        positions[y*8 + x] = null;
    }
}

public class AI{
    static public Move bestChoice(Board board, int turn, int depth){
        return _bestChoice(board, turn, depth, turn, -1, 9999);
    }
    static public Move _bestChoice(Board board, int turn, int depth, int maxmizeTurn, int alpha, int beta){        
        Piece _p;
        Move bestMove = Move.fake();
        foreach(var p in board.getPieces(turn)){
            foreach(var m in p.movement(board)){
                // Console.WriteLine("T"+turn +" - "+ m.x +" "+ m.y + " -> " + m.destX +" "+ m.destY +" $"+ m.score);
                if(depth > 0){
                    _p = board.getPiece(m.destX, m.destY);
                    board._move(m); // Possivelmente parte de um bug (1)
                    m.score -= _bestChoice(board, (turn+1)%2, depth-1, maxmizeTurn, alpha, beta).score; // Recursive Score
                    board._rMove(m); // Possivelmente parte de um bug (2)
                    board.setPiece(m.destX, m.destY, _p);
                }
                if(m.score > bestMove.score){ // Max(this, last)
                    bestMove = m;
                    if(turn == maxmizeTurn){
                        if(m.score > alpha){
                            alpha = m.score;
                            if(beta <= alpha){
                                return Move.fake(); // Return doesn't matter here
                            }
                        }
                    }else{
                        if(m.score > beta){
                            beta = m.score;
                            if(beta <= alpha){
                                return Move.fake(); // Return doesn't matter here
                            }
                        }
                    }
                }
            }
        }
        return bestMove;
    }
}