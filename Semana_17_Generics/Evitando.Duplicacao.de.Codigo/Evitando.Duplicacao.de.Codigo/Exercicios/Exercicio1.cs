using Evitando.Duplicacao.de.Codigo.OO;

namespace Evitando.Duplicacao.de.Codigo.Exercicios
{
    // Exercicio 1: Precisamos preencher as informações de contato do prestador, 
    // beneficiário e contratante em um dto, atualmente nosso código esta duplicado
    // para cada uma das entidades, como podemos melhorar isso?

    public interface IDadosContato
    {
        string Email { get; set; }

        string Telefone { get; set; }

        string Endereco { get; set; }
    }

    public class Prestador : Entidade<Prestador>, IDadosContato
    {
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }

    public class Beneficiario : Entidade<Beneficiario>, IDadosContato
    {
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }

    public class Contratante : Entidade<Contratante>, IDadosContato
    {
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }

    public class ContatoDto
    {
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }

    public class InformacoesContato
    {
        public ContatoDto ObterContato(IDadosContato dadosUsuarioContrato)
        {
            return new ContatoDto
            {
                Email = dadosUsuarioContrato.Email,
                Endereco = dadosUsuarioContrato.Endereco,
                Telefone = dadosUsuarioContrato.Telefone
            };
        }
    }
}
