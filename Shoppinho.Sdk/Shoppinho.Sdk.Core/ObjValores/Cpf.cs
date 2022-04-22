using Shoppinho.Sdk.Core.ObjValores.ObjValores.Base;

namespace Shoppinho.Sdk.Core.ObjValores
{
    public class Cpf : CpfCnpjBase
    {
        private const string SquencialInvalido = "12345678909";

        public Cpf(string numero) : base(numero) { }

        protected override int TamanhoMaximo => 11;

        //https://www.macoratti.net/alg_cpf.htm
        public override bool Validar()
        {
            if (ValidarTamanho() &&
                !TodosDigitosIguais() &&
                (SomenteNumeros != SquencialInvalido) &&
                VerificarPrimeiroDigito() &&
                VerificarSegundoDigito())
                return true;

            return false;
        }
        
        protected override bool VerificarPrimeiroDigito()
        {
            var multiplicadores = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            return VerificarDigito(9, multiplicadores);
        }
        
        protected override bool VerificarSegundoDigito() 
        {
            var multiplicadores = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            return VerificarDigito(10, multiplicadores);
        }

        public static implicit operator Cpf(string numero)
        {
            if (Cpf.TryParse(numero, out var cpf))
                return cpf;

            throw new InvalidOperationException("O número de cpf informado é inválido");
        }

        public static bool TryParse(string numero, out Cpf cpf)
        {
            cpf = new Cpf(numero);
            return cpf.Validar();
        }
    }
}