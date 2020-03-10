using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Repositories;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    public class HealthCheckController : Controller
    {
        #region Properties
        private HealthCheckRepository healthCheckRepository { get { return new HealthCheckRepository(); } }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Check that the database is accessible
        /// </summary>
        /// <returns>http status 200 if the check succeeds, else http status 500</returns>
        public HttpStatusCodeResult Index()
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            try
            {
                if (healthCheckRepository.HealthCheck())
                {
                    statusCode = (int)HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }
            return new HttpStatusCodeResult(statusCode);
        }

        #endregion Methods
    }
}