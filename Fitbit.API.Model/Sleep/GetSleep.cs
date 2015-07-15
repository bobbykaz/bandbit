using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Model.Sleep
{
    public class GetSleepResponse
    {
        public List<Sleep> sleep { get; set; }
        public Summary summary { get; set; }
    }
}
