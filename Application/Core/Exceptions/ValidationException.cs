﻿
using FluentValidation.Results;

namespace Application.Core.Exceptions
{
    public class ValidationException: Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base("Una o más validaciones han fallado.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
