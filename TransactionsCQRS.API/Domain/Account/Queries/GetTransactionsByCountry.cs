using System;
using System.Collections.Generic;
using EventFlow.Queries;
using TransactionsCQRS.API.Domain.Account.ReadModels;

namespace TransactionsCQRS.API.Domain.Account.Queries
{
    public class GetTransactionsByCountry:IQuery<IList<TransactionReadModel>>
    {
        public string Countrycode { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public int Take { get; }
        public int Skip { get; set; }

        public GetTransactionsByCountry(string countrycode, DateTime from, DateTime to,int skip, int take)
        {
            Countrycode = countrycode;
            From = @from;
            To = to;
            Skip = skip;
            Take = take;
        }
    }
}