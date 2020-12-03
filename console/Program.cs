using System;

namespace tdc_ifless
{
    class Program
    {
        static void Main(int[] args)
        {            
            if (IngressoIsValid(args[0]))
                Console.WriteLine("Pode entrar");
            else
                Console.WriteLine("Por favor compre um ingresso válido");
        }

        private static bool IngressoIsValid(int numeroIngresso)
        {
            if ((numeroIngresso > 0 && numeroIngresso < 10000))
                return true;
            else
                return false;

            // return (numeroIngresso > 0 && numeroIngresso < 10000) ? true : false;
        }

        private static bool IngressoIsValidIfless(int numeroIngresso)
        {            
            return (numeroIngresso > 0 && numeroIngresso < 10000);
        }
    }
}
