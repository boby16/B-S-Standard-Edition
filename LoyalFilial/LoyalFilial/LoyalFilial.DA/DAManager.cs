using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.DA
{
    public class DAManager
    {
        private static AuthDA _authDAManager;
        public static AuthDA AuthDAManager
        {
            get
            {
                if (_authDAManager == null)
                    _authDAManager = new AuthDA();
                return _authDAManager;
            }
        }
        private static MaintainDA _maintainDAManager;
        public static MaintainDA MaintainDAManager
        {
            get
            {
                if (_maintainDAManager == null)
                    _maintainDAManager = new MaintainDA();
                return _maintainDAManager;
            }
        }
        private static CarPartsDA _carpartsDAManager;
        public static CarPartsDA CarPartsDAManager
        {
            get
            {
                if (_carpartsDAManager == null)
                    _carpartsDAManager = new CarPartsDA();
                return _carpartsDAManager;
            }
        }
    }
}
