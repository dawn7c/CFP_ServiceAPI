using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TypeOfActivityDto
    {

        public TypeOfActivity ParseValue(string activityString)
        {
            if (Enum.TryParse(typeof(TypeOfActivity), activityString, true, out object activity))
            {
                return (TypeOfActivity)activity;
            }
            else
            {
                
                return TypeOfActivity.Report; 
            }
        }
    }
}
