using System;

namespace Evitando.Duplicacao.de.Codigo.Generics._2.ComGenerics
{
    public partial class SamPrestador : OO.Entidade<SamPrestador>
    {
        public string Nome { get; set; }
    }

    public interface ISamPrestadorProperties
    {
        string Nome { get; set; }
    }

    public partial class SamPrestador : ISamPrestadorProperties
    {
    }

    public interface IDAO<TInterface>
    {
        TInterface Create();
        TInterface Get(long handle);
        TInterface GetFirstOrDefault(OO.Criteria handle);
    }

    public class DaoGenerico<TEntidade, TInterface> : IDAO<TInterface>  where TEntidade : OO.Entidade<TEntidade>, TInterface
    {
        public TInterface Create()
        {
            return OO.Entidade<TEntidade>.Create();
        }

        public TInterface Get(long handle)
        {
            return OO.Entidade<TEntidade>.Get(handle);
        }

        public TInterface GetFirstOrDefault(OO.Criteria criterio)
        {
            return OO.Entidade<TEntidade>.GetFirstOrDefault(criterio);
        }
    }

    public class BusinessComponent
    {
        public void Salvando()
        {
            var meuDao = new DaoGenerico<SamPrestador, ISamPrestadorProperties>();
            var novaEntidade = meuDao.Create();
        }
    }

}
