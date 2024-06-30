namespace TestApi.Helpers.File;

    /// <summary>
    /// Represents the result of validating a file.
    /// </summary>
    public class FileValidationResult
    {
        /// <summary>
        /// Gets a value indicating whether the file is valid or not.
        /// </summary>
        public bool IsValid { get; }
        
        /// <summary>
        /// Gets the error message if the file is invalid.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileValidationResult"/> class.
        /// </summary>
        /// <param name="isValid">A value indicating whether the file is valid or not.</param>
        /// <param name="errorMessage">The error message if the file is invalid.</param>
        public FileValidationResult(bool isValid, string errorMessage = "")
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }
