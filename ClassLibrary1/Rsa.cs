using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Rsa
    {
        private long p, q, e;
        public long D { get; set; }
        public long N { get; set; }

        private char[] characters = null;

        public Rsa(long p, long q, params char[] characters)
        {
            this.characters = characters;
            this.p = p;
            this.q = q;
            N = p * q;
            var m = (p - 1) * (q - 1);
            D = Calculate_d(m);
            e = Calculate_e(m);
        }

        public Rsa(long p, long q, long e, long d, params char[] characters)
        {
            this.characters = characters;
            this.p = p;
            this.q = q;
            N = p * q;
            D = d;
            this.e = e;
        }
        public static bool IsSimple(params long[] nums)
        {
            foreach (var num in nums)
            {
                if (num < 2)
                {
                    return false;
                }

                if (num == 2)
                {
                    return true;
                }

                for (long i = 2; i < num; i++)
                {
                    if (num % i == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public List<string> Encrypt(string line)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < line.Length; i++)
            {
                int index = Array.IndexOf(characters, line[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)N);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        private long Calculate_d(long m)
        {
            long d = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0))
                {
                    d--;
                    i = 1;
                }

            return d;
        }

        private long Calculate_e(long m)
        {
            long e = 10;

            while (true)
            {
                if ((e * D) % m == 1)
                    break;
                else
                    e++;
            }

            return e;
        }

        public string Decrypt(List<string> input, long d, long n)
        {
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

    }
}
