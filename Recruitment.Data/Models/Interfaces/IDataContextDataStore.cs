﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recruitment.Models.Interfaces
{
    public interface IDataContextDataStore
    {
        object this[string key] { get; set; }
    }
}