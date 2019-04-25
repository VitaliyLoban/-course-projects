namespace log_reg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_activity
    {
        [Key]
        public int act_id { get; set; }

        public int? id_stud { get; set; }

        [StringLength(50)]
        public string participation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public virtual User User { get; set; }
    }
}
