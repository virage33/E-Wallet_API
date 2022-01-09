
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ewallet.Models
{
    public class BlacklistedTokens
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlacklistedTokensId { get; set; } 
        public string Token { get; set; }
    }
}
