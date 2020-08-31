using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedUp.Ex2.Util
{
    abstract public class OperationHandler
    {

        abstract public int Iterator(int i);

        abstract public bool Comparator(int i, int n);

        abstract public int Operator(int i, int j);
    }
}
