using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUDTEMPLATE.Shared.Lib
{
    public static class LIB
    {
        #region Mã hóa mật khẩu
        static private DatSymmetric m_symmetric = new DatSymmetric();
        public static string EncryptSvc(String strKey, String strData)//mã hóa mật khẩu
        {
            return m_symmetric.EncryptData(strKey, strData);
        }

        public static string DecryptSvc(String strKey, String strData)//Giải mã mật khâu
        {
            return m_symmetric.DecryptData(strKey, strData);
        }
        #endregion
    }
}
