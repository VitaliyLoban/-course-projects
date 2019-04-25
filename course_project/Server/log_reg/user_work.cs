namespace log_reg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_work
    {
        public int stud_id { get; set; }

        public string working_off { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [Key]
        public int work_id { get; set; }

        public virtual User User { get; set; }
    }
}
