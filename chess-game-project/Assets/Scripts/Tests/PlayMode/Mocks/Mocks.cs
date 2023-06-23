using UnityEngine;

namespace Mocks
{
    public class MocksTest
    {
        public Game game;
        
        public Game StartMock()
        {
            // Create a chess piece prefab as a placeholder for testing
            var gameObject = new GameObject();
            gameObject.AddComponent<Chessman>();
            gameObject.AddComponent<SpriteRenderer>();

            var chessman = gameObject.GetComponent<Chessman>();

            var spriteBlackKnight = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/CavaloPreto-PLastico.png");
            Sprite[] mockBlackKnight = new[] { spriteBlackKnight, spriteBlackKnight, spriteBlackKnight, spriteBlackKnight };
            chessman.blackKnight = mockBlackKnight;

            var spriteBlackKing = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/ReiPreto-PLastico.png");
            Sprite[] mockBlackKing= new[] { spriteBlackKing, spriteBlackKing, spriteBlackKing, spriteBlackKing };
            chessman.blackKing = mockBlackKing;

            var spriteBlackQueen = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/RainhaPreto-PLastico.png");
            Sprite[] mockBlackQueen = new[] { spriteBlackQueen, spriteBlackQueen, spriteBlackQueen, spriteBlackQueen };
            chessman.blackQueen = mockBlackQueen;

            var spriteBlackTower = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/TorrePreto-PLastico.png");
            Sprite[] mockBlackTower = new[] { spriteBlackTower, spriteBlackTower, spriteBlackTower, spriteBlackTower };
            chessman.blackTower = mockBlackTower;

            var spriteBlackBishop = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/BispoPreto-PLastico.png");
            Sprite[] mockBlackBishop = new[] { spriteBlackBishop, spriteBlackBishop, spriteBlackBishop, spriteBlackBishop };
            chessman.blackBishop = mockBlackBishop;

            var spriteBlackPawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/Black/Piece by Piece/New Cut/PeaoPreto-PLastico.png");
            Sprite[] mockBlackPawn = new[] { spriteBlackPawn, spriteBlackPawn, spriteBlackPawn, spriteBlackPawn };
            chessman.blackPawn = mockBlackPawn;

            var spriteWhiteKnight = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/CavaloWhite-PLastico.png");
            Sprite[] mockWhiteKnight = new[] { spriteWhiteKnight, spriteWhiteKnight, spriteWhiteKnight, spriteWhiteKnight };
            chessman.whiteKnight = mockWhiteKnight;

            var spriteWhiteKing = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/ReiWhite-PLastico.png");
            Sprite[] mockWhiteKing= new[] { spriteWhiteKing, spriteWhiteKing, spriteWhiteKing, spriteWhiteKing };
            chessman.whiteKing = mockWhiteKing;

            var spriteWhiteQueen = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/RainhaWhite-PLastico.png");
            Sprite[] mockWhiteQueen = new[] { spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen, spriteWhiteQueen };
            chessman.whiteQueen = mockWhiteQueen;

            var spriteWhiteTower = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/TorreWhite-PLastico.png");
            Sprite[] mockWhiteTower = new[] { spriteWhiteTower, spriteWhiteTower, spriteWhiteTower, spriteWhiteTower };
            chessman.whiteTower = mockWhiteTower;

            var spriteWhiteBishop = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/BispoWhite-PLastico.png");
            Sprite[] mockWhiteBishop = new[] { spriteWhiteBishop, spriteWhiteBishop, spriteWhiteBishop, spriteWhiteBishop };
            chessman.whiteBishop = mockWhiteBishop;

            var spriteWhitePawn = Resources.Load<Sprite>("../../../../Sprite/Pieces/Pieces/White/Piece by Piece/New Cut/PeaoWhite-PLastico.png");
            Sprite[] mockWhitePawn = new[] { spriteWhitePawn, spriteWhitePawn, spriteWhitePawn, spriteWhitePawn };
            chessman.whitePawn = mockWhitePawn;

            // Act
            game.chesspiece = gameObject;
            
            return game;
        }
    }   
}