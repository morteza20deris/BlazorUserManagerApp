using System.ComponentModel.DataAnnotations;

namespace BlazorUserManagerApp.Models
{
    public class RoleType
    {
        public string Id { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
