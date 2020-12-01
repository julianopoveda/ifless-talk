using System;

namespace console
{
    public class MathOperations
    {
        public double SenoXIfless(double x, int precisao)
        {
            double senx = x;
            for (int i = 1; i < precisao; i++)
            {
                int termo = 2 * i + 1;
                senx = senx + (2 * (i % 2) - 1) * (Math.Pow(x, termo) / Fatorial(termo));
            }
            return senx;
        }

        //versão não otimizada
        public double SenoX(double x, int precisao)
        {
            bool inverte = true;
            double senx = x;
            for (int i = 1; i < precisao; i++)
            {
                int termo = 2 * i + 1;
                if (!inverte)
                {
                    senx = senx + Math.Pow(x, termo) / Fatorial(termo);
                }
                else
                {
                    senx = senx - Math.Pow(x, termo) / Fatorial(termo);
                }
                inverte = !inverte;
            }
            return senx;
        }

        public int Fatorial(int n)
        {
            int acumulado = 1;

            while (n > 1)
            {
                acumulado *= n;
                n--;
            }

            return acumulado;
        }
    }
}