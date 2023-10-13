namespace net_il_mio_fotoalbum.Models
{
    public class FormContent
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }

        public FormContent(string email, string message)
        {
            this.Email = email;
            this.Message = message;
        }

        public FormContent() { }   // AGGIUNGERE SEMPRE COSTRUTTORE VUOTO
    }
}
