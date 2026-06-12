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
    public class PTTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CategoryId { get; set; }
        public PTCategory? Category { get; set; }
    }

    public class PTCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PTTask> Tasks { get; set; } = new List<PTTask>();
    }
}
