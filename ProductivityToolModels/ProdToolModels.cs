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
    //public class TaskItem
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public bool IsCompleted { get; set; }
    //}
}