using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        // constructor 
        public DeleteBookCommandValidator(){
            RuleFor(command => command.BookID).GreaterThan(0);
        }
    }
}