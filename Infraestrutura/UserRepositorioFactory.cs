using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura
{
    public class UserRepositoryFactory
    {
        public static IRepositorio Criar()
        {
            return Criar(TipoRepositorio.LinkedList);
        }

        public static IRepositorio Criar(TipoRepositorio tipoRepositorio)
        {
            switch (tipoRepositorio)
            {
                case TipoRepositorio.List:
                    return new List.Repositorio();
                case TipoRepositorio.LinkedList:
                    return new LinkedList.Repositorio();
                default:
                    throw new NotImplementedException("Não existe implementação padrão!");
            }
        }
    }
}