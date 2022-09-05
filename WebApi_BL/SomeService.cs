using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_BL
{
    public class SomeService
    {
        private int _value;

        public SomeService()
        {

        }

        public int Value => _value++;
    }
}
