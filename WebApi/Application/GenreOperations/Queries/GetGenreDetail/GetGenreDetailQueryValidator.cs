using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery> // kimin validatorı
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreID).GreaterThan(0); // queryde 0 dan küçük gelmesin
        }
    }
}