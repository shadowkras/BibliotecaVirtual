﻿using BibliotecaVirtual.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public interface IPublisherService
    {
        /// <summary>
        /// Retorna se a operação foi bem sucedida.
        /// </summary>
        /// <returns></returns>
        bool IsSuccessful();

        /// <summary>
        /// Retorna as mensagens de erro da validação da regra de negócios.
        /// </summary>
        /// <returns></returns>
        string GetModelErrors();

        /// <summary>
        /// Cadastra um novo editora.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<PublisherViewModel> AddPublisher(PublisherViewModel viewModel);

        /// <summary>
        /// Altera informações de uma editora cadastrada.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        Task<PublisherViewModel> UpdatePublisher(PublisherViewModel viewModel);

        /// <summary>
        /// Remove uma editora.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        Task<bool> DeletePublisher(PublisherViewModel viewModel);

        /// <summary>
        /// Deleta uma editora cadastrada.
        /// </summary>
        /// <param name="PublisherId">Identificador da editora.</param>
        /// <returns></returns>
        Task<bool> DeletePublisher(int PublisherId);

        /// <summary>
        /// Obtém uma lista com as editoras cadastradas.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PublisherViewModel>> ObtainPublishers();

        /// <summary>
        /// Obtém uma editora a partir do seu identificador.
        /// </summary>
        /// <param name="PublisherId">Identificador da editora.</param>
        /// <returns></returns>
        Task<PublisherViewModel> ObtainPublisher(int PublisherId);
    }
}
