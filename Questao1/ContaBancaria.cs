using System.Globalization;
using System;

namespace Questao1
{
    public class ContaBancaria {

        public int Id { get; private set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Id = numero;
            Titular = titular;
            Saldo = depositoInicial;
        }
        public ContaBancaria(int numero, string titular)
        {
            Id = numero;
            Titular = titular;
        }

        public void Deposito(double valor)
        {
            Saldo += valor;
        }
        public void Saque(double valor)
        {
            Saldo -= (valor + Constants.TaxaSaque);
        }

        public override string ToString()
        {
            return $"Conta {Id}, Titular: {Titular}, Saldo: $ {Saldo.ToString("N2", CultureInfo.InvariantCulture)}";
        }
    }
}
