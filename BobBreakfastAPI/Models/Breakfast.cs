using BobBreakfast.Breakfast;
using BobBreakfastAPI.ServiceErrors;
using ErrorOr;

namespace BobBreakfastAPI.Models
{
    public class Breakfast
    {
        public const int MinNameLength = 3;

        public const int MaxNameLength = 20;
        
        public const int MinDescriptionLength = 5;

        public const int MaxDescriptionLength = 200;
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
        public DateTime LastModifiedDateTime { get; }
        public List<string> Savory { get; }
        public List<string> Sweet { get; }

        private Breakfast(
            Guid id,
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            DateTime lastModifiedDateTime,
            List<string> savory,
            List<string> sweet)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LastModifiedDateTime = lastModifiedDateTime;
            Savory = savory;
            Sweet = sweet;
        }

        public static ErrorOr<Breakfast> Create(
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            List<string> savory,
            List<string> sweet)
        {
            List<Error> errors = new();
            if (name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                errors.Add (Errors.Breakfast.InvalidName);
            }
            if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength)
            {
                errors.Add (Errors.Breakfast.InvalidDescription);
            }
            if(errors.Count > 0)
            {
                return errors;
            }
            return new Breakfast(
                Guid.NewGuid(),
                name,
                description,
                startDateTime: startDateTime,
                endDateTime,
                DateTime.UtcNow,
                savory,
                sweet);
        }
    }
}
