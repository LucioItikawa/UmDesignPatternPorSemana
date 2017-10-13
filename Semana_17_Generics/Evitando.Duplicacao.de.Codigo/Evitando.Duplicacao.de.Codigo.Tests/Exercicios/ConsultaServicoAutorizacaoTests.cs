using Evitando.Duplicacao.de.Codigo.Exercicios;
using Evitando.Duplicacao.de.Codigo.Generics._2.ComGenerics;
using Evitando.Duplicacao.de.Codigo.tissLoteGuiasV3_03_02;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Evitando.Duplicacao.de.Codigo.Tests.Exercicios
{

    [TestFixture]
    public class ConsultaServicoAutorizacaoTests
    {
        [TestCase(1)]
        [TestCase(11)]
        [TestCase(21)]
        [TestCase(1211)]
        [TestCase(111221)]
        [TestCase(312211)]
        [TestCase(13112221)]
        [TestCase(1113213211)]
        [TestCase(31131211131221)]
        public void Deve_consultar_autorizacao(long handleGuia)
        {
            var valorDadoBase = handleGuia.ToString();
            var dadosGuia = Substitute.For<IDadosGuia>();
            dadosGuia.NumeroGuia.Returns(valorDadoBase);
            dadosGuia.Beneficiario.Returns(valorDadoBase);
            dadosGuia.ProfissionalExecutante.Returns(valorDadoBase);
            var daoBase = Substitute.For<IDAO<IDadosGuia>>();
            daoBase.Get(Arg.Any<long>()).Returns(dadosGuia);

            var servicoConsulta = new ConsultaServicoAutorizacao(daoBase);
            var resultadoConsulta = servicoConsulta.Consultar(handleGuia);
            var resultadoEsperado = new ctm_consultaGuia()
            {
                numeroGuiaOperadora = valorDadoBase,
                dadosBeneficiario = new ct_beneficiarioDados
                {
                    nomeBeneficiario = valorDadoBase
                },
                profissionalExecutante = new ct_contratadoProfissionalDados
                {
                    nomeProfissional = valorDadoBase
                }
            };

            resultadoConsulta.ShouldBeEquivalentTo(resultadoEsperado);
        }
    }
}
