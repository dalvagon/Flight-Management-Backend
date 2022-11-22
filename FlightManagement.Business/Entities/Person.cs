using FlightManagement.Business.Helpers;

namespace FlightManagement.Business.Entities
{
    public class Person
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }

        public static Result<Person> Create(
            string name,
            string surname,
            DateTime dateOfBirth,
            string gender
        )
        {
            if (!Enum.TryParse<Gender>(gender, out var personGender))
            {
                var expectedGenderValues = Enum.GetNames(typeof(Gender));
                var textExpectedGenderValues = string.Join(", ", expectedGenderValues);

                return Result<Person>.Failure(
                    $"The provided gender {gender} is not one from the values: {textExpectedGenderValues}"
                );
            }

            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                Gender = personGender
            };

            return Result<Person>.Success(person);
        }
    }
}
