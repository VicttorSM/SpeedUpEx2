using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedUp.Ex2.Util
{
    class SubUtil : OperationHandler
    {
        public override bool Comparator(int i, int n)
        {
            return i > n;
        }

        public override int Iterator(int i)
        {
            return i - 1;
        }

        public override int Operator(int i, int j)
        {
            return i - j;
        }
    }
}
