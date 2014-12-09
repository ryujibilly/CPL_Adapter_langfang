using System;
using System.Collections.Generic;
using System.Text;

namespace GLAS_Adapter
{
    public interface IGroundBox
    {
        bool Start();
        void Stop();
        void SaveData(string strWits);
    }
}
