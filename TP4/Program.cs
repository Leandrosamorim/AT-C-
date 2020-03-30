using System;
using System.Collections.Generic;
using System.Linq;
using Dominio;
using Infraestrutura;

namespace TP4
{
    class Program
    {
        public static void Main(string[] args)
        {
            var repositorio = UserRepositoryFactory.Criar(TipoRepositorio.List);

            List<User> aniversariantes = new List<User>(); //cria uma lista vazia
            
            foreach (var usuario in repositorio.Pesquisar("")) //repositorio.pesquisar("") tem o mesmo valor que o getall
            {
                if (usuario.Aniversario() == 0)
                {
                    aniversariantes.Add(usuario); //se o aniversario for hoje, adiciona na lista
                }
            }

            if (aniversariantes.Any()) //se existirem elementos na lista
            {
                Console.WriteLine("Aniversariantes do dia");
                foreach (var usuario in aniversariantes)
                {
                    Console.WriteLine("{0}, {1}, {2}", usuario.Nome, usuario.Sobrenome, usuario.Birth);
                }
            }
            var opt = Menu();

            while (opt != "5")
            {
                switch (opt)
                {
                    case "1":
                        Console.WriteLine("Digite o nome a pesquisar");
                        var foundUsers = repositorio.Pesquisar(Console.ReadLine());

                        int i = 0;

                        foreach (User x in foundUsers)
                        {
                            Console.WriteLine("{0} - {1} {2}", i, x.Nome, x.Sobrenome);
                            i++;
                        }

                        if (i == 0)
                        {
                            Console.WriteLine("Nenhum usuario encontrado");
                            opt = Menu();
                            break;
                        }

                        var opt2 = Convert.ToInt32(Console.ReadLine());
                        var primeiro = foundUsers.ElementAt(opt2);

                        var aniversario = primeiro.Aniversario();

                        Console.WriteLine($"Dados da pessoa:\n Nome Completo: {primeiro.Nome} {primeiro.Sobrenome}\n Data do aniversário: {primeiro.Birth}");
                        Console.WriteLine($"Faltam {aniversario} dias para esse aniversario");
                        opt = Menu();
                        break;

                    case "2":
                        Console.WriteLine("Digite o Nome da pessoa que deseja adicionar");
                        var nome = Console.ReadLine();
                        Console.WriteLine("Digite o Sobrenome da pessoa que deseja adicionar");
                        var sobrenome = Console.ReadLine();
                        Console.WriteLine("Digite a data do aniversário no formato dd/mm/yyyy");
                        var birth = Convert.ToDateTime(Console.ReadLine());

                        repositorio.Adicionar(new User(nome, sobrenome, birth));
                        opt = Menu();
                        break;

                    case "3":
                        Console.WriteLine("Digite o Id do usuario a ser atualizado");
                        foreach (var usuario in repositorio.Pesquisar(""))
                            Console.WriteLine($"{usuario.Id} - {usuario.Nome} {usuario.Sobrenome} {usuario.Birth}");

                        var id = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Qual atributo deseja editar?\n1 - Nome\n2 - Sobrenome\n3 - Data de nascimento");
                        var editOpt = Console.ReadLine();
                        switch (editOpt)
                        {
                            case "1":
                                Console.WriteLine("Digite o novo nome");
                                var novoNome = Console.ReadLine();
                                repositorio.Update(id, novoNome, null, null);
                                break;
                            case "2":
                                Console.WriteLine("Digite o novo sobrenome");
                                var novoSobrenome = Console.ReadLine();
                                repositorio.Update(id, null, novoSobrenome, null);
                                break;
                            case "3":
                                Console.WriteLine("Digite a nova data de nascimento");
                                var novaData = Convert.ToDateTime(Console.ReadLine());
                                repositorio.Update(id, null, null, novaData);
                                break;
                        }

                        opt = Menu();
                        break;

                    case "4":
                        Console.WriteLine("Digite o Id do cadastro a ser removido");

                        foreach (var usuario in repositorio.Pesquisar(""))
                            Console.WriteLine($"{usuario.Id} - {usuario.Nome} {usuario.Sobrenome} {usuario.Birth}");

                        id = Int32.Parse(Console.ReadLine());
                        var usuarioRemovido = repositorio.GetById(id);
                        repositorio.Remover(usuarioRemovido);
                        opt = Menu();
                        break;

                    default:
                        Console.WriteLine("Insira um valor entre 1 e 5");
                        opt = Menu();
                        break;

                }
            }
        }

        public static string Menu()
        {
            Console.WriteLine("\nGerenciador de Aniversários \nSelecione uma das opções abaixo" +
                "\n1 - Pesquisar Pessoas \n2 - Adicionar nova pessoa " +
                "\n3 - Atualizar um cadastro \n4 - Remover um cadastro \n5 - Sair");
            var opt = Console.ReadLine();
            return opt;
        }
    }
}