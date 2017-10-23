using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS2;
using Triad.CabinetOffice.PAWS.API.Models;


namespace Triad.CabinetOffice.PAWS.API.Repositories
{
    public class SessionRepository:RepositoryBase,IRepository<Models.Session>
    {

        public SessionRepository() : base()
        {
        }

        public SessionRepository(PAWS2Entities context) : base(context)
        {
        }
        
        public IEnumerable<Models.Session> List()
        {
            throw new NotImplementedException();
        }
        public Models.Session Get(int Id)
        {
            throw new NotImplementedException();
        }
        public void Add(Models.Session entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Models.Session entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Models.Session entity)
        {
            throw new NotImplementedException();
        }

    }
}