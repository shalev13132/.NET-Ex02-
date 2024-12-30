namespace CheckersGame
{
    public class Move
    {
        public int SourceRow { get; set; }
        public int SourceColumn { get; set; }
        public int DestRow { get; set; }
        public int DestColumn { get; set; }
        public bool IsCapture { get; set; } // מציין אם המהלך כולל אכילה

        public Move(int sourceRow, int sourceColumn, int destRow, int destColumn, bool isCapture = false)
        {
            SourceRow = sourceRow;
            SourceColumn = sourceColumn;
            DestRow = destRow;
            DestColumn = destColumn;
            IsCapture = isCapture;
        }
    }
}