using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZooPlanet.Models;

namespace ZooPlanet.Repositories
{
	public class EspeciesRepository : Repository<Especies>
	{

        public EspeciesRepository(animalesContext ctx):base(ctx)
        {

        }


		public override IEnumerable<Especies> GetAll()
		{
			return base.GetAll().OrderBy(x=>x.Especie);
		}

		public IEnumerable<Especies> GetEspeciesByClase(string Id)
		{
			return Context.Especies
				.Include(x => x.IdClaseNavigation)
				.Where(x => x.IdClaseNavigation.Nombre == Id)
				.OrderBy(x => x.Especie);
		}

		public Especies GetEspecieById(int id)
		{
			return Context.Especies.Include(x => x.IdClaseNavigation).FirstOrDefault(x => x.Id == id);
		}
    }
}
