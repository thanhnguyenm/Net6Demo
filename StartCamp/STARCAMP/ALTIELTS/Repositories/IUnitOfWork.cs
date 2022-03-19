using ALTIELTS.Entities;

namespace ALTIELTS.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> User { get; }
        IRepository<RatingResult> RatingResult { get; }
        IRepository<Entities.Service> Service { get; }
        IRepository<SurveyQuestion> SurveyQuestion { get; }
        void Commit();
    }
}
