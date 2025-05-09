using AutoMapper;
using Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOS;
using Presentation.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IGeneric<Member> _memberRepo;
        private readonly IMapper _mapper;

        public MemberController(IGeneric<Member> memberRepo, IMapper mapper)
        {
            _memberRepo = memberRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberRepo.GetAll();
            var memberDtos = _mapper.Map<IEnumerable<MemberDTO>>(members);
            return Ok(memberDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _memberRepo.GetById(id);
            if (member == null) return NotFound();
            var memberDto = _mapper.Map<MemberDTO>(member);
            return Ok(memberDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] MemberDTO createDto)
        {
            if (createDto == null) return BadRequest();
            var member = _mapper.Map<Member>(createDto);
            var result = await _memberRepo.Add(member);
            if (!result) return BadRequest();
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, _mapper.Map<MemberDTO>(member));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberDTO updateDto)
        {
            var existing = await _memberRepo.GetById(id);
            if (existing == null) return NotFound();

            _mapper.Map(updateDto, existing); // map updated fields into existing entity
            var result = await _memberRepo.Update(existing);
            if (!result) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _memberRepo.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
