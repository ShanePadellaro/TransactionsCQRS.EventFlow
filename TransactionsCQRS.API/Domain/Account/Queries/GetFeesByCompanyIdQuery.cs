using System.Collections.Generic;
using EventFlow.Queries;
using TransactionsCQRS.API.Domain.Account.ReadModels;

namespace TransactionsCQRS.API.Domain.Account.Queries
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