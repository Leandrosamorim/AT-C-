using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infraestrutura.List
{
    public class Repositorio : IRepositorio
    {

        private static List<User> listaUsuarios = new List<User>();

        private const string NOME_ARQUIVO = @"C:\Users\Leandro\source\repos\ConsoleApp3\Infraestrutura\lista_users.txt";

        public Repositorio()
        {
            CarregarLista();
        }

        private void CarregarLista()
        {
            if (!File.Exists(NOME_ARQUIVO))
                File.Create(NOME_ARQUIVO).Close();

            var linhas = File.ReadAllLines(NOME_ARQUIVO);

            foreach (var linha in linhas)
            {
                var info = linha.Split("|");

                var birth = DateTime.Parse(info[3]);

                var user = new User(info[1], info[2], birth);
                listaUsuarios.Add(user);
            }
        }
        public List<User> Pesquisar(string? nome)
        {
            if (!String.IsNullOrEmpty(nome))
                return listaUsuarios.Where(x => x.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase) ||
                x.Sobrenome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                                                           .ToList();
            else
                return listaUsuarios;
        }

        public User GetById(int id)
        {
            return listaUsuarios.First(x => x.Id == id);
        }

        public void Adicionar(User user)
        {
            listaUsuarios.Add(user);
            File.WriteAllLines(NOME_ARQUIVO, listaUsuarios.Select(user => user.ToString()));
        }

        public void Update(int id, string? nome, string? sobrenome, DateTime? birth)
        {
            var user = GetById(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(nome))
                    user.Nome = nome;

                if (!string.IsNullOrEmpty(sobrenome))
                    user.Sobrenome = sobrenome;

                if (birth != null)
                    user.Birth = Convert.ToDateTime(birth);
            }
            File.WriteAllLines(NOME_ARQUIVO, listaUsuarios.Select(user => user.ToString()));
        }

        public void Remover(User user)
        {
            listaUsuarios.Remove(user);
            File.WriteAllLines(NOME_ARQUIVO, listaUsuarios.Select(user => user.ToString()));
        }
    }
}
