using System;

namespace StudyProject.Application.ViewModels
{
    public abstract class ViewModelBase
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateModified { get; set; }
    }
}
