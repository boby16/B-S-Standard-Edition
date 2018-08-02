using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.BL
{
    public class BLManager
    {
        private static AuthBL _authBLManager;
        public static AuthBL AuthBLManager
        {
            get
            {
                if (_authBLManager == null)
                    _authBLManager = new AuthBL();
                return _authBLManager;
            }
        }
        private static MaintainBL _maintainBLManager;
        public static MaintainBL MaintainBLManager
        {
            get
            {
                if (_maintainBLManager == null)
                    _maintainBLManager = new MaintainBL();
                return _maintainBLManager;
            }
        }
        private static CarPartsBL _carpartsBLManager;
        public static CarPartsBL CarPartsBLManager
        {
            get
            {
                if (_carpartsBLManager == null)
                    _carpartsBLManager = new CarPartsBL();
                return _carpartsBLManager;
            }
        }
    }
}
