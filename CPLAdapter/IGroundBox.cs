using System;
using System.Collections.Generic;
using System.Text;

namespace CPLAdapter
{
    interface IGroundBox
    {
        bool Start();
        void Stop();
        void SaveData(string strWits);
    }
}
