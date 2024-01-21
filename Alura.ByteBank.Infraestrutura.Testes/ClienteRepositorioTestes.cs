using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _repositorio;

        public ClienteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IClienteRepositorio>();
        }

        [Fact]
        public void TestaObterTodosCliente()
        {
            //Arrange
            //Act
            List<Cliente> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
            Assert.Equal(4, lista.Count);
        }

        [Fact]
        public void TestaObterClientePorId()
        {
            //Arrange
            //Act
            var cliente = _repositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(cliente);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterClientePorVariosId(int id)
        {
            //Arrange
            //Act
            var cliente = _repositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(cliente);
        }

        [Fact]
        public void TestaInsereUmNovoClienteNoBancoDeDados()
        {
            //Arrange
            var cliente = new Cliente()
            {
                Nome = "Carlos Silva",
                CPF = "757.778.650-03",
                Identificador = Guid.NewGuid(),
                Profissao = "Pintor"
            };

            //Act
            var retorno = _repositorio.Adicionar(cliente);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaAtualizaInformacaoDeterminadoCliente()
        {
            //Arrange
            var cliente = _repositorio.ObterPorId(1);
            var nomeNovo = "Marcos Guilherme";
            cliente.Nome = nomeNovo;

            //Act
            var atualizado = _repositorio.Atualizar(1, cliente);

            //Assert
            Assert.True(atualizado);
        }
    }
}
