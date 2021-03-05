using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<Factor> FactorRepository { get;  }
        Repository<Tip> TipRepository { get; }
        Repository<Company> CompanyRepository { get; }
        Repository<Feedback> FeedbackRepository { get; }
        Repository<UserCharacteristic> UserCharacteristicRepository { get; }
        Repository<UserTip> UserTipRepository { get; }
        bool Complete();
    }
}
