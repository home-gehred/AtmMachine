using System;
using ATMMachine.interfaces;

namespace ATMMachine.Entities
{
    public class WithdrawalResult : IWithdrawalResult
    {
        private bool _isSuccess;
        private string _failureReason;
        private IReadOnlyCashTransaction _details;
        private WithdrawalResult(bool isSuccess, string failureReason, IReadOnlyCashTransaction transactionDetails)
        {
            _isSuccess = isSuccess;
            _failureReason = failureReason;
            _details = transactionDetails;
        }

        public static WithdrawalResult CreateSuccessResult(IReadOnlyCashTransaction transactionDetails)
        {
            return new WithdrawalResult(true, string.Empty, transactionDetails);
        }

        public static WithdrawalResult CreateFailureResult(string failureReason, IReadOnlyCashTransaction transactionDetails)
        {
            if (string.IsNullOrWhiteSpace(failureReason))
            {
                throw new ArgumentException("Cannont be blank or empty", nameof(failureReason));
            }
            return new WithdrawalResult(false, failureReason, transactionDetails);
        }

        public bool IsSuccess => _isSuccess;

        public string FailureReason => _failureReason;

        public IReadOnlyCashTransaction Details => _details;
    }
}
