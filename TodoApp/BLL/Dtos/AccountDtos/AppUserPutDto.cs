﻿namespace BLL.Dtos.AccountDtos
{
    public class AppUserPutDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

