using System;
using System.Collections.Generic;

namespace Carbonara.Models.Calculation
{
    public class TotalCalculationResult
    {
        public Dictionary<int, CalculationResult> CalculationPerYear;
        public List<Country.Country> AverageCo2EmissionPerCountryInKg;
        public List<KeyValuePair<string, DateTime>> TransactionDates; 
        public TotalCalculationResult()
        {
            if (CalculationPerYear == null)
                CalculationPerYear = new Dictionary<int, CalculationResult>();

            if (TransactionDates == null)
                TransactionDates = new List<KeyValuePair<string, DateTime>>();
        }
    }
}



