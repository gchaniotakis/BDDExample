using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDDExample.Models
{
    public class UserMailerTemplate
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(1080)]
        public string Markdown { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public MailerType MailerType { get; set; }
        public ICollection<UserMailerMessage> Messages { get; set; }

        public UserMailerTemplate()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public string FormatBody(string link)
        {
            var engine = new MarkdownSharp.Markdown();
            var html = engine.Transform(Markdown.Replace("{LINK}", link));
            return html;
        }

    }

    public enum MailerType
    {
        EmailConfirmation,
        ReminderToken
    }
}
