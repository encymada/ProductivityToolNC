namespace ProductivityToolModels
{
    public class ProdToolModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public ProdToolModels(string name, string description)
        {
            Name = name.ToUpper();
            Description = description.ToUpper();
            Status = "PENDING";
        }
    }
}