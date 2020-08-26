using Application.DTO;
using Application.Validation;
using Application.Validation.Helpers;
using Common.Tests.Generators.DTO;
using Common.Tests.Integration;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Validation.Event
{
    [Trait("Category", "Validators")]
    [Trait("Category", "Events")]
    public class UpdateEventDTOValidatorShould
    {
        private readonly UpdateEventDTOValidator _sut;
        private readonly Mock<EventValidationHelpers> _EventValidation;

        public UpdateEventDTOValidatorShould()
        {
            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();
            _EventValidation = new Mock<EventValidationHelpers>(context);
            _sut = new UpdateEventDTOValidator(_EventValidation.Object);
        }

        [Fact]
        public void Fail_If_Cost_Is_LessThan_2000()
        {
            // Arrange
            UpdateEventDTO ValidUpdateEventDTO = EventDTOGenerator.GetValidUpdateEventDTO();
            ValidUpdateEventDTO.Cost = 500;

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(ValidUpdateEventDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(ValidUpdateEventDTO.Cost));
        }

        [Fact]
        public void Fail_If_Name_LengthIsLess_Than_5()
        {
            // Arrange
            UpdateEventDTO ValidUpdateEventDTO = EventDTOGenerator.GetValidUpdateEventDTO();
            ValidUpdateEventDTO.Name = "Hi";

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(ValidUpdateEventDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(ValidUpdateEventDTO.Name));
        }

        [Fact]
        public void Pass_If_CreateEventCommand_IsValid()
        {
            // Arrange
            UpdateEventDTO ValidUpdateEventDTO = EventDTOGenerator.GetValidUpdateEventDTO();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(ValidUpdateEventDTO);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Fail_If_EventTypeId_DoesNotExist()
        {
            // Arrange
            UpdateEventDTO inValidUpdateEventDTO = EventDTOGenerator.GetValidUpdateEventDTOWithInvalidEventTypeId();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(inValidUpdateEventDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(inValidUpdateEventDTO.EventTypeId));
        }

    }
}
