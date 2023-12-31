﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBasketStats.API.Models;
using MyBasketStats.API.Services.StatsheetServices;

namespace MyBasketStats.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/statsheets")]
    public class StatsheetController : ControllerBase
    {
        private readonly IStatsheetService _statsheetService;


        public StatsheetController(IStatsheetService statsheetService, IMapper mapper)
        {
            _statsheetService=statsheetService ?? throw new ArgumentNullException(nameof(statsheetService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatsheetDto>>> GetStatsheets()
        {
            var statsheets = await _statsheetService.GetAllAsync();
            return Ok(statsheets);
        }
        [HttpGet("{statsheetid}", Name = "GetStatsheet")]
        public async Task<ActionResult<StatsheetDto>> GetStatsheet(int statsheetid)
        {
            if (ModelState.IsValid)
            {
                var item = await _statsheetService.GetByIdAsync(statsheetid);
                if (item!=null)
                {
                    return Ok(item);
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("list", Name = "GetStatsheetList")]
        public async Task<ActionResult<IEnumerable<StatsheetDto>>> GetStatsheetsList(IEnumerable<int> ids)
        {
            var item = await _statsheetService.GetExtendedListWithEagerLoadingAsync(ids);
            if (item!=null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{statsheettodeleteid}")]
        public async Task<ActionResult> DeleteStatsheet(int statsheettodeleteid)
        {
            var operationResult = await _statsheetService.DeleteByIdAsync(statsheettodeleteid);
            if (operationResult.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(operationResult.HttpResponseCode, operationResult.ErrorMessage);
            }
        }
    }
}
