﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Math
{
    public static class Combinatorics
    {
        static Combinatorics()
        {
            const int factorialsCount = 13;

            _factorials = new int[factorialsCount];

            _factorials[0] = 1;

            for (var i = 1; i < factorialsCount; i++)
            {
                _factorials[i] = _factorials[i - 1] * i;
            }

            const int pascalTriangleSize = 34;

            _pascalTriangle = new int[pascalTriangleSize, pascalTriangleSize];

            for (var n = 0; n < pascalTriangleSize; n++)
            {
                _pascalTriangle[n, 0] = 1;

                for (var k = 1; k <= n; k++)
                {
                    _pascalTriangle[n, k] = _pascalTriangle[n - 1, k] + _pascalTriangle[n - 1, k - 1];
                }
            }
        }

        private static readonly int[,] _pascalTriangle;
        private readonly static int[] _factorials;

        public static int Factorial(int n)
        {
            return _factorials[n];
        }

        public static int FallingFactorial(int n, int k)
        {
            var answer = n;

            for (var i = 1; i < k; i++)
            {
                answer *= n - i;
            }

            return answer;
        }

        public static int QuickFallingFactorial(int n, int k)
        {
            return Factorial(n) / Factorial(n - k);
        }

        public static int Binomial(int n, int k)
        {
            if (k < n / 2)
            {
                return FallingFactorial(n, k) / Factorial(k);
            }
            else
            {
                var j = n - k;
                return FallingFactorial(n, j) / Factorial(j);
            }            
        }

        public static int QuickBinomial(int n, int k)
        {
            return _pascalTriangle[n, k];
        }
    }
}
