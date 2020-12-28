﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class ChatMessageModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string MessageText { get; set; }
        public bool WithImage { get; set; }
    }
}
