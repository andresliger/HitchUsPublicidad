using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Facade
    {
        private static Facade instance;

        public static Facade Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Facade();
                }
                return instance;                
            }
        }

        private Facade() {
        }

        #region CRUD_USUARIO
        /// <summary>
        /// Metodos necesarios para realizar un CRUD de la Entidad Usuario
        /// </summary>        

        public Boolean insertaUsuario(USUARIO objA)
        {
            Int32 cambios = 0;
            using (var db = new PublicidadContext())
            {
                db.USUARIOs.Add(objA);
                cambios = db.SaveChanges();
            }
            return cambios > 0 ? true : false;
        }

        public Boolean modificarUsuario(USUARIO objA)
        {
            Int32 cambios = 0;
            using (var db = new PublicidadContext())
            {
                USUARIO aux = new USUARIO();
                aux = db.USUARIOs.Where(s => s.ID_USUARIO == objA.ID_USUARIO).FirstOrDefault<USUARIO>();
                if (aux != null)
                {
                    db.Entry(aux).CurrentValues.SetValues(objA);                    
                    //aux = objA;
                }
                cambios = db.SaveChanges();                

            }
            return cambios > 0 ? true : false;
        }

        public Boolean eliminarUsuario(USUARIO objA)
        {
            Int32 cambios = 0;
            using (var db = new PublicidadContext())
            {
                USUARIO aux = new USUARIO();
                aux = db.USUARIOs.Where(s => s.ID_USUARIO == objA.ID_USUARIO).FirstOrDefault<USUARIO>();
                if (aux != null)
                {
                    db.USUARIOs.Remove(aux);
                }
                cambios = db.SaveChanges();
            }
            return cambios > 0 ? true : false;
        }

        public List<USUARIO> mostrarUsuarios()
        {
            using (var db = new PublicidadContext())
            {
                return db.USUARIOs.ToList();
            }
        }

        #endregion

        #region CRUD_EMPRESA
        /// <summary>
        /// Metodos necesarios para realizar un CRUD de la Entidad Empresa
        /// </summary>        

        public Boolean insertaEmpresa(EMPRESA objA)
        {
            Int32 cambios = 0;
            using (var db = new PublicidadContext())
            {
                db.EMPRESAs.Add(objA);
                cambios = db.SaveChanges();
            }
            return cambios > 0 ? true : false;
        }

        public Boolean modificarEmpresa(EMPRESA objA)
        {
            Int32 cambios = 0;
            using (var db = new PublicidadContext())
            {
                EMPRESA aux = new EMPRESA();
                aux = db.EMPRESAs.Where(s => s.RUC == objA.RUC).FirstOrDefault<EMPRESA>();
                if (aux != null)
                {
                    db.Entry(aux).CurrentValues.SetValues(objA);
                    //aux = objA;
                }
                cambios = db.SaveChanges();

            }
            return cambios > 0 ? true : false;
        }

        public Boolean eliminarEmpresa(EMPRESA objA)
        {
            Int32 cambios = 0;
            using (var db = new PublicidadContext())
            {
                EMPRESA aux = new EMPRESA();
                aux = db.EMPRESAs.Where(s => s.RUC == objA.RUC).FirstOrDefault<EMPRESA>();
                if (aux != null)
                {
                    db.EMPRESAs.Remove(aux);
                }
                cambios = db.SaveChanges();
            }
            return cambios > 0 ? true : false;
        }

        public List<EMPRESA> mostrarEmpresas()
        {
            using (var db = new PublicidadContext())
            {
                return db.EMPRESAs.ToList();
            }
        }

        #endregion
    }
}
