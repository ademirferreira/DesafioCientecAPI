using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DesafioCientec.API.ViewModels;
using DesafioCientec.Business.Interfaces;
using DesafioCientec.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCientec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundacaoController : MainController
    {
        private readonly IFundacaoRepository _fundacaoRepository;
        private readonly IFundacaoService _fundacaoService;
        private readonly IMapper _mapper;
        public FundacaoController(IFundacaoRepository fundacaoRepository,
                                    IFundacaoService fundacaoService,
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _fundacaoRepository = fundacaoRepository;
            _fundacaoService = fundacaoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<FundacaoViewModel>> ObterTodos()
        {
            var fundacao = _mapper
                .Map<IEnumerable<FundacaoViewModel>>(await _fundacaoRepository.ObterTodos());
            return fundacao;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FundacaoViewModel>> ObterPorId(Guid id)
        {
            var fundacao = _mapper.Map<FundacaoViewModel>( await _fundacaoRepository.ObterPorId(id));
            if (fundacao is null) return NotFound();
            return fundacao;
        }

        [HttpGet("{documento}")]
        public async Task<ActionResult<FundacaoViewModel>> Buscar(string documento)
        {
            var fundacao =
                _mapper.Map<FundacaoViewModel>(await _fundacaoRepository.BuscarPorDocumento(documento));
            if (fundacao is null) return NotFound();

            return fundacao;
        }

        [HttpPost]
        public async Task<ActionResult<FundacaoViewModel>> Adicionar(FundacaoViewModel fundacaoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _fundacaoService.Adicionar(_mapper.Map<Fundacao>(fundacaoViewModel));

            return CustomResponse(fundacaoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FundacaoViewModel>> Atualizar(Guid id, FundacaoViewModel fundacaoViewModel)
        {
            if (id != fundacaoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(fundacaoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fundacaoService.Atualizar(_mapper.Map<Fundacao>(fundacaoViewModel));
            return CustomResponse(fundacaoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FundacaoViewModel>> Excluir(Guid id)
        {
            await _fundacaoService.Remover(id);
            return CustomResponse();
        }
    }
}
