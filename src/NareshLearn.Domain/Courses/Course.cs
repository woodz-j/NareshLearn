using NareshLearn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Domain.Courses
{
    public class Course: AuditableEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid InstructorId { get; private set; }
        public bool IsPublished { get; private set; }

        private Course() { } // EF

        public Course(Guid instructorId, string title, string description)
        {
            if (instructorId == Guid.Empty)
                throw new DomainException("InstructorId is required.");

            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Course title cannot be empty.");

            if (title.Trim().Length > 200)
                throw new DomainException("Course title is too long.");

            Title = title.Trim();
            Description = description?.Trim() ?? string.Empty;
            InstructorId = instructorId;

            IsPublished = false;
        }

        public void Publish()
        {
            IsPublished = true;
            MarkUpdated();
        }
    }
}
