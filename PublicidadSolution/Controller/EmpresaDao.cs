using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Controller
{
    public class EmpresaDao
    {
        private List<EMPRESA> empresas;
        private static EmpresaDao instance;

        public static EmpresaDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmpresaDao();
                }
                return instance;
            }

        }

        public List<EMPRESA> Empresas
        {
            get
            {
                return empresas;
            }

            set
            {
                empresas = value;
            }
        }

        private EmpresaDao()
        {
            Empresas = new List<EMPRESA>();
            Empresas = Facade.Instance.mostrarEmpresas();
        }
    }
}
