namespace TP_PW3.Models
{
    public class TokenClass
    {
        public string TokenOrMessage { get; set; } = "";
        public Boolean TrueSession { get; set; } = false;

        public DateTime Expired { get; set; } 
    }
}
