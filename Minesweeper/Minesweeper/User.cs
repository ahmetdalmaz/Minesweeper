using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }

    }
    
}
