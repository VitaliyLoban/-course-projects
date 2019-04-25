namespace log_reg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Note")]
    public partial class Note
    {
        [Key]
        public int id_note { get; set; }

        public string notification { get; set; }

        
        public DateTime? date_of_create { get; set; }

        
        public DateTime? date_of_takeplace { get; set; }
    }
}
