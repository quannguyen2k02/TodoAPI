using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Enitites
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title bắt buộc")]
        public string Title { get; set; }
        public bool? IsCompleted { set; get; } = false;
    }
}
