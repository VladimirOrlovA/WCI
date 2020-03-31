using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCI.BLL
{
    // Model Data Transfer Object
    public abstract class ModelDTO { }

    abstract public class ClassBuilder
    {
        public string ResourceAddress { get; set; }

        public ClassBuilder(string resourceAddress)
        {
            ResourceAddress = resourceAddress;
        }

        abstract public ModelDTO Create();

    }
}
