using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repository;
        private string ModelError = string.Empty;
        public bool OperationSuccesful = false;

        #region Construtor

        public PublisherService(IPublisherRepository repositorio)
        {
            _repository = repositorio;
        }

        #endregion

        /// <summary>
        /// Retorna se a operação foi bem sucedida.
        /// </summary>
        /// <returns></returns>
        public bool IsSuccessful()
        {
            return OperationSuccesful;
        }

        /// <summary>
        /// Retorna as mensagens de erro da validação da regra de negócios.
        /// </summary>
        /// <returns></returns>
        public string GetModelErrors()
        {
            return ModelError;
        }

        /// <summary>
        /// Cadastra uma nova editora.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        public async Task<PublisherViewModel> AddPublisher(PublisherViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p=> p.Name == viewModel.Name))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrada_0, "Editora");
                return viewModel;
            }

            #endregion

            var Publisher = viewModel.AutoMapear<PublisherViewModel, Publisher>();
            _repository.Insert(Publisher);
            OperationSuccesful = await _repository.Commit();

            //Recuperando o valor recebido pelo PublisherId.
            viewModel = Publisher.AutoMapear<Publisher, PublisherViewModel>();

            return viewModel;
        }

        /// <summary>
        /// Altera informações de uma editora cadastrada.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        public async Task<PublisherViewModel> UpdatePublisher(PublisherViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Name == viewModel.Name))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outra Editora com este nome.");
                return viewModel;
            }

            #endregion

            var Publisher = viewModel.AutoMapear<PublisherViewModel, Publisher>();
            _repository.Update(Publisher);
            OperationSuccesful = await _repository.Commit();

            return viewModel;
        }

        /// <summary>
        /// Remove uma editora.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        public async Task<bool> DeletePublisher(PublisherViewModel viewModel)
        {
            var Publisher = viewModel.AutoMapear<PublisherViewModel, Publisher>();
            _repository.Delete(Publisher);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta uma editora cadastrado.
        /// </summary>
        /// <param name="PublisherId">Identificador da editora.</param>
        /// <returns></returns>
        public async Task<bool> DeletePublisher(int PublisherId)
        {
            _repository.Delete(p=> p.PublisherId == PublisherId);
            OperationSuccesful = await _repository.Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os editoraes cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PublisherViewModel>> ObtainPublishers()
        {
            var Publishers = await _repository.SelectAll();
            var viewModel = Publishers.AutoMapearLista<Publisher, PublisherViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um editora a partir do seu identificador.
        /// </summary>
        /// <param name="PublisherId">Identificador do editora.</param>
        /// <returns></returns>
        public async Task<PublisherViewModel> ObtainPublisher(int PublisherId)
        {
            var Publishers = await _repository.Select(p=> p.PublisherId == PublisherId);
            var viewModel = Publishers.AutoMapear<Publisher, PublisherViewModel>();
            return viewModel;
        }
    }
}
