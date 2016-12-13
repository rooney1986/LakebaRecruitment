using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recruitment.Data
{
    public interface IDataRepositoryStore
    {
        object this[string key] { get; set; }
    }
}
