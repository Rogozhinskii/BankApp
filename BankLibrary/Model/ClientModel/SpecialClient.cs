﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.ClientModel
{
    public class SpecialClient:Client
    {
        public SpecialClient(string name, string surname, ClientType type = ClientType.Special)
            : base(name, surname, type) { }
    }
}
