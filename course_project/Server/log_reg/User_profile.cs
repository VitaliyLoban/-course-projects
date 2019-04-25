namespace log_reg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;

    public partial class User_profile
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string S_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? b_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? receipt_date { get; set; }

        [Column(TypeName = "image")]
        public byte[] photo { get; set; }

        public string hobbies { get; set; }

        public int? room_num { get; set; }

        [StringLength(20)]
        public string phone_num { get; set; }

        public virtual User User { get; set; }

      
    }
}
