using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class User     
    {
        public static int idCount = 0;
        public User(string nome, string sobrenome, DateTime birth)
        {
            Id = idCount++;
            Nome = nome;
            Sobrenome = sobrenome;
            Birth = birth;
        }

        public int Id { get; private set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Birth { get; set; }


        public int Aniversario()
        {
            DateTime today = DateTime.Today; //pega a data de hoje
            DateTime next = Birth.AddYears(today.Year - Birth.Year); //adiciona a diferenca do ano atual 
                                                                        //menos o ano de nascimento

            if (next < today) //se o ano for maior q o atual
                next = next.AddYears(1); //so faz aniversario no ano q vem, entao adiciona um ano

            int numDays = (next - today).Days; //calcula o numero de dias entre o proximo aniversario e hoje
            return numDays;
        }

        public override string ToString()
        {
            return $"{Id}|{Nome}|{Sobrenome}|{Birth.Date}";
        }
    }
}