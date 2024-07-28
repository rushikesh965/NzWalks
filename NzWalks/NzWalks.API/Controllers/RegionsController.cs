using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Data;
using NzWalks.API.Models.Domain;
using NzWalks.API.Models.DTOs;
using System.Xml.Linq;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            //get data from database  - domain models 
            var regionsDomain = dbContext.Regions.ToList();

            //Map domain models to dtos 
            var regionDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl

                });
            }
            return Ok(regionDto);

        }


        [HttpGet]
        [Route("{id:Guid}")]
        //get region by id 
        public IActionResult GetById([FromRoute]Guid id)
        {
            //get data from database  - regiondomain models 
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            
            if(regionDomain == null)
            {
                return NotFound();
            }

            //map/convert region domain model to Region dto
            
                var regionsDto = new RegionDto
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl

                };
                // return dto back to client
                return Ok(regionsDto); 
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert Dto to domian model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain model back to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDomainModel );
        }






    }
}
