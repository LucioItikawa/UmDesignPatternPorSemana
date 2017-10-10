using Evitando.Duplicacao.de.Codigo.Generics._2.ComGenerics;
using Evitando.Duplicacao.de.Codigo.OO;
using Evitando.Duplicacao.de.Codigo.tissLoteGuiasV3_02_02;

namespace Evitando.Duplicacao.de.Codigo.Exercicios
{
    // Exercicio 3: Temos 3 entidades que representam uma autorizacao,
    // precisamos converte-las para a resposta de um servico, o codigo
    // atual triplica essa implementacao, como podemos melhorar?

    // Entidades

    public interface IDadosGuia
    {
        string NumeroGuia { get; set; }
        string ProfissionalExecutante { get; set; }
        string Beneficiario { get; set; }
    }

    public class SamGuia : Entidade<SamGuia>, IDadosGuia
    {
        public string NumeroGuia { get; set; }
        public string ProfissionalExecutante { get; set; }
        public string Beneficiario { get; set; }
    }

    public class SamAutoriz : Entidade<SamAutoriz>, IDadosGuia
    {
        public string NumeroGuia { get; set; }
        public string ProfissionalExecutante { get; set; }
        public string Beneficiario { get; set; }
    }

    public class WebAutoriz : Entidade<SamAutoriz>, IDadosGuia
    {
        public string NumeroGuia { get; set; }
        public string ProfissionalExecutante { get; set; }
        public string Beneficiario { get; set; }
    }

    // Serviços

    public class ConsultaServicoAutorizacao
    {
        private Generics._2.ComGenerics.IDAO<IDadosGuia> _dao;

        public ConsultaServicoAutorizacao(Generics._2.ComGenerics.IDAO<IDadosGuia> dao)
        {
            _dao = dao;
        }

        public ctm_consultaGuia Consultar(long handle)
        {
            var guia = _dao.Get(handle);

            return new ctm_consultaGuia
            {
                numeroGuiaOperadora = guia.NumeroGuia,
                dadosBeneficiario = new ct_beneficiarioDados
                {
                    nomeBeneficiario = guia.Beneficiario
                },
                profissionalExecutante = new ct_contratadoProfissionalDados
                {
                    nomeProfissional = guia.ProfissionalExecutante
                }
            };
        }
    }

    public class ConsultaGuias
    {
        public ctm_consultaGuia ConsultarGuia(long handle)
        {
            var servicoConsulta = new ConsultaServicoAutorizacao(new DaoGenerico<SamGuia, IDadosGuia>());
            return servicoConsulta.Consultar(handle);
        }
    }

    public class ConsultaAutorizacao
    {
        public ctm_consultaGuia ConsultarAutorizacao(long handle)
        {
            var servicoConsulta = new ConsultaServicoAutorizacao(new DaoGenerico<SamAutoriz, IDadosGuia>());
            return servicoConsulta.Consultar(handle);
        }
    }

    public class ConsultaAutorizacaoWeb
    {
        public ctm_consultaGuia ConsultarAutorizacaoWeb(long handle)
        {
            var servicoConsulta = new ConsultaServicoAutorizacao(new DaoGenerico<WebAutoriz, IDadosGuia>());
            return servicoConsulta.Consultar(handle);
        }
    }
}
