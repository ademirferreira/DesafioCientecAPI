using System;
using System.Linq;
using System.Threading.Tasks;
using DesafioCientec.Business.Interfaces;
using DesafioCientec.Business.Models;

namespace DesafioCientec.Business.Services
{
    public class FundacaoService : IFundacaoService
    {
        private readonly IFundacaoRepository _fundacaoRepository;

        public FundacaoService(IFundacaoRepository fundacaoRepository)
        {
            _fundacaoRepository = fundacaoRepository;
        }

        public async Task<bool> Adicionar(Fundacao fundacao)
        {
            if (_fundacaoRepository.Buscar(f => f.Documento == fundacao.Documento).Result.Any())
            {
                return false;
            }

            await _fundacaoRepository.Adicionar(fundacao);
            return true;
        }

        public async Task<bool> Atualizar(Fundacao fundacao)
        {
            if (_fundacaoRepository.Buscar(f => f.Documento == fundacao.Documento && f.Id != fundacao.Id).Result.Any())
            {
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