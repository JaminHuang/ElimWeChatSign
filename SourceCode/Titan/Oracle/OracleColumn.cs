namespace Titan.Oracle
{
    public class OracleColumn : ColumnBase
    { 
        public bool HasSequence { get; private set; }

        private string _sequence; 
        public string Sequence
        {
            get { return _sequence; }
            set { _sequence = value; HasSequence = !string.IsNullOrWhiteSpace(_sequence); }
        } 
    
    }
}
