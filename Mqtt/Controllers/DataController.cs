using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mqtt.Models;
using Mqtt.Repositories;

namespace Mqtt.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DataController: Controller {

        private DataRepository Repository = new();

        [HttpGet]
        public IActionResult Get() {
            var result = Repository.GetDataModels();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DataModel data) {
            return Ok(Repository.PostDataModel(data));
        }

        [HttpPut("id/{id}")]
        public IActionResult Put(int id, [FromBody] DataModel data) {
            var result = Repository.PutDataModel(id, data);
            if (result == null) {
                return NotFound();
            } else {
                return Ok(result);
            }
        }

        [HttpDelete("id/{id}")]
        public IActionResult Delete(int id) {
            var result = Repository.DeleteDataModel(id);
            if (result) {
                return Ok(null);
            } else {
                return NotFound();
            }
        }

    }
}

