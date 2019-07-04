using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class GameObjectException:Exception
    {
        static string message = "Попытка создания объекта с неверными характеристиками";
        
        public GameObjectException():base(message)
        {

        }
    }
}
