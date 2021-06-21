using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHopClassLib;
using Microsoft.EntityFrameworkCore;

namespace BHopsREST.Managers
{
    public class HopsManager
    {
        //FØLGENDE UDKOMMENTEREDE BRUGES NÅR VI IKKE SKAL ANVENDE DATABASE
        //BRUG hops I STEDET FOR context

        //private static int _nextId = 1;

        //private static List<Hop> hops = new List<Hop>()
        //{
        //    new Hop { Id = _nextId++, Name = "Amarillo", AlphaAcid = 7.5, HarvestYear = 2019, Price = 49, Stock = 50.0 },
        //    new Hop { Id = _nextId++, Name = "Cascade US", AlphaAcid = 6.5, HarvestYear = 2020, Price = 69, Stock = 210.1 },
        //    new Hop { Id = _nextId++, Name = "Chinook", AlphaAcid = 11.3, HarvestYear = 2020, Price = 65, Stock = 914.2 },
        //    new Hop { Id = _nextId++, Name = "Mosaic, øko ", AlphaAcid = 10.01, HarvestYear = 2020, Price = 85, Stock = 88.0 },
        //    new Hop { Id = _nextId++, Name = "Saaz", AlphaAcid = 2.5, HarvestYear = 2020, Price = 54, Stock = 22.7 },
        //};

        private readonly HopsContext _context;

        public HopsManager(HopsContext context)
        {
            _context = context;
        }

        public IEnumerable<Hop> GetAll(string sortBy = null, int? maxPrice = null)
        {
            IEnumerable<Hop> hops = _context.Hops;
            if (sortBy != null)
            {
                hops = hops.OrderByDescending(hop => hop.AlphaAcid).ToList();
            }
            if (maxPrice != null)
            {
                hops = hops.Where(hop => hop.Price <= maxPrice);
            }
            return hops;
        }

        public Hop GetById(int id)
        {
            return _context.Hops.Find(id);
        }

        public Hop Add(Hop hop)
        {
            _context.Hops.Add(hop);
            _context.SaveChanges();
            return hop;
        }

        public Hop Delete(int id)
        {
            Hop hop = _context.Hops.Find(id);
            if (hop == null) return null;
            _context.Hops.Remove(hop);
            _context.SaveChanges();
            return hop;
        }

        public Hop Update(int id, Hop updates)
        {
            Hop hop = _context.Hops.Find(id);
            if (hop == null) return null;
            hop.Stock = updates.Stock;
            _context.Entry(hop).State = EntityState.Modified;
            _context.SaveChanges();
            return hop;
        }

        //public List<Hop> GetAll(string sortBy = null, int? maxPrice = null)
        //{
        //    List<Hop> hopslist = new List<Hop>(hops);
        //    if (sortBy != null)
        //    {
        //        hopslist = hopslist.OrderByDescending(hop => hop.AlphaAcid).ToList();
        //    }
        //    if (maxPrice != null)
        //    {
        //        hopslist = hopslist.FindAll(hop => hop.Price <= maxPrice);
        //    }
        //    return hopslist;
        //}

        //public Hop GetById(int id)
        //{
        //    return hops.Find(hop => hop.Id == id);
        //}

        //public Hop Add(Hop hop)
        //{
        //    hop.Id = _nextId++;
        //    hops.Add(hop);
        //    return hop;
        //}

        //public Hop Delete(int id)
        //{
        //    Hop hop = hops.Find(hop => hop.Id == id);
        //    if (hop == null) return null;
        //    hops.Remove(hop);
        //    return hop;
        //}

        //public Hop Update(int id, Hop updates)
        //{
        //    Hop hop = hops.Find(hop => hop.Id == id);
        //    if (hop == null) return null;
        //    hop.Stock = updates.Stock;
        //    return hop;
        //}
    }
}
