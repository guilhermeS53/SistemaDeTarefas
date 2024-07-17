﻿using SistemaDeTarefas.Integracao.Interfaces;
using SistemaDeTarefas.Integracao.Refit;
using SistemaDeTarefas.Integracao.Response;

namespace SistemaDeTarefas.Integracao
{
    public class viaCepIntegracao : IViaCepIntegracao
    {
        private readonly IViaCepIntegracaoRefit _viaCepIntegracaoRefit;
        public viaCepIntegracao(IViaCepIntegracaoRefit viaCepIntegracaoRefit)
        {
            _viaCepIntegracaoRefit = viaCepIntegracaoRefit;   
        }

    public async Task<ViaCepResponse> ObterDadosViaCep(string cep)
        {
           var responseData = await _viaCepIntegracaoRefit.ObterDadosViaCep(cep);

            if (responseData != null && responseData.IsSuccessStatusCode)
            {
                return responseData.Content;
            }

            return null;
        }
    }
}
