using System.ComponentModel;

namespace MogriChess.Models
{
    public class Piece : INotifyPropertyChanged
    {
        private ColorScheme _colorScheme;
        private readonly Enums.PieceType _pieceType;
        private bool _isPromoted;

        public event PropertyChangedEventHandler PropertyChanged;

        public Enums.Color Color { get; }

        public MovementIndicator Forward { get; } =
            new MovementIndicator();
        public MovementIndicator ForwardRight { get; } =
            new MovementIndicator();
        public MovementIndicator Right { get; } =
            new MovementIndicator();
        public MovementIndicator BackRight { get; } =
            new MovementIndicator();
        public MovementIndicator Back { get; } =
            new MovementIndicator();
        public MovementIndicator BackLeft { get; } =
            new MovementIndicator();
        public MovementIndicator Left { get; } =
            new MovementIndicator();
        public MovementIndicator ForwardLeft { get; } =
            new MovementIndicator();

        public bool IsKing => _pieceType.Equals(Enums.PieceType.King);
        public bool IsPawn => _pieceType.Equals(Enums.PieceType.Pawn);
        public bool IsUnpromotedPawn => IsPawn && !_isPromoted;

        public string UiColor =>
            Color == Enums.Color.Light
                ? _colorScheme.LightColor
                : _colorScheme.DarkColor;
        public string KingIndicatorUiColor =>
            IsKing
                ? Color == Enums.Color.Light
                    ? _colorScheme.DarkColor
                    : _colorScheme.LightColor
                : UiColor;
        public int PieceColorTransformAngle =>
            Color == Enums.Color.Light ? 0 : 180;

        public Piece(ColorScheme colorScheme, Enums.Color color,
            Enums.PieceType type,
            int squaresForward, int squaresForwardRight,
            int squaresRight, int squaresBackRight,
            int squaresBack, int squaresBackLeft,
            int squaresLeft, int squaresForwardLeft)
        {
            _colorScheme = colorScheme;
            _pieceType = type;

            Color = color;

            AddMovementAbilities(squaresForward, squaresForwardRight,
                squaresRight, squaresBackRight,
                squaresBack, squaresBackLeft,
                squaresLeft, squaresForwardLeft);
        }

        public void AddMovementAbilities(Piece capturedPiece)
        {
            // Kings do not acquire the movement abilities of pieces they capture
            if (_pieceType == Enums.PieceType.King)
            {
                return;
            }

            AddMovementAbilities(capturedPiece.Forward.Squares, capturedPiece.ForwardRight.Squares,
                capturedPiece.Right.Squares, capturedPiece.BackRight.Squares,
                capturedPiece.Back.Squares, capturedPiece.BackLeft.Squares,
                capturedPiece.Left.Squares, capturedPiece.ForwardLeft.Squares);
        }

        public int MaxMovementSquaresForDirection(Enums.Direction direction)
        {
            return direction switch
            {
                Enums.Direction.Forward => Forward.Squares,
                Enums.Direction.ForwardRight => ForwardRight.Squares,
                Enums.Direction.Right => Right.Squares,
                Enums.Direction.BackRight => BackRight.Squares,
                Enums.Direction.Back => Back.Squares,
                Enums.Direction.BackLeft => BackLeft.Squares,
                Enums.Direction.Left => Left.Squares,
                Enums.Direction.ForwardLeft => ForwardLeft.Squares,
                _ => throw new InvalidEnumArgumentException(
                    "Invalid enum passed to MaxMovementSquaresForDirection() function")
            };
        }

        public (int rankMultiplier, int fileMultiplier) MovementMultipliersForDirection(Enums.Direction direction)
        {
            (int rm, int fm) multipliers = direction switch
            {
                Enums.Direction.Forward => (1, 0),
                Enums.Direction.ForwardRight => (1, 1),
                Enums.Direction.Right => (0, 1),
                Enums.Direction.BackRight => (-1, 1),
                Enums.Direction.Back => (-1, 0),
                Enums.Direction.BackLeft => (-1, -1),
                Enums.Direction.Left => (0, -1),
                Enums.Direction.ForwardLeft => (1, -1),
                _ => throw new InvalidEnumArgumentException(
                    "Invalid direction parameter sent to MovementMultipliersForDirection")
            };

            return Color == Enums.Color.Light
                ? multipliers
                : (-multipliers.rm, -multipliers.fm);
        }

        public void Promote()
        {
            // Pawns that reach opponent's back rank gain ability to move one square in all directions
            if (_pieceType == Enums.PieceType.Pawn)
            {
                AddMovementAbilities(1, 1, 1, 1, 1, 1, 1, 1);

                _isPromoted = true;
            }
        }

        public Piece Clone()
        {
            return new Piece(_colorScheme, Color, _pieceType,
                Forward.Squares, ForwardRight.Squares,
                Right.Squares, BackRight.Squares,
                Back.Squares, BackLeft.Squares,
                Left.Squares, ForwardLeft.Squares);
        }

        private void AddMovementAbilities(int squaresForward, int squaresForwardRight,
            int squaresRight, int squaresBackRight,
            int squaresBack, int squaresBackLeft,
            int squaresLeft, int squaresForwardLeft)
        {
            Forward.MergeMovementAbility(squaresForward);
            ForwardRight.MergeMovementAbility(squaresForwardRight);
            Right.MergeMovementAbility(squaresRight);
            BackRight.MergeMovementAbility(squaresBackRight);
            Back.MergeMovementAbility(squaresBack);
            BackLeft.MergeMovementAbility(squaresBackLeft);
            Left.MergeMovementAbility(squaresLeft);
            ForwardLeft.MergeMovementAbility(squaresForwardLeft);
        }
    }
}