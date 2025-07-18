﻿using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Usuarios
{
    public class GetById: IGetById<UsuarioDto>
    {
        private IRepositorioUsuario _repo;

        public GetById(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public UsuarioDto Execute(int id)
        {
            return UsuarioMapper.ToDto(_repo.GetById(id));
        }
    }
}
