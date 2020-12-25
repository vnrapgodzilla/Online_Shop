namespace Model.EF
{
    using StaticResources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_Name", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "Content_RequiredName", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_MetaTitle", ResourceType = typeof(Resources))]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Content_Description", ResourceType = typeof(Resources))]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_Image", ResourceType = typeof(Resources))]
        public string Image { get; set; }

        [Display(Name = "Content_CategoryID", ResourceType = typeof(Resources))]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        //[DisplayName("Chi tiết")]
        [Display(Name = "Content_Detail", ResourceType = typeof(Resources))]
        public string Detail { get; set; }

        [Display(Name = "Content_Warranty", ResourceType = typeof(Resources))]
        public int? Warranty { get; set; }

        [Display(Name = "Content_Status", ResourceType = typeof(Resources))]
        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_MetaKeywords", ResourceType = typeof(Resources))]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_MetaDescriptions", ResourceType = typeof(Resources))]
        public string MetaDescriptions { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        [Display(Name = "Content_Tags", ResourceType = typeof(Resources))]
        public string Tags { get; set; }

        [StringLength(2)]
        public string Language { get; set; }
    }
}
