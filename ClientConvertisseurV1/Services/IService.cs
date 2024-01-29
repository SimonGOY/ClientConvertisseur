using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSConvertisseur.Models;

namespace ClientConvertisseurV1.Services
{
    internal interface IService
    {
        Task<List<Devise>> GetDevisesAsync(string nomControleur);
    }
}
