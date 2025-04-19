using DVotingBackendApp.exceptions;
using DVotingBackendApp.models;
using DVotingBackendApp.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVotingBackendApp.controllers
{
    [ApiController]
    [Route("api/V")]
    public class VoterController : Controller
    {

        private readonly IVoterService _voterService;
        private readonly ICandidateService _candidateService;
        private readonly IConstituencyService _constituencyService;

        public VoterController(IVoterService voterService, ICandidateService candidateService, IConstituencyService constituencyService)
        {
            _voterService = voterService;
            _candidateService = candidateService;
            _constituencyService = constituencyService;
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

        [HttpPost("vote")]
        public async Task<IActionResult> Vote(Vote vote)
        {
            try
            {
                var tx = await _voterService.VoteAsync(vote);

                return Ok($"Voted successfully.\nTx Hash: {tx}");

            }
            catch (InvalidVoterException ex)
            {
                return BadRequest(ex.Message);
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
}
