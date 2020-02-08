using System;
using System.Collections.Generic;

namespace Connect.Classes.DataModels
{
    public class Note
    {
        public Guid Id { get; set; }
        public int NoteId { get; set; }
        public int IndividualId { get; set; }
        public string NoteText { get; set; }
        public bool MarkedAsProcessed { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }

    public class NotesViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
    }
}