using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class UserDal : BaseDb
    {
        public void Add(User user)
        {
            RunCommand("insert into Users (Name) values ");

        }

    }
}
