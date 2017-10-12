using Evitando.Duplicacao.de.Codigo.Generics._2.ComGenerics;
using Evitando.Duplicacao.de.Codigo.OO;

namespace Evitando.Duplicacao.de.Codigo.Exercicios
{
    // Exercicio 4: Precisamos verificar se o cpf ou cnpj informado é
    // o mesmo que esta salvo no banco para 3 entidades diferentes

    public interface IDadosDocumento
    {
        string ObterCpfCnpj();
    }

    public class SamBeneficiario : Entidade<SamBeneficiario>, IDadosDocumento
    {
        public string Cpf { get; set; }
        
        public string ObterCpfCnpj()
        {
            return Cpf;
        }
    }

    public class SamPrestador : Entidade<SamPrestador>, IDadosDocumento
    {
        public string Cnpj { get; set; }

        public string ObterCpfCnpj()
        {
            return Cnpj;
        }
    }

    public class SamContratante : Entidade<SamContratante>, IDadosDocumento
    {
        public string CnpjOuCpf { get; set; }

        public string ObterCpfCnpj()
        {
            return CnpjOuCpf;
        }
    }

    public class ValidacaoDto
    {
        public bool Resultado { get; set; }
        public string Mensagem { get; set; }
    }

    public class ValidacaoDocumento<TDAO> where TDAO : IDAO<IDadosDocumento>
    {
        private TDAO _daoBase;

        public ValidacaoDocumento(TDAO daoBase)
        {
            _daoBase = daoBase;
        }

        public ValidacaoDto Validar(string documentoInformado, string nomeCampo)
        {
            var criteria = new Criteria(string.Format("WHERE {0} = :{0}", nomeCampo.ToUpperInvariant()), documentoInformado);
            var cpfCadastrado = _daoBase.GetFirstOrDefault(criteria).ObterCpfCnpj();
            if (string.IsNullOrEmpty(cpfCadastrado))
                return new ValidacaoDto { Resultado = false, Mensagem = string.Format("Não foi encontrado {0} em seu cadastro", nomeCampo.Replace("_", "/").ToLowerInvariant()) };

            if (cpfCadastrado != documentoInformado)
                return new ValidacaoDto { Resultado = false, Mensagem = string.Format("O {0} informado é diferente", nomeCampo.Replace("_", "/").ToLowerInvariant()) };

            return new ValidacaoDto { Resultado = true, Mensagem = "Sem erro!" };
        }
    }

    public class ValidacaoCadastro
    {
        public ValidacaoDto ValidarCpfBeneficiario(string cpfInformado)
        {
            var validacaoDocumento = ObterValidadorDocumentos<SamBeneficiario>();
            return validacaoDocumento.Validar(cpfInformado, "CPF");
        }

        public ValidacaoDto ValidarCnpjPrestador(string cnpjInformado)
        {
            var validacaoDocumento = ObterValidadorDocumentos<SamPrestador>();
            return validacaoDocumento.Validar(cnpjInformado, "CNPJ");
        }

        public ValidacaoDto ValidarCpfCnpjPrestador(string informado)
        {
            var validacaoDocumento = ObterValidadorDocumentos<SamContratante>();
            return validacaoDocumento.Validar(informado, "CPF_CNPJ");
        }

        private ValidacaoDocumento<DaoGenerico<TEntidade, IDadosDocumento>> ObterValidadorDocumentos<TEntidade>() where TEntidade : Entidade<TEntidade>, IDadosDocumento
        {
            return new ValidacaoDocumento<DaoGenerico<TEntidade, IDadosDocumento>>(new DaoGenerico<TEntidade, IDadosDocumento>());
        }
    }
}
