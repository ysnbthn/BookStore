using System;
using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        // constructor 
        public UpdateBookCommandValidator(){
            RuleFor(command => command.BookID).GreaterThan(0);
            RuleFor(command => command.Model.GenreID).GreaterThan(0).When(x=>x.Model.GenreID != 0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0).When(x=>x.Model.PageCount != 0);
            RuleFor(command => command.Model.PublishDate.Date).LessThan(DateTime.Now.Date).When(x=>x.Model.PublishDate.Date != DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4).When(x=>x.Model.Title != null);
        }
    }
}