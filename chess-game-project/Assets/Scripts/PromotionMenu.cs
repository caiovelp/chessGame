//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PromotionMenu : MonoBehaviour
//{
//    public GameObject promotionMenu;
//    //public Button queenButton;
//    //public Button rookButton;
//    //public Button bishopButton;
//    //public Button knightButton;

//    private Piece promotedPiece;

//    public void ShowPromotionMenu(Piece piece)
//    {
//        promotedPiece = piece;
//        promotionMenu.SetActive(true);
//    }

//    public void HidePromotionMenu()
//    {
//        promotionMenu.SetActive(false);
//    }

//    public void PromoteToQueen()
//    {
//        promotedPiece.PromoteTo(PieceType.Queen);
//        HidePromotionMenu();
//    }

//    public void PromoteToRook()
//    {
//        promotedPiece.PromoteTo(PieceType.Rook);
//        HidePromotionMenu();
//    }

//    public void PromoteToBishop()
//    {
//        promotedPiece.PromoteTo(PieceType.Bishop);
//        HidePromotionMenu();
//    }

//    public void PromoteToKnight()
//    {
//        promotedPiece.PromoteTo(PieceType.Knight);
//        HidePromotionMenu();
//    }
//}