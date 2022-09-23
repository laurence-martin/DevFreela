using DevFreela.Core.Enums;

namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel(
                int id, 
                string title, 
                string description, 
                decimal totalCost, 
                DateTime createAt, 
                DateTime? startedAt, 
                DateTime? finisheddAt, 
                ProjectStatusEnum status,
                string clientFullName,
                string freelancerFullName)
        {
            Id = id;
            Title = title;
            Description = description;
            TotalCost = totalCost;
            CreateAt = createAt;
            StartedAt = startedAt;
            FinishedAt = finisheddAt;
            Status = status;
            ClientFullName = clientFullName;
            FreelancerFullName = freelancerFullName;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public string ClientFullName { get; private set; }  
        public string FreelancerFullName { get; private set; }
    }
}
