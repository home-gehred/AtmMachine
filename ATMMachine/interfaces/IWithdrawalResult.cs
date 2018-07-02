using System;
namespace ATMMachine.Interfaces
{
    public interface IWithdrawalResult
    {
        bool IsSuccess { get; }
        string FailureReason { get; }
        IReadOnlyCashTransaction Details { get; }
    }
}
