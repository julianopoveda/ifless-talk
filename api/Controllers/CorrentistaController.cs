using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CorrentistaController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public CorrentistaController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Save(CorrentistaDTO correntista)
        {
            if(!correntista.IsValid())
                return BadRequest("Campos obrigatórios não enviados");

            if(correntista.ID == 0)
            {                
                Correntista novoCorrentista = Correntista.NovaCorrentista(correntista.CPF, correntista.Nome, correntista.Endereco);
            }
            else 
            {                
            }
            return Ok();
        }
    }
}
