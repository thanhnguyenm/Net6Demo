using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace ALTIELTS.Entities
{
    public class User
    {
        private readonly ILazyLoader _lazyLoader;

        public User()
        {
        }
        public User(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public int Id { get; set; }
        public string Passcode { get; set; }
        public string Fullname { get; set; }

        private List<RatingResult> _ratingResults;
        public List<RatingResult> RatingResults
        {
            get => _lazyLoader.Load(this, ref _ratingResults);
            set => _ratingResults = value;
        }
    }
}
