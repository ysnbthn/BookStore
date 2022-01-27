using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        // constructor 
        public DeleteBookCommandValidator(){
            RuleFor(command => command.BookID).GreaterThan(0);
        }
    }
}