using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.ClientModel
{
    /// <summary>
    /// Клиент с привелегиями
    /// </summary>
    public class SpecialClient:ClientBase
    {
        public SpecialClient(Guid id,string name, string surname)
           : base(id,name, surname, ClientType.Special) { }
    }
}
