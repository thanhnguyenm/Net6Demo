using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.Entities
{
    public class RatingResult
    {
        private readonly ILazyLoader _lazyLoader;

        public RatingResult()
        {
        }
        public RatingResult(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }

        private User _user;
        public User User
        {
            get => _lazyLoader.Load(this, ref _user);
            set => _user = value;
        }

        private SurveyQuestion _surveyQuestion;
        public SurveyQuestion SurveyQuestion
        {
            get => _lazyLoader.Load(this, ref _surveyQuestion);
            set => _surveyQuestion = value;
        }
    }
}
