using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    internal interface IObserver
    {
        void Update(string message);
    }
}
