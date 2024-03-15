namespace ExpenseTracker.Domain.Abstractions
{
    internal abstract class ValidatorsBase<T>(IEnumerable<(IValidator<T> validator, bool isDependentOnPrevSuccess)> validators)
    {
        public (bool IsValid, IReadOnlyCollection<Error> Errors) AreValid(T entity)
        {
            var errors = new List<Error>();
            var prevSuccessful = false;

            foreach (var validator in validators)
            {
                if (validator.isDependentOnPrevSuccess && !prevSuccessful)
                {
                    continue;
                }

                var error = validator.validator.Validate(entity);

                if (error.HasValue)
                {
                    errors.Add(error.Value);

                    prevSuccessful = false;
                }
                else
                {
                    prevSuccessful = true;
                }
            }

            return (errors.Count == 0, errors);
        }
    }
}
