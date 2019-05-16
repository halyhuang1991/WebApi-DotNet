using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSameDomain")]
    public class ValuesController : Controller
    {
        public static List<book> lss=new List<book>();
        private readonly ApiContext _context;

        public ValuesController(ApiContext context)
        {
            _context = context;
        }
        //---------------------------
        [HttpPost]
        public IActionResult Create([FromBody]book model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _context.book.Add(model);//book对应数据库表名
            _context.SaveChanges();
            _context.book.Update(new book(){id=12,name="sd",booknum=11});
            _context.SaveChanges();
            return Ok(model);
        }
        //GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        { 
            //http://localhost:5000/api/Values?id=1&type=4
            List<book> ls=_context.book.Where(x=>x.id>1).ToList();
            lss.AddRange(ls);
            return new string[] { "value1", "value2" };
        }
        [HttpGet,Route("search")]
        public List<book> GetLS(int id)
        {
            return lss;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.book.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Remove(book);
            _context.SaveChanges();

            return Ok(book);
        }
        public ActionResult index()
        {
            return Ok("name:value");
        }
    }
}
