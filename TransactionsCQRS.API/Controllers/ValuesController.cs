using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using TransactionsCQRS.API.Domain.Account.Commands;
using TransactionsCQRS.API.Domain.Account.Queries;
using TransactionsCQRS.API.Domain.Account.ValueObjects;
using KeyValuePair = TransactionsCQRS.API.Domain.Account.ValueObjects.KeyValuePair;

namespace TransactionsCQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public ValuesController(ICommandBus commandBus,IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id2)
        {
            
            var accountDetails = new AccountDetails("ExternalId","Name", "GBR", "GBP", 50);
            var recipt = await _commandBus.PublishAsync(new CreateAccountCommand(accountDetails),
                CancellationToken.None);

//            var id = new AccountId("account-b71862d8-5972-4359-87c3-b7c8d0f06dbb");
            var id = recipt.Id;
//            var props = new List<Dictionary<string, object>>();
//            props.Add(new Dictionary<string, object>() {{"item1", "value1"}});
//            props.Add(new Dictionary<string, object>() {{"item2", "value2"}});
            
            var subfee1 = new KeyValuePair("myKey","MyValue");
            var companyId = "b3e4bf26-c93b-41f6-adf1-27b85fa82c91";
            var subfee2 = new Fee(companyId, "MyLabel", "USD", "0.9", 0, 0);
            var subfees = new List<Fee>(){subfee2};
            var keyValueParis = new List<KeyValuePair>(){subfee1};
            var item = new TransactionItem(100,"B2C Renewal",1,keyValueParis,subfees);
            
            var transaction = new Transaction("T-00001",id.ToString(),"Transaction","B2C Renewal",100,0,DateTime.Now, 20,"GBR","GBP",new List<TransactionItem>(){item});
            
            var result = await _commandBus.PublishAsync(new CreditAccountCommand(id,transaction),
                CancellationToken.None);
            
            
            var accountReadModel = await _queryProcessor.ProcessAsync(
                    new GetAccountByIdQuery(id), 
                    CancellationToken.None)
                .ConfigureAwait(false);
            
            var transactions = await _queryProcessor.ProcessAsync(
                    new GetFeesByCompanyIdQuery("b3e4bf26-c93b-41f6-adf1-27b85fa82c91"), 
                    CancellationToken.None)
                .ConfigureAwait(false);
            
            var taxes = await _queryProcessor.ProcessAsync(
                    new GetTransactionsByCountry("gbr",DateTime.Now.AddDays(-1),DateTime.Now.AddDays(1),skip:1,take:2), 
                    CancellationToken.None)
                .ConfigureAwait(false);
            
            return taxes.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}