﻿namespace MogriChess.Models
{
    public static class Enums
    {
        public enum ColorType
        {
            Light,
            Dark
        }

        public enum PieceType
        {
            King,
            Pawn,
            Other
        }

        public enum Direction
        {
            Forward = 0,
            ForwardRight = 45,
            Right = 90,
            BackRight = 135,
            Back = 180,
            BackLeft = 225,
            Left = 270,
            ForwardLeft = 315
        }

    }
}