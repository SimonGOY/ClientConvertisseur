using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.Services
{
    internal interface IService
    {
        Task<List<Devise>> GetDevisesAsync(string nomControleur);
    }
}
