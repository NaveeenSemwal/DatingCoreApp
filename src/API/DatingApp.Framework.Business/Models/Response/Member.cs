﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business.Models.Response
{
    public class Member
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public int Age { get; set; }

        public string PhotoUrl { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }

        public string Introducation { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<MemberPhoto>? Photos { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
