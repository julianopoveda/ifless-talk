using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public ContaController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/accountnumber")]
        public decimal Post([FromQuery] int accountnumber, [FromBody] DepositoDTO deposito, [FromServices] ICorrentistaRepository repository)
        {
            Conta conta = repository.GetAccountById(accountnumber);
            decimal saldo = conta.EfetuarDeposito(deposito.DataDeposito, deposito.Valor);
            return saldo;
        }
    }
}
