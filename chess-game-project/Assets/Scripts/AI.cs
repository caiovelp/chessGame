using System;
using System.Collections.Generic;

public class Move {
    public int x;
    public int y;
    public int destX;
    public int destY;
    public int score;
    public bool attack;
    public bool roque;
    public Move(int _x, int _y, int _destX, int _destY, bool roque = false)
    {
        Board board = new Board();
        // if (!board.VerifyInsideBoard(_destX, _destX) || !board.VerifyInsideBoard(_x, _y))
        // {
        //     throw new Exception("Jogada " + _destX + _destY + "está fora do tabuleiro");
        // }
        x = _x;
        y = _y;
        destX = _destX;
        destY = _destY;
        score = 0;
        this.roque = roque;
        attack = false;
    }
    public Move(int _x, int _y, int _destX, int _destY, int _score){
        Board board = new Board();
        // if (!board.VerifyInsideBoard(_destX, _destX) || !board.VerifyInsideBoard(_x, _y))
        // {
        //     throw new Exception("Jogada " + _destX + _destY + "está fora do tabuleiro");
        // }
        x = _x;
        y = _y;
        destX = _destX;
        destY = _destY;
        score = _score;
        attack = true;
    }
    static public Move Fake(){
        return new Move(0,0,0,0,-9999);
    }
}

public class Piece {
    public int enabled = 1;
    public int type;
    public int team;
    public int x;
    public int y;
    public bool move = false;
    public Piece(int _type, int _team, int _x, int _y){
        type = _type;
        team = _team;
        x = _x;
        y = _y;
    }
	public int TypeToScore(){
    	return this.type+1;
	}
    public Move[] Movement(Board board){
        switch(type){
            case 0: return this.pawn(board); // Peao
            case 1: return this.L(board); // Cavalo
            case 2: return this.cross(board); // Bispo
            case 3: return this.Plus(board); // Castelo   
            case 4: return this.Queen(board); // Rainha    
        }
        return this.adj(board); // Rei
    }
    
    public Move[] Queen(Board board){
        Move[] x = this.cross(board);
        Move[] y = this.Plus(board);
        Move[] z = new Move[x.Length + y.Length];
        x.CopyTo(z, 0);
        y.CopyTo(z, x.Length);
        return z;
    }
    // 4 straight lines
    public Move[] Plus(Board board){
		Piece piece = this;
        List<Move> moves = new List<Move>();
        int x = piece.x;
        int y = piece.y;
        for(int i = x + 1; i < 8; i++){
            if (!board.VerifyInsideBoard(i, y)) continue;
            if(board.GetPiece(i, y) == null){
                moves.Add(new Move(x,y, i,y));
            }else{
                if(board.GetPiece(i, y).team != piece.team){
                    moves.Add(new Move(x,y, i,y, (board.GetPiece(i, y)).TypeToScore()));
                }
                break;
            }
        }
  
        for(int i = x - 1; i >= 0; i--){
            if (!board.VerifyInsideBoard(i, y)) continue;
            if(board.GetPiece(i, y) == null){
                moves.Add(new Move(x,y, i,y));
            }else{
                if(board.GetPiece(i, y).team != piece.team){
                    moves.Add(new Move(x,y, i,y, (board.GetPiece(i, y)).TypeToScore()));
                }
                break;
            }
        }
  
        for(int i = y + 1; i < 8; i++){
            if (!board.VerifyInsideBoard(x, i)) continue;
            if(board.GetPiece(x, i) == null){
                moves.Add(new Move(x,y, x,i));
            }else{
                if(board.GetPiece(x, i).team != piece.team){
                    moves.Add(new Move(x,y, x,i, (board.GetPiece(x, i)).TypeToScore()));
                }
                break;
            }
        }
  
        for(int i = y - 1; i >= 0; i--){
            if (!board.VerifyInsideBoard(x, i)) continue;
            if(board.GetPiece(x, i) == null){
                moves.Add(new Move(x,y, x,i));
            }else{
                if(board.GetPiece(x, i).team != piece.team){
                    moves.Add(new Move(x,y, x,i, (board.GetPiece(x, i)).TypeToScore()));
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
            if (!board.VerifyInsideBoard(x + i, y + i)) continue;
            if(board.GetPiece(x+i, y+i) == null){
                moves.Add(new Move(x,y, x+i, y+i));
            }else{
                if(board.GetPiece(x+i, y+i).team != piece.team){
                    moves.Add(new Move(x,y, x+i,y+i, (board.GetPiece(x+i,y+i)).TypeToScore()));
                }
                break;
            }
        }
 
        for(int i = -1; (i+x >= 0) && (i+y >= 0); i--){
            if (!board.VerifyInsideBoard(x + i, y + i)) continue;
            if(board.GetPiece(x+i, y+i) == null){
                moves.Add(new Move(x,y, x+i, y+i));
            }else{
                if(board.GetPiece(x+i, y+i).team != piece.team){
                    moves.Add(new Move(x,y, x+i,y+i, (board.GetPiece(x+i,y+i)).TypeToScore()));
                }
                break;
            }
        }
 
        for(int i = 1; (i+x < 8) && (i-y >= 0); i++){
            if (!board.VerifyInsideBoard(x + i, i - y)) continue;
            if(board.GetPiece(x+i, i-y) == null){
                moves.Add(new Move(x,y, x+i, i-y));
            }else{
                if(board.GetPiece(x+i, i-y).team != piece.team){
                    moves.Add(new Move(x,y, x+i,i-y, (board.GetPiece(x+i,i-y)).TypeToScore()));
                }
                break;
            }
        }
 
        for(int i = 1; (i-x >= 0) && (i+y < 8); i++){
            if (!board.VerifyInsideBoard(i - x, y + i)) continue;
            if(board.GetPiece(i-x, y+i) == null){
                moves.Add(new Move(x,y, i-x, y+i));
            }else{
                if(board.GetPiece(i-x, y+i).team != piece.team){
                    moves.Add(new Move(x,y, i-x,y+i, (board.GetPiece(i-x,y+i)).TypeToScore()));
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
            if (!board.VerifyInsideBoard(piece.x + i, piece.y)) continue;
            target = board.GetPiece(x+i, y);
			if(target == null){
				moves.Add(new Move(x,y, x+i, y));
			}else if(target.team != piece.team){
				moves.Add(new Move(x,y, x+i,y, target.TypeToScore()));
			}
		}
		for(int i = -1; i <= 1; i+=2){
            if (!board.VerifyInsideBoard(piece.x, piece.y + i)) continue;
            target = board.GetPiece(x, y+i);
			if(target == null){
				moves.Add(new Move(x,y, x,y+i));
			}else if(target.team != piece.team){
				moves.Add(new Move(x,y, x,y+i, target.TypeToScore()));
			}
		}
		
		for(int i = -1; i <= 1; i+=2){
			for(int z = -1; z <= 1; z+=2){
                if (!board.VerifyInsideBoard(piece.x + i, piece.y + z)) continue;
                target = board.GetPiece(x+i, y+z);
				if(target == null){
					moves.Add(new Move(x,y, x+i, y+z));
				}else if(target.team != piece.team){
					moves.Add(new Move(x,y, x+i, y+z, target.TypeToScore()));
				}
			}
		}

        if (!piece.move)
        {
            if (piece.team == 0)
            {
                if (board.GetPiece(0,0) != null && !board.GetPiece(0, 0).move && VerifyRoque(board, 0))
                {
                    moves.Add(new Move(x,y, 0,0, roque: true));
                } 
                if (board.GetPiece(7, 0) != null && !board.GetPiece(7, 0).move && VerifyRoque(board, 7))
                {
                    moves.Add(new Move(x,y, 0,0, roque: true));
                }
            }
            else
            {
                if (board.GetPiece(0,7) != null && !board.GetPiece(0, 7).move && VerifyRoque(board, 0))
                {
                    moves.Add(new Move(x,y, 0,7, roque: true));
                } 
                if (board.GetPiece(7,7) != null && !board.GetPiece(7, 7).move && VerifyRoque(board, 7))
                {
                    moves.Add(new Move(x,y, 7,7, roque: true));
                } 
            }
        }
        return moves.ToArray();
	}

    public bool VerifyRoque(Board board, int x)
    {
        if (this.x > x)
        {
            for (int i = this.x - 1; i > x; i--)
                if (board.GetPiece(i, this.y) != null) return false;

            return true;
        }
        else
        {
            for (int i = this.x + 1; i < x; i++)
                if (board.GetPiece(i, this.y) != null) return false;

            return true;
        }
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
        if(board.VerifyInsideBoard(piece.x, piece.y + delta))
        {
            if (board.GetPiece(piece.x, piece.y + delta) == null)
            {
                moves.Add(new Move(piece.x, piece.y, piece.x, piece.y + delta));
                if (firstMove == 1)
                {
                    if (board.GetPiece(piece.x, piece.y + delta * 2) == null)
                    {
                        // Só vale pra casa inicial
                        moves.Add(new Move(piece.x, piece.y, piece.x, piece.y + delta*2));
                    }
                }
            }
        }
        for(int ii = -1; ii <= 1; ii+=2)
        {
            if (!board.VerifyInsideBoard(piece.x + ii, piece.y + delta)) continue; 
            target = board.GetPiece(piece.x + ii, piece.y + delta);
            if(target != null && target.team != this.team){
                moves.Add(new Move(piece.x, piece.y, piece.x + ii, piece.y + delta, target.TypeToScore()));
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
        for(int i = -1; i <= 1; i+=2){ // Invert Y
            for(int z = 0; z <= 1; z++){ // Change x and y sizes
                for(int w = -1; w <= 1; w+=2){ // Invert x
                    x = (1+z)*w;
                    y = (2-z)*i;
                    if (!board.VerifyInsideBoard(piece.x + x, piece.y + y)) continue;
                    target = board.GetPiece(piece.x + x,piece.y + y);
                    if(target == null){
                        moves.Add(new Move(piece.x,piece.y, piece.x+x, piece.y+y));
                    }else if(target.team != piece.team){
                        moves.Add(new Move(piece.x,piece.y, piece.x+x, piece.y+y, target.TypeToScore()));
                    }
                }
            }
        }
        return moves.ToArray();
    }
}
public class Board {
    public Piece[,] positions = new Piece[8,8];
    public List<Piece> wPieces = new();
    public List<Piece> bPieces = new();
    public void AddPiece(int type, int team, int x, int y){
        Piece p = new Piece(type, team, x, y);
        positions[x, y]  = p;
        if(team == 0) { 
            wPieces.Add(p);
        }else {
            bPieces.Add(p);
        }
    }
    public void AddPiece(Piece p){
        positions[p.x, p.y]  = p;
        if(p.team == 0) { 
            wPieces.Add(p);
        }else {
            bPieces.Add(p);
        }
    }

    public void RemovePiece(Piece p)
    {
        positions[p.x, p.y] = null;
        if (p.team == 0)
        {
            wPieces.Remove(p);
        }
        else
        {
            bPieces.Remove(p);
        }
    }
    
    public Piece[] GetPieces(int turn) {
        List<Piece> resp = new List<Piece>();
        if(turn == 0){
            foreach (var p in wPieces)
            {
                if(p != null && p.enabled == 1){resp.Add(p);}
            }
            
        }else{
            foreach (var p in bPieces)
            {
                if(p != null && p.enabled == 1){resp.Add(p);}
            }
        }
        return resp.ToArray();
    }
    public Piece GetPiece(int x, int y){
        return this.positions[x,y];
    }
    public void SetPiece(int x, int y, Piece p){
        this.positions[x,y] = p;
    }

    public bool VerifyInsideBoard(int x, int y)
    {
        if ((x < 0 || x > 7) || (y < 0 || y > 7))
            return false;
        return true;
    }
    public void _move(Move _move){
        if (_move.roque)
            RoqueMove(_move.x, _move.y, _move.destX, _move.destY);
        else
            Move(_move.x, _move.y, _move.destX, _move.destY);
    }
    public void _rMove(Move _move)
    {
        if (_move.roque)
            RoqueRMove(_move.x, _move.y, _move.destX, _move.destY);
        else
            Move(_move.destX, _move.destY, _move.x, _move.y);
    }

    private void RoqueRMove(int x, int y, int xd, int yd)
    {
        if (x == 0)
        {
            positions[2, y].x = x;
            positions[x, y] = this.positions[2, y];
            positions[3, yd].x = xd;
            positions[xd, yd] = this.positions[3, yd];
            positions[2, y] = null;
            positions[3, yd] = null;
        }
        else
        {
            positions[6, y].x = x;
            positions[x, y] = this.positions[6, y];
            positions[5, yd].x = xd;
            positions[xd, yd] = this.positions[5, yd];
            positions[6, y] = null;
            positions[5, yd] = null;
        }
    }

    public void Move(int x, int y, int xd, int yd){
        positions[x,y].x = xd;
        positions[x,y].y = yd;
        positions[xd,yd] = this.positions[x,y];
        positions[xd,yd].move = true;
        positions[x,y] = null;
    }

    public void RoqueMove(int x, int y, int xd, int yd)
    {
        if(positions[x,y].x == 0)
        {
            positions[x, y].x = 2;
            positions[2, y] = this.positions[x, y];
            positions[xd, yd].x = 3;
            positions[3, y] = this.positions[xd, yd];
            positions[x, y] = null;
            positions[xd, yd] = null;
        }
        else
        {
            positions[x, y].x = 6;
            positions[6, y] = this.positions[x, y];
            positions[xd, yd].x = 5;
            positions[5, y] = this.positions[xd, yd];
            positions[x, y] = null;
            positions[xd, yd] = null;
        }
    }
}

public class AI{
    public static Move BestChoice(Board board, int turn, int depth){
        return _bestChoice(board, turn, depth, turn, -1, 9999);
    }

    private static Move _bestChoice(Board board, int turn, int depth, int maxmizeTurn, int alpha, int beta){        
        Piece _p;
        Move bestMove = Move.Fake();
        foreach(var p in board.GetPieces(turn)){
            foreach(var m in p.Movement(board)){
                // Console.WriteLine("T"+turn +" - "+ m.x +" "+ m.y + " -> " + m.destX +" "+ m.destY +" $"+ m.score);
                if(depth > 0){
                    _p = board.GetPiece(m.destX, m.destY);
                    if(_p != null) board.RemovePiece(_p);
                    board._move(m); // Possivelmente parte de um bug (1)
                    m.score += _bestChoice(board, (turn+1)%2, depth-1, maxmizeTurn, alpha, beta).score; // Recursive Score
                    board._rMove(m); // Possivelmente parte de um bug (2)
                    board.SetPiece(m.destX, m.destY, _p);
                    if(_p != null) board.AddPiece(_p);
                }
                if(m.score > bestMove.score){ // Max(this, last)
                    bestMove = m;
                    // if(turn == maxmizeTurn){
                    //     if(m.score > alpha){
                    //         alpha = m.score;
                    //         if(beta <= alpha){
                    //             return Move.fake(); // Return doesn't matter here
                    //         }
                    //     }
                    // }else{
                    //     if(m.score > beta){
                    //         beta = -m.score;
                    //         if(beta <= alpha){
                    //             return Move.fake(); // Return doesn't matter here
                    //         }
                    //     }
                    // }
                }
            }
        }
        return bestMove;
    }

    public static Move RandomChoice(Board board, int turn)
    {
        List<Piece> pieces = new List<Piece>();
        foreach (var piece in board.GetPieces(turn))
        {
            if(piece.Movement(board).Length > 0)
                pieces.Add(piece);
        }
        Random r = new Random();
        int index = r.Next(pieces.Count);
        Move[] movePiece = pieces[index].Movement(board);
        index = r.Next(movePiece.Length);

        return movePiece[index];
    }

    
}