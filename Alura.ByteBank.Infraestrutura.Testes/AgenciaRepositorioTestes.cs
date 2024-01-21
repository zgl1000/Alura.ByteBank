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
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;

        public AgenciaRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaObterTodasAgencia()
        {
            //Arrange
            //Act
            List<Agencia> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
            Assert.Equal(4, lista.Count);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange
            //Act
            var agencia = _repositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(agencia);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciaPorVariosId(int id)
        {
            //Arrange
            //Act
            var agencia = _repositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(agencia);
        }

        [Fact]
        public void TestaInsereUmaNovaAgenciaNoBancoDeDados()
        {
            //Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Guarapari",
                Identificador = Guid.NewGuid(),
                Endereco = "Rua 7 de Setembro - Centro",
                Numero = 125982
            };

            //Act
            var retorno = _repositorio.Adicionar(agencia);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaAtualizaInformacaoDeterminadaAgencia()
        {
            //Arrange
            var agencia = _repositorio.ObterPorId(2);
            var nomeNovo = "Agência Nova";
            agencia.Nome = nomeNovo;

            //Act
            var atualizado = _repositorio.Atualizar(2, agencia);

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaRemoverInformacaoDeterminadaAgencia()
        {
            //Arrange
            //Act
            var excluido = _repositorio.Excluir(3);

            //Assert
            Assert.True(excluido);
        }

        //Exceções
        [Fact]
        public void TestaExcecaoConsultaAgenciaPorId()
        {
            //Act
            //Assert
            Assert.Throws<FormatException>(
                () => _repositorio.ObterPorId(33)
            );
        }
    }
}
