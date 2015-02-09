using Microsoft.AspNet.Identity.EntityFramework;
using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class UserSubscriptions
    {
        [Key]
        public long Id { get; set; }
        public string UserEmail { get; set; }
        public string SubscribedUserId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public UserSubscriptions()
        {
            // TODO: Complete member initialization
        }

        public UserSubscriptions(string applicationUserId, string subscribedUserId, string userEmail)
        {
            this.ApplicationUserId = applicationUserId;
            this.SubscribedUserId = subscribedUserId;
            this.UserEmail = userEmail;
        }


    }
}
