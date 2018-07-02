using System;
using ATMMachine.Entities;

namespace ATMMachine.Interfaces
{
    public interface IReadOnlyCashTransaction
    {
        int TotalAmount { get; }
        int BillCount(UnitedStatesTender tender);
    }
}
