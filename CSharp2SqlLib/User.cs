﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp2SqlLib
{
    public class User
    {

        public int ID { get; set; }
        public int Id { get; internal set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public  bool  IsReviewer { get; set; }
        public bool IsAdmin { get; set; }


    }
}
