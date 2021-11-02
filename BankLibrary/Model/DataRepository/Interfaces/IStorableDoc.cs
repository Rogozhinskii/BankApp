using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.DataRepository.Interfaces
{
    public interface IStorableDoc
    {
        Guid Guid { get; set; }

        IStorableDoc Clone();
    }
}
