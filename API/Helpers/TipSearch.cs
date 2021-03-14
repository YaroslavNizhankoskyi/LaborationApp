using API.DTO;
using API.Models;
using API.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class TipSearch : Tip
    {
        private readonly IUnitOfWork _uow;
        private readonly string hash;

        public TipSearch(IUnitOfWork uow, string hash, Tip tip) : base(tip)
        {
            _uow = uow;
            this.hash = hash;
        }

        public Tip FindTip() 
        {
            var tipFromDb = _uow.TipRepository
                .Find(p => p.FactorHash == hash)
                .FirstOrDefault();

            if (tipFromDb != null) 
            {
                return tipFromDb;
            }

            return null;
        }

        public bool TipExists() 
        {
            var tipFromDb = _uow.TipRepository
                .Find(p => p.FactorHash == hash)
                .FirstOrDefault();
            return tipFromDb == null;
        }


    }
}
