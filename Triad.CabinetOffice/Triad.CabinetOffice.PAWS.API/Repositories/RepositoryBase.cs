using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS2;

namespace Triad.CabinetOffice.PAWS.API.Repositories
{
    public class RepositoryBase
    {
        protected PAWS2Entities db { get; set; }

        public RepositoryBase(PAWS2Entities context)
        {
            this.db = context;
        }

        public RepositoryBase() : this(new PAWS2Entities())
        {

        }
    }
}