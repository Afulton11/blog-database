using System;
namespace Core.Business.Contracts
{
    public interface IValidator
    {
        /// <summary>Validates the given instance.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the instance is a null reference.</exception>
        /// <exception cref="Exception">Thrown when the instance is invalid.</exception>
        void ValidateObject(object instance);
    }
}
