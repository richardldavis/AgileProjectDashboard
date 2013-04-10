using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Domain
{
    public class BaseTextfileRepository
    {
        protected string _fileRoot { get; set; }

        public BaseTextfileRepository(string fileRoot)
        {
            _fileRoot = fileRoot;
        }


    }
}
