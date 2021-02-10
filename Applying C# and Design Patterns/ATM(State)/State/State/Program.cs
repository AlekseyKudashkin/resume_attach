using System;

namespace State
{
    class Program
    {
        static void Main()
        {
            ATM ATM = new ATM(new Waiting(), 1, 10000, 0.2);
        }
    }
}
