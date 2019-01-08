using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class BSTController : ApiController
    {
        static Node T = new Node(0);
        public tree Get() {
            var list = new List<int>();
            var l = new List<edge>();
            T.fill(list, l);

            return new tree(list, l);
        }

        public tree Post(int id) {
            T.insert(id);
            return Get();
        }
    }
}
