using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.Entities.Models
{
    /// <summary>
    /// Provides main structure to make password secure
    /// </summary>
    interface IUserSecurePassword
    {
        string PasswordHash { get; set; }
        string PasswordSalt { get; set; }
    }
}
