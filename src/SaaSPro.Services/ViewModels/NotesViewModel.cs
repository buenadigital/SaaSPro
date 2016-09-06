using System;
using SaaSPro.Common;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class NotesViewModel
    {
        public Note CustomerNote { get; set; }

        public IPagedList<Note> Notes { get; set; }

        public class Note
        {
            public Guid Id { get; set; }
            public Guid CustomerId { get; set; }
            [Required(ErrorMessage = "Note is required.")]
            public string NoteContent { get; set; }
            [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
            public DateTime CreatedOn { get; set; }
        }
    }
}