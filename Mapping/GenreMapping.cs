using LearningDotNet.Dtos.GenreDto;
using LearningDotNet.Entities;

namespace LearningDotNet.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
