using System;
using InsuranceQuoteCalculator.Models;

namespace InsuranceQuoteCalculator.Services
{
    public class QuoteCalculatorService : IQuoteCalculatorService
    {
        private const decimal BASE_QUOTE = 50m;

        public decimal CalculateQuote(Insuree insuree)
        {
            int age = CalculateAge(insuree.DateOfBirth);
            decimal quote = BASE_QUOTE;

            quote += ApplyAgeRules(age);
            quote += ApplyCarRules(insuree.CarYear, insuree.CarMake, insuree.CarModel);
            quote += ApplyDrivingHistoryRules(insuree.SpeedingTickets, insuree.DUI);
            quote = ApplyCoverageRules(quote, insuree.FullCoverage);

            return quote;
        }

        public int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age--;
            return age;
        }

        public decimal ApplyAgeRules(int age)
        {
            if (age <= 18) return 100m;
            if (age <= 25) return 50m;
            return 25m;
        }

        public decimal ApplyCarRules(int carYear, string carMake, string carModel)
        {
            decimal additional = 0m;

            if (carYear < 2000 || carYear > 2015)
                additional += 25m;

            if (carMake.ToLower() == "porsche")
            {
                additional += 25m;
                if (carModel.ToLower() == "911 carrera")
                    additional += 25m;
            }

            return additional;
        }

        public decimal ApplyDrivingHistoryRules(int speedingTickets, bool hasDUI)
        {
            decimal additional = speedingTickets * 10m;
            if (hasDUI)
                additional *= 1.25m;
            return additional;
        }

        public decimal ApplyCoverageRules(decimal baseQuote, bool isFullCoverage)
        {
            if (isFullCoverage)
                return baseQuote * 1.5m;
            return baseQuote;
        }
    }
}