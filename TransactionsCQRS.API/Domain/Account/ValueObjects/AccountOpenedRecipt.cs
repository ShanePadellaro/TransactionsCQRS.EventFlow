using EventFlow.Aggregates.ExecutionResults;
using EventFlow.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.ValueObjects
{
    public class AccountOpenedRecipt:ValueObject, IExecutionResult
    {
        public string Externalid { get; }
        public AccountId Id { get; }
        
        public AccountOpenedRecipt(AccountDetails accountDetails, AccountId id)
        {
            Id = id;
            Externalid = accountDetails.Externalid;
            IsSuccess = true;
        }

        public bool IsSuccess { get; }
    }
}