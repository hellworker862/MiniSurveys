using MiniSurveys.Domain.Modals.Base;

namespace MiniSurveys.Domain.Modals
{
    public class Department : BaseEntity
    {
        public string Name { get; }

        public ICollection<User> Users { get; }
    }
}
