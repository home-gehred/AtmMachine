using System;
using ATMMachine.Entities;

namespace ATMMachine.interfaces
{
    public interface IReadOnlyCashTransaction
    {
        int TotalAmount { get; }
        int BillCount(UnitedStatesTender tender);
    }
}
