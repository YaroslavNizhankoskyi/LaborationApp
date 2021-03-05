using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;

        private Repository<Company> companyRepo;
        private Repository<Factor> factorRepo;
        private Repository<Feedback> feedbackRepo;
        private Repository<Tip> tipRepo;
        private Repository<UserTip> userTipRepo;
        private Repository<UserCharacteristic> userCharacteristicRepo;


        public Repository<Company> Repository
        {
            get
            {
                return companyRepo ?? (companyRepo = new Repository<Company>(_context));
            }
        }

        public Repository<Feedback> FeedbackRepository
        {
            get
            {
                return feedbackRepo ?? (feedbackRepo = new Repository<Feedback>(_context));
            }
        }


        public Repository<Factor> FactorRepository
        {
            get
            {
                return factorRepo ?? (factorRepo = new Repository<Factor>(_context));
            }
        }
        public Repository<Tip> TipRepository
        {
            get
            {
                return tipRepo ?? (tipRepo = new Repository<Tip>(_context));
            }
        }

        public Repository<UserTip> UserTipRepository
        {
            get
            {
                return userTipRepo ?? (userTipRepo = new Repository<UserTip>(_context));
            }
        }

        public Repository<Company> CompanyRepository
        {
            get
            {
                return companyRepo ?? (companyRepo = new Repository<Company>(_context));
            }
        }

        public Repository<UserCharacteristic> UserCharacteristicRepository
        {
            get
            {
                return userCharacteristicRepo ?? (userCharacteristicRepo = new Repository<UserCharacteristic>(_context));
            }
        }


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }



        public bool Complete()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
