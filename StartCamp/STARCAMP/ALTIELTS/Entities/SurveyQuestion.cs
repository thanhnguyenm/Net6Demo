using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.Entities
{
    public class SurveyQuestion
    {
        private readonly ILazyLoader _lazyLoader;

        public SurveyQuestion()
        {
        }
        public SurveyQuestion(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public int ServiceId { get; set; }

        private Service _service;
        public Service Service
        {
            get => _lazyLoader.Load(this, ref _service);
            set => _service = value;
        }

        private List<RatingResult> _ratingResults;
        public List<RatingResult> RatingResults
        {
            get => _lazyLoader.Load(this, ref _ratingResults);
            set => _ratingResults = value;
        }
    }

}
