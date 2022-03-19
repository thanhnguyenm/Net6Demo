using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.Entities
{
    public class Service
    {
        private readonly ILazyLoader _lazyLoader;

        public Service()
        {
        }
        public Service(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public int Id { get; set; }
        public string Servicename { get; set; }
        public string Icon { get; set; }

        private List<SurveyQuestion> _surveyQuestion;
        public List<SurveyQuestion> SurveyQuestion
        {
            get => _lazyLoader.Load(this, ref _surveyQuestion);
            set => _surveyQuestion = value;
        }
    }
}
