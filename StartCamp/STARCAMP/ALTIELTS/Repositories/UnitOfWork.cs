using ALTIELTS.DatabaseContext;
using ALTIELTS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private StartCampContext _dbContext;
        private IRepository<User> _user;
        private IRepository<RatingResult> _ratingResult;
        private IRepository<Entities.Service> _service;
        private IRepository<SurveyQuestion> _surveyQuestion;

        public UnitOfWork(StartCampContext dbContext,
                            IRepository<User> user,
                            IRepository<RatingResult> ratingResult,
                            IRepository<Entities.Service> service,
                            IRepository<SurveyQuestion> surveyQuestion
                            )
        {
            _dbContext = dbContext;
            _user = user;
            _ratingResult = ratingResult;
            _service = service;
            _surveyQuestion = surveyQuestion;
        }

        public IRepository<User> User
        {
            get
            {
                return _user;
            }
        }

        public IRepository<RatingResult> RatingResult
        {
            get
            {
                return _ratingResult;
            }
        }

        public IRepository<Entities.Service> Service
        {
            get
            {
                return _service;
            }
        }

        public IRepository<SurveyQuestion> SurveyQuestion
        {
            get
            {
                return _surveyQuestion;
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
