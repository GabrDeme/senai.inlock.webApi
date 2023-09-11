using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using senai.inlock.webApi_.Repositories;
using System.Data;

namespace senai.inlock.webApi_.Controller
{   
    
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class EstudioController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }

        public EstudioController()
        {
            _estudioRepository = new EstudioRepository();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        public IActionResult Post(EstudioDomain novoEstudio)
        {
            try
            {
                _estudioRepository.Cadastrar(novoEstudio);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<EstudioDomain> ListarEstudios = _estudioRepository.ListarTodos();
                return Ok(ListarEstudios);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
