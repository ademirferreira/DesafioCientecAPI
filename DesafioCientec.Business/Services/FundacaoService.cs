using System;
using System.Linq;
using System.Threading.Tasks;
using DesafioCientec.Business.Interfaces;
using DesafioCientec.Business.Models;
using DesafioCientec.Business.Models.Validations;

namespace DesafioCientec.Business.Services
{
    public class FundacaoService : BaseService, IFundacaoService
    {
        private readonly IFundacaoRepository _fundacaoRepository;

        public FundacaoService(IFundacaoRepository fundacaoRepository, INotificador notificador) : base(notificador)
        {
            _fundacaoRepository = fundacaoRepository;
        }

        public async Task<bool> Adicionar(Fundacao fundacao)
        {
            if(!ExecutarValidacao(new FundacaoValidation(), fundacao)) return false;
            if (_fundacaoRepository.Buscar(f => f.Documento == fundacao.Documento).Result.Any())
            {
                Notificar("Já existe uma fundação com esse documento");
                return false;
            }

            await _fundacaoRepository.Adicionar(fundacao);
            return true;
        }

        public async Task<bool> Atualizar(Fundacao fundacao)
        {
            if (!ExecutarValidacao(new FundacaoValidation(), fundacao)) return false;
            if (_fundacaoRepository.Buscar(f => f.Documento == fundacao.Documento && f.Id != fundacao.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return false;
            }
            await _fundacaoRepository.Atualizar(fundacao);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _fundacaoRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _fundacaoRepository?.Dispose();
        }
    }
}