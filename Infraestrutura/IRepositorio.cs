using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura
{
    public interface IRepositorio
    {
        void Adicionar(User user);
        void Remover(User user);
        void Update(int id, string? nome, string? sobrenome, DateTime? birth);
        List<User> Pesquisar(string searchTerm);
        User GetById(int id);
    }
}
