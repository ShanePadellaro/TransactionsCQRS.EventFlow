﻿using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.Commands
{
    public class CreateAccountCommandHandler : CommandHandler<AccountAggregate, AccountId,AccountOpenedRecipt ,CreateAccountCommand>
    {
        public override Task<AccountOpenedRecipt> ExecuteCommandAsync(AccountAggregate aggregate, CreateAccountCommand command, CancellationToken cancellationToken)
        {
            var result= aggregate.OpenAccount(command.AccountDetails);
            return Task.FromResult(result);        
        }
    }
}