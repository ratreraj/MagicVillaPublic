
using MagicVillaAPI.Data;
using Microsoft.AspNetCore.Mvc;

using MagicVillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using MagicVillaModelAPI.Data;
using MagicVillaAPI.Models.VillaModel;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MagicVillaAPI.Repository.IRepository;
using MagicVillaAPI.Models;
using System.Net;

namespace MagicVillaAPI.Controllers
{
    [Route("api/VillaAPI/")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;
        private readonly IVillaRepository _db;
        private readonly IMapper _mapper;
        private readonly APIResponse response;

        public VillaApiController(ILogger<VillaApiController> logger, IVillaRepository db, IMapper mapper)
        {
            this._logger = logger;
            this._db = db;
            this._mapper = mapper;
            this.response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {

            try
            {
                _logger.LogInformation("Getting villa information");
                IEnumerable<VillaModel> villaList = await _db.GetAllAsync();
                response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = new List<string> { ex.Message };

            }

            return response;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest, Type= typeof(VillaDTO))]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);

                }
                var result = await _db.GetAsync(x => x.Id == id);
                if (result == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = _mapper.Map<VillaDTO>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = new List<string> { ex.Message };

            }

            return response;




        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }
            if (await _db.GetAsync(x => x.Name.ToLower() == villaCreateDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Name is already exist");
                return BadRequest(ModelState);

            }
            if (villaCreateDTO == null)
            {
                return BadRequest(villaCreateDTO);
            }

            // if (villaDTO.Id > 0)
            // {

            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }

            // villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaModel villas = _mapper.Map<VillaModel>(villaCreateDTO);

            // VillaModel villas= new()
            // {
            //     Name=  villaDTO.Name,
            //     Details=  villaDTO.Details,
            //     Rate=villaDTO.Rate,
            //     Sqft= villaDTO.Sqft,
            //     Occupancy= villaDTO.Occupancy,
            //     ImagUrl= villaDTO.ImagUrl,
            //     Amenity= villaDTO.Amenity,
            //     CreatedDate =  DateTime.Now

            // };
            await _db.Create(villas);
            return CreatedAtRoute("GetVilla", new { id = villas.Id }, villas);

        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            await _db.Remove(villa);

            return NoContent();


        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            if (id == 0 || id != villaUpdateDTO.Id)
            {
                return BadRequest();
            }

            if (villaUpdateDTO == null)
            {
                return BadRequest(villaUpdateDTO);
            }


            var villa = await _db.GetAsync(x => x.Id == id);
            if (villa == null)
            {
                return BadRequest(villaUpdateDTO);
            }
            villa = _mapper.Map<VillaModel>(villaUpdateDTO);

            // villa.Name = villaDTO.Name;
            // villa.Details= villaDTO.Details;
            // villa.Rate = villaDTO.Rate;
            // villa.Sqft = villaDTO.Sqft;
            // villa.Occupancy = villaDTO.Occupancy;
            // villa.ImagUrl = villaDTO.ImagUrl;
            // villa.Amenity = villaDTO.Amenity;
            // villa.UpdatedDate = DateTime.Now;
            await _db.UpdateAsync(villa);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDTO> patctVillsDTO)
        {
            if (id == 0 && patctVillsDTO == null)
            {
                return BadRequest();
            }

            var villa = await _db.GetAsync(u => u.Id == id, tracked: false);
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
            if (villa == null)
            {
                return BadRequest();
            }

            patctVillsDTO.ApplyTo(villaDTO, ModelState);
            VillaModel villas = _mapper.Map<VillaModel>(villaDTO);
            await _db.UpdateAsync(villas);

            return NoContent();
        }

    }
}