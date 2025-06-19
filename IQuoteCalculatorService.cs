using System;
using InsuranceQuoteCalculator.Models;

namespace InsuranceQuoteCalculator.Services
{
    public interface IQuoteCalculatorService
    {
        decimal CalculateQuote(Insuree insuree);
        int CalculateAge(DateTime dateOfBirth);
        decimal ApplyAgeRules(int age);
        decimal ApplyCarRules(int carYear, string carMake, string carModel);
        decimal ApplyDrivingHistoryRules(int speedingTickets, bool hasDUI);
        decimal ApplyCoverageRules(decimal baseQuote, bool isFullCoverage);
    }
}