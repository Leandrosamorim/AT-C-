using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infraestrutura.LinkedList
{
    public class Repositorio : IRepositorio
    {
        private static LinkedList<User> listaUsuarios = new LinkedList<User>();
        private const string NOME_ARQUIVO = @"C:\Users\Leandro\source\repos\ConsoleApp3\Infraestrutura\lista_users.txt";


        public List<User> Pesquisar(string nome)
        {
            return listaUsuarios.Where(x => x.Nome.ToLower().Contains(nome.ToLower()))
                                                       .ToList();
        }

        public void Adicionar(User user)
        {
            listaUsuarios.AddLast(user);
            File.WriteAllLines(NOME_ARQUIVO, listaUsuarios.Select(user => user.ToString()));
        }

        public void Remover(User user)
        {
            listaUsuarios.Remove(user);
            File.WriteAllLines(NOME_ARQUIVO, listaUsuarios.Select(user => user.ToString()));
        }

        public void Update(int id, string? nome, string? sobrenome, DateTime? birth)
        {
            var user = GetById(id);
            if (user != null)
            {
                nome = Console.ReadLine();
                sobrenome = Console.ReadLine();
                birth = Convert.ToDateTime(Console.ReadLine());
            }
            File.WriteAllLines(NOME_ARQUIVO, listaUsuarios.Select(user => user.ToString()));
        }

        public User GetById(int id)
        {
            return listaUsuarios.First(x => x.Id == id);
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
                listaUsuarios.AddLast(user);
            }
        }

    }
}

