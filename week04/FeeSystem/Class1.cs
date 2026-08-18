using System;
using System.Collections.Generic;
using System.Linq;

namespace FeeSystem;

public class FeeCalculator
{
    public decimal OutstandingBalance(
        decimal termFee,IEnumerable<decimal> payments)
    {
        if (termFee < 0)
            throw new ArgumentException("Fee cannot be negative");
        var paid = payments.Sum();
        return termFee - paid;
    }

    public bool IsClearedForExams(
        decimal termFee, IEnumerable<decimal> payments)
    {
        var paid = payments.Sum();
        return paid >= termFee / 2;
    }
}