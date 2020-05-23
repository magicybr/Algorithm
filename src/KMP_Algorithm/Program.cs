using System;

namespace KMP_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            new KmpAlgorithm("abcabcabcababcabcdabcaabc".ToCharArray(), "abcabcd".ToCharArray()).Execute();
            Console.ReadLine();
        }
        public class KmpAlgorithm
        {
            private readonly char[] text;
            private readonly char[] patternStr;
            public KmpAlgorithm(char[] text, char[] patternStr)
            {
                this.text = text;
                this.patternStr = patternStr;
            }

            public void Execute()
            {
                int[] prefix = new int[patternStr.Length];
                CreatePrefixTable(patternStr, prefix, prefix.Length);
                MovePrefixTable(prefix, prefix.Length);
                KmpSearch(prefix);
            }

            private void KmpSearch(int[] prefix)
            {
                // text[i]    , len(text)    = m
                // pattern[j] , len(pattern) = n
                int i = 0, m = text.Length;
                int j = 0, n = patternStr.Length;
                while (i < m)
                {
                    if (j == n - 1 && text[i] == patternStr[j])
                    {
                        Console.WriteLine("Find Pattern:{0},{1}", i - j, new string(text).Substring(i - j, patternStr.Length));
                        j = prefix[j];
                    }
                    if (text[i] == patternStr[j])
                    {
                        i++;
                        j++;
                    }
                    else
                    {
                        j = prefix[j];
                        if (j == -1)
                        {
                            i++;
                            j++;
                        }
                    }

                }
            }

            private void MovePrefixTable(int[] prefix, int n)
            {
                int i;
                for (i = n - 1; i > 0; i--)
                {
                    prefix[i] = prefix[i - 1];
                }
                prefix[0] = -1;
            }
            // abcabcd
            // 0 a
            // 0 ab
            // 0 abc
            // 1 abca
            // 2 abcab
            // 3 abcabc
            // 0 abcabcd
            private void CreatePrefixTable(char[] pattern, int[] prefix, int n)
            {
                prefix[0] = 0;
                int i = 1;
                int len = 0;
                while (i < n)
                {
                    if (pattern[i] == pattern[len])
                    {
                        len++;
                        prefix[i] = len;
                        i++;
                    }
                    else
                    {
                        if (len > 0)
                        {
                            len = prefix[len - 1];
                        }
                        else
                        {
                            prefix[i] = len;
                            i++;
                        }
                    }
                }
            }
        }
    }
}
