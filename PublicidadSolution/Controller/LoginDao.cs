using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Controller
{
    public class LoginDao
    {
        private Facade objF = new Facade();
        private List<USUARIO> usuarios = new List<USUARIO>();

        public void LoginDAO()
        {
            objF = new Facade();
            usuarios = objF.mostrarUsuarios();
            
        }

        public Boolean validateLogin(String correo, String password)
        {
            usuarios = objF.mostrarUsuarios();
            USUARIO aux;
            aux = usuarios.Where(s => s.USERNAME.Equals(correo) && s.PASSWORD.Equals(password)).FirstOrDefault<USUARIO>();
            return aux != null ? true : false;
        }

        public int retrieveUserLogged(String correo, String password)
        {
            USUARIO aux;
            aux = usuarios.Where(s => s.USERNAME.Equals(correo) && s.PASSWORD.Equals(Utils.Encrypt.MD5HashMethod(password))).FirstOrDefault<USUARIO>();
            return aux != null ? aux.ID_USUARIO : -1;
        }

        public String retrieveUserNameLogged(String correo, String password)
        {
            USUARIO aux;
            aux = usuarios.Where(s => s.USERNAME.Equals(correo) && s.PASSWORD.Equals(Utils.Encrypt.MD5HashMethod(password))).FirstOrDefault<USUARIO>();
            return aux != null ? aux.NOMBRES : "";
        }
    }
}
