﻿using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;

        public DataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InitializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                Location loc1 = new Location("Karmstraat", 32, 9790, "Ooike");
                Location loc2 = new Location("Lentestraat", 64);
                Location loc3 = new Location();
                loc3.City = "Ukkel";
                _context.Locations.Add(loc1);
                _context.Locations.Add(loc2);
                _context.Locations.Add(loc3);
                _context.SaveChanges();

                User thibault = new User("Thibault",
                    "De Vrieze",
                    DateTime.Parse("28/01/2000"),
                    "devriezethibault.mail@gmail.com",
                    loc1,
                    "https://www.linkedin.com/in/thibault-de-vrieze-497a55204/",
                    "Ik ben een 21-jarige student Toegepaste Informatica in HoGent");
                User jonas = new User("Jonas",
                    "Vandeputte",
                    DateTime.Parse("03/02/2000"),
                    "jonas.vandeputte@gmail.com",
                    loc2, "",
                    "Ik ben een 20-jarige student Industrieel Ingenieur");

                _context.Users.Add(thibault);
                _context.Users.Add(jonas);
                _context.SaveChanges();

                Event event1 = new Event("Pukkelpop2021", DateTime.Parse("13/08/2021"), thibault, loc3);
                event1.GoToEvent(jonas);
                Event event2 = new Event("Pukkelpop2022", DateTime.Parse("13/08/2022"), jonas, loc3);
                event2.GoToEvent(thibault);
                _context.Events.Add(event1);
                _context.Events.Add(event2);
                _context.SaveChanges();

                Message message1 = new Message("Ik verlang al naar pukkelpop!", jonas);
                _context.Messages.Add(message1);
                _context.SaveChanges();

                event1.PostMessage(message1);

                _context.SaveChanges();

                Console.WriteLine("Database created.");
            }
        }
    }
}
