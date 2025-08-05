using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.ModelExtensions
{
    public class RankTrackingRequest
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
