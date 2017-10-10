using Evitando.Duplicacao.de.Codigo.Exercicios;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Evitando.Duplicacao.de.Codigo.Tests.Exercicios
{
    [TestFixture]
    public class InformacoesContatoTest
    {
        private InformacoesContato _informacoesContato;

        [SetUp]
        public void Setup()
        {
            _informacoesContato = new InformacoesContato();
        }

        private ContatoDto ObterDadosContato(string email, string endereco, string telefone)
        {
            return new ContatoDto()
            {
                Email = email,
                Endereco = endereco,
                Telefone = telefone
            };
        }

        [Test]
        [TestCase("", "", "")]
        [TestCase("email@email.com", "", "")]
        [TestCase("email@email.com", "Rua A", "")]
        [TestCase("email@email.com", "Avenida X", "44 9 9999-9999")]
        [TestCase(null, null, null)]
        public void Deve_obter_informacoes(string email, string endereco, string telefone)
        {
            var dadosUsuarioContrato = Substitute.For<IDadosContato>();
            dadosUsuarioContrato.Email = email;
            dadosUsuarioContrato.Endereco = endereco;
            dadosUsuarioContrato.Telefone = telefone;
            var contatoDTO = _informacoesContato.ObterContato(dadosUsuarioContrato);
            var dtoEsperado = ObterDadosContato(email, endereco, telefone);
            contatoDTO.ShouldBeEquivalentTo(dtoEsperado);
        }

        [Test]
        [TestCase("", "", "")]
        [TestCase("email@email.com", "", "")]
        [TestCase("email@email.com", "Rua A", "")]
        [TestCase("email@email.com", "Avenida X", "44 9 9999-9999")]
        [TestCase(null, null, null)]
        public void Deve_obter_informacoes_prestador(string email, string endereco, string telefone)
        {
            var dadosUsuarioContrato = Substitute.For<Prestador>();
            dadosUsuarioContrato.Email = email;
            dadosUsuarioContrato.Endereco = endereco;
            dadosUsuarioContrato.Telefone = telefone;
            var contatoDTO = _informacoesContato.ObterContato(dadosUsuarioContrato);
            var dtoEsperado = ObterDadosContato(email, endereco, telefone);
            contatoDTO.ShouldBeEquivalentTo(dtoEsperado);
        }

        [Test]
        [TestCase("", "", "")]
        [TestCase("email@email.com", "", "")]
        [TestCase("email@email.com", "Rua A", "")]
        [TestCase("email@email.com", "Avenida X", "44 9 9999-9999")]
        [TestCase(null, null, null)]
        public void Deve_obter_informacoes_beneficiario(string email, string endereco, string telefone)
        {
            var dadosUsuarioContrato = Substitute.For<Beneficiario>();
            dadosUsuarioContrato.Email = email;
            dadosUsuarioContrato.Endereco = endereco;
            dadosUsuarioContrato.Telefone = telefone;
            var contatoDTO = _informacoesContato.ObterContato(dadosUsuarioContrato);
            var dtoEsperado = ObterDadosContato(email, endereco, telefone);
            contatoDTO.ShouldBeEquivalentTo(dtoEsperado);
        }

        [Test]
        [TestCase("", "", "")]
        [TestCase("email@email.com", "", "")]
        [TestCase("email@email.com", "Rua A", "")]
        [TestCase("email@email.com", "Avenida X", "44 9 9999-9999")]
        [TestCase(null, null, null)]
        public void Deve_obter_informacoes_contratante(string email, string endereco, string telefone)
        {
            var dadosUsuarioContrato = Substitute.For<Contratante>();
            dadosUsuarioContrato.Email = email;
            dadosUsuarioContrato.Endereco = endereco;
            dadosUsuarioContrato.Telefone = telefone;
            var contatoDTO = _informacoesContato.ObterContato(dadosUsuarioContrato);
            var dtoEsperado = ObterDadosContato(email, endereco, telefone);
            contatoDTO.ShouldBeEquivalentTo(dtoEsperado);
        }
    }
}
