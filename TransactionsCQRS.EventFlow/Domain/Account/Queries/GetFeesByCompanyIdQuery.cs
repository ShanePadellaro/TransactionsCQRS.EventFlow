using System.Collections.Generic;
using EventFlow.Queries;
using TransactionsCQRS.EventFlow.Domain.Account.ReadModels;

namespace TransactionsCQRS.EventFlow.Domain.Account.Queries
{
    public class GetFeesByCompanyIdQuery : IQuery<List<TransactionReadModel>>
    {
        public string CompanyId { get; }

        public GetFeesByCompanyIdQuery(string companyId)
        {
            CompanyId = companyId;
        }
    }
}