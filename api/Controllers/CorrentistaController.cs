using System;
using System.Linq;
using api.Model;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CorrentistaController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICorrentistaRepository _repository;

        public CorrentistaController(ILogger<WeatherForecastController> logger,  [FromServices] ICorrentistaRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Save([FromBody] CorrentistaDTO correntista)
        {
            if (!correntista.IsValid())
                return BadRequest("Campos obrigatórios não enviados");

            if (correntista.ID == 0)
            {
                Correntista novoCorrentista = Correntista.NovaCorrentista(correntista.CPF, correntista.Nome, correntista.Endereco);
                _repository.Insert(novoCorrentista);
            }
            else
            {
                Correntista correntistaAtualizar = _repository.GetByCPF(correntista.CPF);
                if (correntistaAtualizar == null)
                    return NotFound("Correntista não localizado");

                var dadosAtualizado = correntistaAtualizar.AtualizarDadosCadastrais(correntista.ToCorrentista());

                if (dadosAtualizado.erros.Any())
                    return BadRequest(string.Join(";", dadosAtualizado.erros));

                _repository.update(dadosAtualizado.correntista);
            }

            return Ok();
        }

        [HttpPost("novo")]

        public IActionResult Insert(CorrentistaDTO correntista)
        {
            try
            {
                Correntista novoCorrentista = Correntista.NovaCorrentista(correntista.CPF, correntista.Nome, correntista.Endereco);
                _repository.Insert(novoCorrentista);

                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Update(CorrentistaDTO correntista)
        {
            try
            {
                Correntista correntistaAtualizar = _repository.GetByCPF(correntista.CPF);

                var dadosAtualizado = correntistaAtualizar.AtualizarDadosCadastrais(correntista.ToCorrentista());

                if (dadosAtualizado.erros.Any())
                    return BadRequest(string.Join(";", dadosAtualizado.erros));

                _repository.update(dadosAtualizado.correntista);
                return Ok();
            }
            catch (CustomExceptions.RecordNotFoundException)
            {
                return NotFound("Correntista não localizado");
            }
        }
    }
}
