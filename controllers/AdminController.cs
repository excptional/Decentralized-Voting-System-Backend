using System.Threading.Tasks;
using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.requests;
using DVotingBackendApp.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVotingBackendApp.controllers
{

    [ApiController]
    [Route("api/A")]
    public class AdminController : Controller
    {

        private readonly IVoterService _voterService;
        private readonly ICandidateService _candidateService;
        private readonly IConstituencyService _constituencyService;

        public AdminController(IVoterService voterService, ICandidateService candidateService, IConstituencyService constituencyService)
        {
            _voterService = voterService;
            _candidateService = candidateService;
            _constituencyService = constituencyService;
        }

        [HttpPost("constituencies")]
        public async Task<IActionResult> RegisterConstituency([FromBody] ConstituencyRequest constituencyRequest)
        {
            {
                try
                {
                    var tx = await _constituencyService.RegisterConstituencyAsync(constituencyRequest);
                    return StatusCode(201, $"Constituency registered successfully.\nTx Hash: {tx}");
                }
                catch (InvalidConstituencyException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

        [HttpPost("candidates")]
        public async Task<IActionResult> RegisterCandidate([FromBody] CandidateRequest candidateRequest)
        {
            {
                try
                {
                    var tx = await _candidateService.RegisterCandidateAsync(candidateRequest);
                    return StatusCode(201, $"Candidate registered successfully.\nTx Hash: {tx}");
                }
                catch (InvalidCandidateException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

        [HttpPost("voters")]
        public async Task<IActionResult> RegisterVoter([FromBody] VoterRequest voterRequest)
        {
            {
                try
                {
                    var tx = await _voterService.RegisterVoterAsync(voterRequest);
                    return StatusCode(201, $"Voter registered successfully.\nTx Hash: {tx}");
                }
                catch (InvalidVoterException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

        [HttpGet("constituencies")]
        public async Task<IActionResult> FetchConstituencies()
        {
            {
                try
                {
                    var constituencies = await _constituencyService.FetchConstituenciesAsync();

                    if(!constituencies.Any())
                        return NoContent();

                    return Ok(constituencies);
                }
                catch (InvalidConstituencyException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (EntityNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

        [HttpGet("{constituency}/candidates")]
        public async Task<IActionResult> FetchCandidates(string constituency)
        {
            {
                try
                {
                    var candidates = await _candidateService.FetchCandidatesAsync(constituency);

                    if (!candidates.Any())
                        return NoContent();

                    return Ok(candidates);
                }
                catch (InvalidCandidateException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (EntityNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

        [HttpGet("{constituency}/voters")]
        public async Task<IActionResult> FetchVoters(string constituency)
        {
            {
                try
                {
                    var voters = await _voterService.FetchVotersAsync(constituency);

                    if (!voters.Any())
                        return NoContent();

                    return Ok(voters);
                }
                catch (InvalidVoterException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (EntityNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

        [HttpGet("candidates/{id}")]
        public async Task<IActionResult> FetchCandidate(string id)
        {
            {
                try
                {
                    var candidate = await _candidateService.FetchCandidateAsync(id);

                    if (candidate == null)
                        return NoContent();

                    return Ok(candidate);
                }
                catch (InvalidCandidateException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (EntityNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

        [HttpGet("voters/{id}")]
        public async Task<IActionResult> FetchVoter(string id)
        {
            {
                try
                {
                    var voter = await _voterService.FetchVoterAsync(id);

                    if (voter == null)
                        return NoContent();

                    return Ok(voter);
                }
                catch (InvalidVoterException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (EntityNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(501, ex.Message);
                }
            }
        }

    }
}
