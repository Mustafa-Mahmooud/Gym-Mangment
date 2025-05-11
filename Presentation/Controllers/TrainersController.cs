using AutoMapper;
using AutoMapper.Execution;
using Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOS;
using Presentation.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly IGeneric<Trainers> _trainersRepo;
        private readonly IMapper _mapper;

        public TrainersController(IGeneric<Trainers> TrainersRepo, IMapper mapper)
        {
            _trainersRepo = TrainersRepo;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainersDTO>>> GetAllTrainersAsync()
        {
            var Trainers = await _trainersRepo.GetAll();
            var TrainersMapped = _mapper.Map<List<TrainersDTO>>(Trainers);
            return Ok(TrainersMapped);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<TrainersDTO>> GetTrainerByIdAsync(int id)
        {
            var Trainer = await _trainersRepo.GetById(id);
            var TrainerMapped = _mapper.Map<TrainersDTO>(Trainer);
            return Ok(TrainerMapped);
        }




        [HttpPost]
        public async Task<ActionResult<TrainersDTO>> AddTrainer([FromBody] TrainersDTO trainersDTO)
        {
            if (trainersDTO == null) return BadRequest();
            var MappedTrainer = _mapper.Map<Trainers>(trainersDTO); //Mapping From DTO To Model For Pass It To Repo  
            var TrainerCreated = await _trainersRepo.Add(MappedTrainer);
            if (!TrainerCreated) return BadRequest();
            return CreatedAtAction(nameof(GetTrainerByIdAsync), new { id = MappedTrainer.Id }, _mapper.Map<TrainersDTO>(TrainerCreated));
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<TrainersDTO>> UpdateTrainer(int id, [FromBody] TrainersDTO trainersDTO)
        {
            var existingTrainer = await _trainersRepo.GetById(id);    //Trainers
            if (existingTrainer == null) return BadRequest();

            var UpdatedTrainer = _mapper.Map<Trainers>(trainersDTO);  //Trainers  
            await _trainersRepo.Update(UpdatedTrainer);                  //Save Updated Data
            return Ok(_mapper.Map<TrainersDTO>(UpdatedTrainer)); 
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _trainersRepo.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }




}
