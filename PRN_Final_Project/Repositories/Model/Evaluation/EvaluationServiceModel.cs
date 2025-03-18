using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Model.Evaluation
{
    public class EvaluationServiceModel
    {
        public Guid evaID { get; set; }
        public int point { get; set; }
        public string status { get; set; }
        public Guid accountID { get; set; }
    }
}
