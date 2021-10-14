using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cd_c_weddingPlanner.Models
{
    public class FutureDateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime thisDate = Convert.ToDateTime(value);
            return thisDate <= DateTime.Now;
        }
    }
}