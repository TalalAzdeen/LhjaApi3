using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities;

namespace Entities
{
    public class AuthorizationSession
    {
        [Key]
        public string Id { get; set; } 

        [Required]
        public string SessionToken { get; set; }
        [Required]
        public string AuthorizationType { get; set; } 

        [Required]
        public DateTime StartTime { get; set; } 
        public DateTime? EndTime { get; set; }
        [Required]
        public bool IsActive { get; set; } 

        
        public string UserId { get; set; } 

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } 
        public string IpAddress { get; set; } 

        public string DeviceInfo { get; set; } 

        public AuthorizationSession()
        {
           
            Id = Guid.NewGuid().ToString();
            StartTime = DateTime.UtcNow; 
            IsActive = true; 
        }
    }
}
