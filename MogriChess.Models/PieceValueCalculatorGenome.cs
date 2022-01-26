namespace MogriChess.Models
{
    public class PieceValueCalculatorGenome
    {
        public int ForwardOne { get; }
        public int ForwardTwo { get; }
        public int ForwardInfinite { get; }
        public int ForwardRightOne { get; }
        public int ForwardRightTwo { get; }
        public int ForwardRightInfinite { get; }
        public int RightOne { get; }
        public int RightTwo { get; }
        public int RightInfinite { get; }
        public int BackRightOne { get; }
        public int BackRightTwo { get; }
        public int BackRightInfinite { get; }
        public int BackOne { get; }
        public int BackTwo { get; }
        public int BackInfinite { get; }
        public int BackLeftOne { get; }
        public int BackLeftTwo { get; }
        public int BackLeftInfinite { get; }
        public int LeftOne { get; }
        public int LeftTwo { get; }
        public int LeftInfinite { get; }
        public int ForwardLeftOne { get; }
        public int ForwardLeftTwo { get; }
        public int ForwardLeftInfinite { get; }

        public PieceValueCalculatorGenome(
            int forwardOne, int forwardTwo, int forwardInfinite,
            int forwardRightOne, int forwardRightTwo, int forwardRightInfinite,
            int rightOne, int rightTwo, int rightInfinite,
            int backRightOne, int backRightTwo, int backRightInfinite,
            int backOne, int backTwo, int backInfinite,
            int backLeftOne, int backLeftTwo, int backLeftInfinite,
            int leftOne, int leftTwo, int leftInfinite,
            int forwardLeftOne, int forwardLeftTwo, int forwardLeftInfinite)
        {
            ForwardOne = forwardOne;
            ForwardTwo = forwardTwo;
            ForwardInfinite = forwardInfinite;

            ForwardRightOne = forwardRightOne;
            ForwardRightTwo = forwardRightTwo;
            ForwardRightInfinite = forwardRightInfinite;

            RightOne = rightOne;
            RightTwo = rightTwo;
            RightInfinite = rightInfinite;

            BackRightOne = backRightOne;
            BackRightTwo = backRightTwo;
            BackRightInfinite = backRightInfinite;

            BackOne = backOne;
            BackTwo = backTwo;
            BackInfinite = backInfinite;

            BackLeftOne = backLeftOne;
            BackLeftTwo = backLeftTwo;
            BackLeftInfinite = backLeftInfinite;

            LeftOne = leftOne;
            LeftTwo = leftTwo;
            LeftInfinite = leftInfinite;

            ForwardLeftOne = forwardLeftOne;
            ForwardLeftTwo = forwardLeftTwo;
            ForwardLeftInfinite = forwardLeftInfinite;
        }
    }
}