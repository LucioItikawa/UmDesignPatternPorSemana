using Evitando.Duplicacao.de.Codigo.Exercicios;
using Evitando.Duplicacao.de.Codigo.Generics._2.ComGenerics;
using Evitando.Duplicacao.de.Codigo.OO;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Evitando.Duplicacao.de.Codigo.Tests.Exercicios
{
    public class EntidadeMock : Entidade<EntidadeMock>, IDadosDocumento
    {
        public string ObterCpfCnpj()
        {
            return string.Empty;
        }
    }

    [TestFixture]
    public class ValidacaoDocumentoTests
    {
        private const string DADO_DOCUMENTO_VALIDO = "123456789";
        private const string DADO_DOCUMENTO_INVALIDO = "";

        [TestCase(DADO_DOCUMENTO_VALIDO, DADO_DOCUMENTO_VALIDO, "CPF", true, "Sem erro!")]
        [TestCase(DADO_DOCUMENTO_INVALIDO, DADO_DOCUMENTO_INVALIDO, "CNPJ", false, "Não foi encontrado cnpj em seu cadastro")]
        [TestCase(DADO_DOCUMENTO_INVALIDO, DADO_DOCUMENTO_VALIDO, "CPF_CNPJ", false, "O cpf/cnpj informado é diferente")]
        public void Deve_validar_documento(string documentoInformado, string documentoUsuario, string nomeCampo, bool resultado, string mensagem)
        {
            var dadosDocumento = Substitute.For<IDadosDocumento>();
            dadosDocumento.ObterCpfCnpj().Returns(documentoUsuario);
            var daoBase = Substitute.For<IDAO<IDadosDocumento>>();
            daoBase.GetFirstOrDefault(Arg.Any<Criteria>()).Returns(dadosDocumento);

            var validador = new ValidacaoDocumento<IDAO<IDadosDocumento>>(daoBase);
            var resultadoValidacao = validador.Validar(documentoInformado, nomeCampo);

            var resultadoEsperado = new ValidacaoDto()
            {
                Resultado = resultado,
                Mensagem = mensagem
            };

            resultadoValidacao.ShouldBeEquivalentTo(resultadoEsperado);
        }
    }
}
