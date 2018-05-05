namespace ConsumeJson.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int id { get; set; }

        public int? postId { get; set; }

        [StringLength(150)]
        public string name { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(1050)]
        public string body { get; set; }
    }
}
