using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace EynonSoftware.FIPSAPI.Controllers
{
    public class FIPSController : ApiController
    {
        private List<Models.FIPS> GetData()
        {
            var result = new List<Models.FIPS>();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\FIPS_Dataset.txt");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    try
                    {
                        result.Add(new Models.FIPS()
                        {
                            STATE = values[0],
                            STATEFP = values[1],
                            COUNTYFP = values[2],
                            COUNTYNAME = values[3],
                            CLASSFP = values[4]
                        });
                    }
                    catch (Exception) { }
                }
            }
            return result;
        }

        // GET api/FIPS?state=OH&county=Stark County
        public Models.FIPS Get(string state, string county)
        {
            var data = GetData();
            if (data.Any(o => o.STATE == state.ToUpper() && (o.COUNTYNAME.ToLower() == county.ToLower() || o.COUNTYNAME.ToLower() == county.ToLower() + " county")))
            {
                return data.Where(o => o.STATE == state.ToUpper() && (o.COUNTYNAME.ToLower() == county.ToLower() || o.COUNTYNAME.ToLower() == county.ToLower() + " county")).First();
            }
            return null;
        }
        /*

        // POST api/FIPS
        public void Post([FromBody]string value)
        {
        }

        // PUT api/FIPS/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/FIPS/5
        public void Delete(int id)
        {
        }
        */
    }
}
