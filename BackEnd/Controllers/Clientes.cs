using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class Clientes : ControllerBase
    {
        private readonly IClienteRepositorio clienteRepositorio;
        private readonly IMapper mapper;
        protected ResponseDto _response;

        //repositorios

        public Clientes(IClienteRepositorio clienteRepositorio,IMapper mapper)
        {
            this.clienteRepositorio = clienteRepositorio;
            this.mapper = mapper;
            this._response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<Cliente>>> GetClientes(){
            
            var lista = await clienteRepositorio.GetClientes();
            var listaDto = mapper.Map<ICollection<ClienteDto>>(lista);
            _response.Result = listaDto;
            _response.Mensaje = "Listado de Clientes";
            return Ok(_response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cliente>> GetCliente(int id){

            try
            {
                var cliente = await clienteRepositorio.GetCliente(id);

                if(cliente is null){

                    return NotFound();
                }
                var clienteDto = mapper.Map<ClienteDto>(cliente);

                _response.Result = clienteDto;
                return Ok(_response);
            }
            catch (System.Exception ex)
            {
                _response.IsExitoso = false;
                _response.Error = new List<string> {ex.Message.ToString()};
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] ClientePostDto clientePostDto){

            try
            {
                if (clientePostDto is null)
                {
                    return BadRequest(ModelState);
                }

                if (await clienteRepositorio.ExisteNombre(clientePostDto.Nombre))
                {
                    _response.IsExitoso = false;
                    _response.Mensaje = "Ya Existe el Nombre";
                    return BadRequest(_response);
                }

                var cliente = mapper.Map<Cliente>(clientePostDto);

                if (!await clienteRepositorio.CrearCliente(cliente))
                {
                    _response.IsExitoso = false;
                    _response.Mensaje = "Algo Salio Mal al Guardar la Categoria";
                    return BadRequest(_response);
                }
                _response.IsExitoso = true;
                _response.Result = cliente;
                _response.Mensaje = "Se Guardo Correctamente el Cliente";
                return CreatedAtAction("GetCliente", new {id = cliente.Id},_response);
            }
            catch (System.Exception ex)
            {
                _response.IsExitoso = false;
                _response.Error = new List<string> {ex.Message.ToString()};
                return BadRequest(_response);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Cliente>> PatchCliente(int id, [FromBody] ClienteDto clienteDto){

            try
            {
                if (id != clienteDto.Id)
                {
                    _response.IsExitoso = false;
                    _response.Mensaje = "ID No Coinside";
                    return BadRequest(_response);
                }

                var cliente = mapper.Map<Cliente>(clienteDto);

                if (!await clienteRepositorio.ActualizarCliente(cliente))
                {
                    _response.IsExitoso = false;
                    _response.Mensaje = "Algo Salio Mal al Actualizar el Cliente";
                    return BadRequest(_response);
                }
                _response.IsExitoso = true;
                _response.Result = cliente;
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _response.IsExitoso = false;
                _response.Error = new List<string> {ex.Message.ToString()};
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id){

            try
            {
                if (! await clienteRepositorio.ExisteId(id))
                {
                    return NotFound();
                }

                var cliente = await clienteRepositorio.GetCliente(id);

                if (! await clienteRepositorio.EliminarCliente(cliente))
                {
                    _response.IsExitoso = false;
                    _response.Mensaje = "Algo Salio Mal al Eliminar el Cliente";
                    return BadRequest(_response);
                }
                _response.IsExitoso = true;
                _response.Result = cliente;
                _response.Mensaje = "Se Elimino Correctamente el Cliente";
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _response.IsExitoso = false;
                _response.Error = new List<string> {ex.Message.ToString()};
                return BadRequest(_response);
            }
        }
    }
}