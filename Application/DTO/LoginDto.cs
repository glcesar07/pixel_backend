﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class LoginDto
    {        
        public string? Username { get; set; }        
        public string? Password { get; set; }        
    }
}
