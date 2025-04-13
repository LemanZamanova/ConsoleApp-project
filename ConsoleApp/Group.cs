namespace ConsoleApp
{
    internal class Group
    {
        public string No { get; set; }
        public string Category { get; set; }
        public bool IsOnline { get; set; }
        public int Limit
        {
            get
            {
                if (IsOnline)
                    return 15;
                else return 10;
            }

        }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
