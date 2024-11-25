using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Enitites
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title bắt buộc")]
        public string Title { get; set; }

        public bool IsFinished { set; get; } = false;

        public bool IsDeleted { set; get; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = "";

        public DateTime ModifiedDate { get; set; } =DateTime.Now;

        public string ModifiedBy { get; set; } = "";
    }
}
