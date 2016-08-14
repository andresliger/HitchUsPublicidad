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
        private List<USUARIO> usuarios;
        private static LoginDao instance;

        public static LoginDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginDao();
                }
                return instance;
            }

        }

        private LoginDao()
        {
            usuarios = new List<USUARIO>();
            usuarios = Facade.Instance.mostrarUsuarios();            
        }             

        public Boolean validateLogin(String usuario, String password)
        {
            USUARIO aux = new USUARIO();
            aux = usuarios.Where(s => s.USERNAME.Equals(usuario) && s.PASSWORD.Equals(password)).FirstOrDefault<USUARIO>();
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
            aux = usuarios.Where(s => s.USERNAME.Equals(correo) && s.PASSWORD.Equals(password)).FirstOrDefault<USUARIO>();
            return aux != null ? aux.USERNAME : "";
        }

        public List<USUARIO> retrieveAllUsers()
        {
            return usuarios;
        }

        public Boolean modificarUsuario(USUARIO user)
        {
            Boolean aux = Facade.Instance.modificarUsuario(user);
            usuarios.Clear();
            usuarios = Facade.Instance.mostrarUsuarios();
            return aux;
        }

        public Boolean eliminarUsuario(USUARIO user)
        {
            Boolean aux = Facade.Instance.eliminarUsuario(user);
            usuarios.Clear();
            usuarios = Facade.Instance.mostrarUsuarios();
            return aux;            
        }

        public Boolean agregarUsuario(USUARIO user)
        {
            if (!existsUser(user.USERNAME))
            {
                Boolean aux = Facade.Instance.insertaUsuario(user);
                usuarios.Clear();
                usuarios = Facade.Instance.mostrarUsuarios();
                return aux;
            }
            else
            {
                return false;
            }

            
        }

        public Boolean existsUser(String username)
        {
            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuarios[i].USERNAME.Equals(username))
                    return true;
            }
            return false;
        }
    }
}
