using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    public class Image
    {
        [Key] public int Id { get; set; }


        // TITOLO
        [Required(ErrorMessage = "Il nome della foto è obbligatorio")] // aggiunta per la VALIDAZIONE
        [MaxLength(50, ErrorMessage = "Lunghezza massima di 50 CARATTERI")] // aggiunta per la VALIDAZIONE
        public string Title { get; set; }



        // DESCRIZIONE
        [Required(ErrorMessage = "La descrizione della foto è obbligatoria")] // aggiunta per la VALIDAZIONE
        [Column(TypeName = "text")]  // AGGIUNTA se si vogliono specificare ancora di più le colonne
        public string Description { get; set; }



        // IMMAGINE
        [Url(ErrorMessage = "Devi inserire un link valido per l'immagine della Pizza")] // aggiunta per la VALIDAZIONE
        [MaxLength(500, ErrorMessage = "Il link non può essere più lungo di 500 caratteri")] // aggiunta per la VALIDAZIONE
        public string Img { get; set; }



        // RELAZIONE N:N con le Categorie
        public List<Category>? Categories { get; set; }



        //======================================COSTRUTTORE========================================
        public Image(string title, string description, string img) 
        {
            this.Title = title;
            this.Description = description;
            this.Img = img;
        }
        public Image() { }  // SEMPRE aggiungere costruttore VUOTO
    }
}
