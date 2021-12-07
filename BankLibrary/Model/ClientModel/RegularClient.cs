using BankLibrary.Model.ClientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model
{
    /// <summary>
    /// Клиент без привелегий
    /// </summary>
    public class RegularClient : ClientBase
    {
        public RegularClient(Guid id,string name, string surname)
            : base(id,name, surname, ClientType.Regular) { }
    }
}
