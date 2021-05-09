using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]

        [Route(CConfig.ListProduct)]
        public JToken FindProduct(string Tensp)
        {
            try
            {

                //viewModel.ListProducts = DatabaseInternal.GetListProduct(new ListProductViewModel());
                //viewModel.CurrentListProductType = DatabaseInternal.GetListProduct(viewModel);
                return JToken.FromObject(Tensp);
            }
            catch (Exception ex)
            {
                /*LogFile.Error(ex.ToString());   */// Ghi thông tin ra file
            }
            return null;
        }
    }
}
