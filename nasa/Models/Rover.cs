namespace nasa.Models
{
    public class RoverMachine
    {
        public int Id { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public char Heading { get; set; }

    }   

    public class RoverMovements
    {
        public string move { get; set; }
    }

    public class Grid
    {
        public int MaxSize { get; set; }
    }
}
