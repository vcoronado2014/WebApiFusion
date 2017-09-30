using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;
using VCFramework.EntidadFuniconal;
using VCFramework.EntidadFuncional;

namespace VCFramework.NegocioMySQL
{
    public class AutentificacionUsuario
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.AutentificacionUsuario> ListarUsuarios()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<object> lista = fac.Leer<VCFramework.Entidad.AutentificacionUsuario>(setCnsWebLun);
            List<VCFramework.Entidad.AutentificacionUsuario> lista2 = new List<VCFramework.Entidad.AutentificacionUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.AutentificacionUsuario>().ToList();
            }
            return lista2.FindAll(p => p.EsVigente == 1);
        }

        public static List<VCFramework.Entidad.AutentificacionUsuario> ListarUsuariosPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro2 = new FiltroGenerico();
            filtro2.Campo = "ELIMINADO";
            filtro2.Valor = "0";
            filtro2.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtro2);

            List<object> lista = fac.Leer<VCFramework.Entidad.AutentificacionUsuario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.AutentificacionUsuario> lista2 = new List<VCFramework.Entidad.AutentificacionUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.AutentificacionUsuario>().ToList();
            }
            return lista2;
        }
        public static bool ValidarUsuario(string userName, string password)
        {
            //viene encriptada... la desencripto

            //password = NegocioMySQL.Utiles.Encriptar(password);
            bool retorno = false;

            List<VCFramework.Entidad.AutentificacionUsuario> lista = ListarUsuarios();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Exists(p => p.NombreUsuario == userName && p.Password == password && p.Eliminado == 0 && p.EsVigente == 1);
            }

            return retorno;
        }
        public static VCFramework.Entidad.AutentificacionUsuario ObtenerUsuario(string userName, string password)
        {
            password = NegocioMySQL.Utiles.Encriptar(password);
            VCFramework.Entidad.AutentificacionUsuario retorno = new Entidad.AutentificacionUsuario();

            List<VCFramework.Entidad.AutentificacionUsuario> lista = ListarUsuarios();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.NombreUsuario == userName && p.Password == password && p.Eliminado == 0 && p.EsVigente == 1);
            }

            return retorno;
        }

        public static VCFramework.Entidad.AutentificacionUsuario ObtenerUsuario(string userName)
        {
            VCFramework.Entidad.AutentificacionUsuario retorno = new Entidad.AutentificacionUsuario();

            List<VCFramework.Entidad.AutentificacionUsuario> lista = ListarUsuarios();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.NombreUsuario == userName && p.Eliminado == 0 && p.EsVigente == 1);
            }

            return retorno;
        }
        public static VCFramework.Entidad.AutentificacionUsuario ObtenerUsuario(int id)
        {
            VCFramework.Entidad.AutentificacionUsuario retorno = new Entidad.AutentificacionUsuario();

            List<VCFramework.Entidad.AutentificacionUsuario> lista = ListarUsuarios();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.Id == id && p.Eliminado == 0 && p.EsVigente == 1);
            }

            return retorno;
        }
        public static VCFramework.Entidad.AutentificacionUsuario ObtenerUsuarioCompleto(int id)
        {
            VCFramework.Entidad.AutentificacionUsuario retorno = new Entidad.AutentificacionUsuario();

            List<VCFramework.Entidad.AutentificacionUsuario> lista = ListarUsuarios();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.Id == id);
            }

            return retorno;
        }
        public static int ModificarAus(VCFramework.Entidad.AutentificacionUsuario aus)
        {
            aus.Nuevo = false;
            aus.Borrado = false;
            aus.Modificado = true;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Update<VCFramework.Entidad.AutentificacionUsuario>(aus, setCnsWebLun);
        }
        public static int InsertarAus(VCFramework.Entidad.AutentificacionUsuario aus)
        {
            aus.Nuevo = true;
            aus.Borrado = false;
            aus.Modificado = false;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Insertar<VCFramework.Entidad.AutentificacionUsuario>(aus, setCnsWebLun);
        }


        public static UsuarioFuncional ObtenerUsuarioFuncional(int idUsuario)
        {
            UsuarioFuncional retorno = new UsuarioFuncional();
            retorno.AutentificacionUsuario = new Entidad.AutentificacionUsuario();
            retorno.Rol = new Entidad.Rol();
            retorno.AutentificacionUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(idUsuario);
            if (retorno.AutentificacionUsuario != null && retorno.AutentificacionUsuario.Id > 0)
                retorno.Rol = NegocioMySQL.Rol.ObtenerRolDelUsuario(retorno.AutentificacionUsuario.RolId);
            if (retorno.AutentificacionUsuario != null && retorno.AutentificacionUsuario.Id > 0)
            {
                //si no tiene rol institucion se asocia
                retorno.RolInstitucion = NegocioMySql.RolInstitucion.ObtenerRolPorId(retorno.AutentificacionUsuario.RolId);
                if (retorno.RolInstitucion.Id == 0)
                {
                    retorno.RolInstitucion.Descripcion = retorno.Rol.Descripcion;
                    retorno.RolInstitucion.Id = retorno.Rol.Id;
                    retorno.RolInstitucion.IdOriginal = retorno.Rol.Id;
                    retorno.RolInstitucion.InstId = retorno.AutentificacionUsuario.InstId;
                    retorno.RolInstitucion.Nombre = retorno.Rol.Nombre;
                }
                else
                {
                    retorno.Rol = new Entidad.Rol();
                    retorno.Rol.Id = retorno.RolInstitucion.Id;
                    retorno.Rol.Descripcion = retorno.RolInstitucion.Descripcion;
                    retorno.Rol.Nombre = retorno.RolInstitucion.Nombre;
                }

            }
            if (retorno.AutentificacionUsuario.InstId > 0)
                retorno.Institucion = NegocioMySQL.Institucion.ObtenerInstitucionPorId(retorno.AutentificacionUsuario.InstId);
            if (retorno.AutentificacionUsuario.Id > 0)
                retorno.Persona = NegocioMySQL.Persona.ObtenerPersonaPorUsuId(retorno.AutentificacionUsuario.Id);
            if (retorno.Persona != null)
                retorno.Region = NegocioMySQL.Region.ObtenerRegionPorId(retorno.Persona.RegId);
            if (retorno.Persona != null)
                retorno.Comuna = NegocioMySQL.Comuna.ObtenerComunaPorId(retorno.Persona.ComId);
            if (retorno.RolInstitucion != null && retorno.RolInstitucion.Id > 0)
                retorno.PermisoRol = NegocioMySql.PermisoRol.ObtenerPermisoRolPorId(retorno.RolInstitucion.Id);

            return retorno;
        }
        public static UsuarioFuncional ObtenerUsuarioFuncional(string userName, string password)
        {
            UsuarioFuncional retorno = new UsuarioFuncional();
            retorno.AutentificacionUsuario = new Entidad.AutentificacionUsuario();
            retorno.Rol = new Entidad.Rol();
            retorno.AutentificacionUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(userName, password);
            if (retorno.AutentificacionUsuario != null && retorno.AutentificacionUsuario.Id > 0)
                retorno.Rol = NegocioMySQL.Rol.ObtenerRolDelUsuario(retorno.AutentificacionUsuario.RolId);
            if (retorno.AutentificacionUsuario != null && retorno.AutentificacionUsuario.Id > 0)
            {
                //si no tiene rol institucion se asocia
                retorno.RolInstitucion = NegocioMySql.RolInstitucion.ObtenerRolPorId(retorno.AutentificacionUsuario.RolId);
                if (retorno.RolInstitucion.Id == 0)
                {
                    retorno.RolInstitucion.Descripcion = retorno.Rol.Descripcion;
                    retorno.RolInstitucion.Id = retorno.Rol.Id;
                    retorno.RolInstitucion.IdOriginal = retorno.Rol.Id;
                    retorno.RolInstitucion.InstId = retorno.AutentificacionUsuario.InstId;
                    retorno.RolInstitucion.Nombre = retorno.Rol.Nombre;
                }
                else
                {
                    retorno.Rol = new Entidad.Rol();
                    retorno.Rol.Id = retorno.RolInstitucion.Id;
                    retorno.Rol.Descripcion = retorno.RolInstitucion.Descripcion;
                    retorno.Rol.Nombre = retorno.RolInstitucion.Nombre;
                }

            }
                
            if (retorno.AutentificacionUsuario.InstId > 0)
                retorno.Institucion = NegocioMySQL.Institucion.ObtenerInstitucionPorId(retorno.AutentificacionUsuario.InstId);
            if (retorno.AutentificacionUsuario.Id > 0)
                retorno.Persona = NegocioMySQL.Persona.ObtenerPersonaPorUsuId(retorno.AutentificacionUsuario.Id);
            if (retorno.Persona != null)
                retorno.Region = NegocioMySQL.Region.ObtenerRegionPorId(retorno.Persona.RegId);
            if (retorno.Persona != null)
                retorno.Comuna = NegocioMySQL.Comuna.ObtenerComunaPorId(retorno.Persona.ComId);
            if (retorno.RolInstitucion != null && retorno.RolInstitucion.Id > 0)
                retorno.PermisoRol = NegocioMySql.PermisoRol.ObtenerPermisoRolPorId(retorno.RolInstitucion.Id);

            return retorno;
        }
        public static List<UsuarioEnvoltorio> ListarUsuariosEnvoltorio(int instId)
        {
            List<UsuarioEnvoltorio> lista = new List<UsuarioEnvoltorio>();

            List<UsuarioFuncional> usuarios = ListarUsuariosFuncional(instId);
            if (usuarios != null && usuarios.Count > 0)
            {
                foreach(UsuarioFuncional us in usuarios)
                {
                    UsuarioEnvoltorio env = new UsuarioEnvoltorio();
                    env.Id = us.AutentificacionUsuario.Id;
                    env.NombreCompleto = us.Persona.Nombres + " " + us.Persona.ApellidoPaterno + " " + us.Persona.ApellidoMaterno;
                    env.NombreUsuario = us.AutentificacionUsuario.NombreUsuario;
                    env.Rol = us.RolInstitucion.Nombre;
                    //nombre institucion
                    env.OtroUno = us.Institucion.Nombre;
                    env.OtroDos = us.AutentificacionUsuario.Eliminado.ToString();
                    env.OtroTres = us.AutentificacionUsuario.CorreoElectronico;
                    env.Url = "crearModificarUsuario.html?idUsuario=" + env.Id.ToString() + "&ELIMINADO=0"; ;
                    env.UrlEliminar = "crearModificarUsuario.html?idUsuario=" + env.Id.ToString() + "&ELIMINADO=1";
                    lista.Add(env);
                    
                }
            }

            return lista;
        }
        public static List<UsuarioEnvoltorio> ListarUsuariosEnvoltorioPorRol(int instId, int rolId)
        {
            List<UsuarioEnvoltorio> lista = new List<UsuarioEnvoltorio>();
            List<UsuarioFuncional> usuarios = new List<UsuarioFuncional>();
            //para el administrador
            if (rolId == 1)
                usuarios = ListarUsuariosFuncional();
            else
                usuarios = ListarUsuariosFuncional(instId);

            if (usuarios != null && usuarios.Count > 0)
            {
                foreach (UsuarioFuncional us in usuarios)
                {
                    UsuarioEnvoltorio env = new UsuarioEnvoltorio();
                    env.Id = us.AutentificacionUsuario.Id;
                    env.NombreCompleto = us.Persona.Nombres + " " + us.Persona.ApellidoPaterno + " " + us.Persona.ApellidoMaterno;
                    env.NombreUsuario = us.AutentificacionUsuario.NombreUsuario;
                    env.Rol = us.RolInstitucion.Nombre;
                    //nombre institucion
                    env.OtroUno = us.Institucion.Nombre;
                    env.OtroDos = us.AutentificacionUsuario.Eliminado.ToString();
                    env.OtroTres = us.AutentificacionUsuario.CorreoElectronico;
                    env.Url = "crearModificarUsuario.html?idUsuario=" + env.Id.ToString() + "&ELIMINADO=0"; ;
                    env.UrlEliminar = "crearModificarUsuario.html?idUsuario=" + env.Id.ToString() + "&ELIMINADO=1";
                    lista.Add(env);

                }
            }
            if (usuarios != null && usuarios.Count > 0)
                usuarios = usuarios.OrderBy(p => p.Institucion.Id).ToList();

            return lista;
        }

        public static List<UsuarioEnvoltorio> ListarUsuariosEnvoltorio()
        {
            List<UsuarioEnvoltorio> lista = new List<UsuarioEnvoltorio>();

            List<UsuarioFuncional> usuarios = ListarUsuariosFuncional();
            if (usuarios != null && usuarios.Count > 0)
            {
                foreach (UsuarioFuncional us in usuarios)
                {
                    UsuarioEnvoltorio env = new UsuarioEnvoltorio();
                    env.Id = us.AutentificacionUsuario.Id;
                    env.NombreCompleto = us.Persona.Nombres + " " + us.Persona.ApellidoPaterno + " " + us.Persona.ApellidoMaterno;
                    env.NombreUsuario = us.AutentificacionUsuario.NombreUsuario;
                    env.Rol = us.RolInstitucion.Nombre;
                    env.OtroDos = us.AutentificacionUsuario.Eliminado.ToString();
                    //nombre institucion
                    env.OtroUno = us.Institucion.Nombre;
                    env.OtroTres = us.AutentificacionUsuario.CorreoElectronico;
                    env.Url = "crearModificarUsuario.html?idUsuario=" + env.Id.ToString() + "&ELIMINADO=0"; ;
                    env.UrlEliminar = "crearModificarUsuario.html?idUsuario=" + env.Id.ToString() + "&ELIMINADO=1";
                    lista.Add(env);

                }
            }

            return lista;
        }

        public static List<UsuarioFuncional> ListarUsuariosFuncional(int instId)
        {
            List<UsuarioFuncional> retorno = new List<UsuarioFuncional>();

            List<VCFramework.Entidad.AutentificacionUsuario> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosPorInstId(instId);
            if (usuarios != null && usuarios.Count > 0)
            {
                foreach (VCFramework.Entidad.AutentificacionUsuario usu in usuarios)
                {
                    UsuarioFuncional uf = new UsuarioFuncional();
                    uf.AutentificacionUsuario = new Entidad.AutentificacionUsuario();
                    uf.AutentificacionUsuario = usu;

                    uf.Institucion = new Entidad.Institucion();
                    uf.Institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(usu.InstId);

                    uf.Rol = new Entidad.Rol();
                    uf.Rol = VCFramework.NegocioMySQL.Rol.ListarRoles().Find(p => p.Id == usu.RolId);

                    //if (uf.AutentificacionUsuario != null && uf.AutentificacionUsuario.Id > 0)
                    //{
                    //    //si no tiene rol institucion se asocia
                    //    uf.RolInstitucion = NegocioMySql.RolInstitucion.ObtenerRolPorPadreId(uf.AutentificacionUsuario.RolId, uf.AutentificacionUsuario.InstId);
                    //    if (uf.RolInstitucion.Id == 0)
                    //    {
                    //        uf.RolInstitucion.Descripcion = uf.Rol.Descripcion;
                    //        uf.RolInstitucion.Id = uf.Rol.Id;
                    //        uf.RolInstitucion.IdOriginal = uf.Rol.Id;
                    //        uf.RolInstitucion.InstId = uf.AutentificacionUsuario.InstId;
                    //        uf.RolInstitucion.Nombre = uf.Rol.Nombre;
                    //    }

                    //}

                    uf.RolInstitucion = new Entidad.RolInstitucion();
                    uf.RolInstitucion = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(usu.RolId);

                    uf.Persona = new Entidad.Persona();
                    uf.Persona = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(usu.Id);

                    uf.Region = new Entidad.Region();
                    if (uf.Persona != null)
                        uf.Region = VCFramework.NegocioMySQL.Region.ObtenerRegionPorId(uf.Persona.RegId);

                    uf.Comuna = new Entidad.Comuna();
                    if (uf.Persona != null)
                        uf.Comuna = VCFramework.NegocioMySQL.Comuna.ObtenerComunaPorId(uf.Persona.ComId);

                    if (uf.RolInstitucion != null && uf.RolInstitucion.Id > 0)
                        uf.PermisoRol = NegocioMySql.PermisoRol.ObtenerPermisoRolPorId(uf.RolInstitucion.Id);

                    retorno.Add(uf);
                }
            }

            return retorno;
        }

        public static List<UsuarioFuncional> ListarUsuariosFuncional()
        {
            List<UsuarioFuncional> retorno = new List<UsuarioFuncional>();

            List<VCFramework.Entidad.AutentificacionUsuario> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuarios();
            if (usuarios != null && usuarios.Count > 0)
            {
                foreach (VCFramework.Entidad.AutentificacionUsuario usu in usuarios)
                {
                    UsuarioFuncional uf = new UsuarioFuncional();
                    uf.AutentificacionUsuario = new Entidad.AutentificacionUsuario();
                    uf.AutentificacionUsuario = usu;

                    uf.Institucion = new Entidad.Institucion();
                    uf.Institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(usu.InstId);

                    uf.Rol = new Entidad.Rol();
                    uf.Rol = VCFramework.NegocioMySQL.Rol.ListarRoles().Find(p => p.Id == usu.RolId);

                    //if (uf.AutentificacionUsuario != null && uf.AutentificacionUsuario.Id > 0)
                    //{
                    //    //si no tiene rol institucion se asocia
                    //    uf.RolInstitucion = NegocioMySql.RolInstitucion.ObtenerRolPorPadreId(uf.AutentificacionUsuario.RolId, uf.AutentificacionUsuario.InstId);
                    //    if (uf.RolInstitucion.Id == 0)
                    //    {
                    //        uf.RolInstitucion.Descripcion = uf.Rol.Descripcion;
                    //        uf.RolInstitucion.Id = uf.Rol.Id;
                    //        uf.RolInstitucion.IdOriginal = uf.Rol.Id;
                    //        uf.RolInstitucion.InstId = uf.AutentificacionUsuario.InstId;
                    //        uf.RolInstitucion.Nombre = uf.Rol.Nombre;
                    //    }

                    //}

                    uf.RolInstitucion = new Entidad.RolInstitucion();
                    uf.RolInstitucion = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(usu.RolId);

                    uf.Persona = new Entidad.Persona();
                    uf.Persona = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(usu.Id);

                    uf.Region = new Entidad.Region();
                    if (uf.Persona != null)
                        uf.Region = VCFramework.NegocioMySQL.Region.ObtenerRegionPorId(uf.Persona.RegId);

                    uf.Comuna = new Entidad.Comuna();
                    if (uf.Persona != null)
                        uf.Comuna = VCFramework.NegocioMySQL.Comuna.ObtenerComunaPorId(uf.Persona.ComId);

                    if (uf.RolInstitucion != null && uf.RolInstitucion.Id > 0)
                        uf.PermisoRol = NegocioMySql.PermisoRol.ObtenerPermisoRolPorId(uf.RolInstitucion.Id);

                    retorno.Add(uf);
                }
            }

            return retorno;
        }
    }
}
