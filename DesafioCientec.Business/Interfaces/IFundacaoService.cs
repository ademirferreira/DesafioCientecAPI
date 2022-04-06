using System;
using System.Threading.Tasks;
using DesafioCientec.Business.Models;

namespace DesafioCientec.Business.Interfaces
{
    public interface IFundacaoService
    {
        Task<bool> Adicionar(Fundacao fundacao);
        Task<bool> Atualizar(Fundacao fundacao);
        Task<bool> Remover(Guid id);
    }
}