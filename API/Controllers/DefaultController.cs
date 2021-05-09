using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default
        [Route("anh")]
        [HttpGet]
        public IEnumerable<ListSinhVien> List()
        {
            return ListSinhVien.sinhvien;
        }

        // GET: api/Default/5
        [Route("{Name}/{Age}")]
        public string Get(string Name, int Age)
        {
            return Name;
            
        }

        // POST: api/Default
        [Route("em/{name}")]
        [HttpPost]
        public ListSinhVien Sdt([FromUri] string name,ListSinhVien listSinhVien)
        {
            return listSinhVien;
        }

        // PUT: api/Default/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
