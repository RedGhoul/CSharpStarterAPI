using Application.DTO;
using Application.Validation.Event;
using Application.Validation.Helpers;
using Common.Tests.Generators.DTO;
using Common.Tests.Generators.Services;
using Common.Tests.Integration;
using ExternalServices;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Application.UnitTests.Validation.Event
{
    [Trait("Category", "Validators")]
    [Trait("Category", "Events")]
    public class CreateEventDTOValidatorShould
    {
        private readonly CreateEventDTOValidator _sut;
        private readonly Mock<ILogger<CreateEventDTOValidator>> _Logger;
        private readonly Mock<EventValidationHelpers> _EventValidation;
        private readonly Mock<IBitCoinService> _BitCoinService;

        public CreateEventDTOValidatorShould()
        {
            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _Logger = new Mock<ILogger<CreateEventDTOValidator>>();
            _EventValidation = new Mock<EventValidationHelpers>(context);


            _BitCoinService = new Mock<IBitCoinService>();
            _BitCoinService.Setup(s => s.GetInfo())
                .ReturnsAsync(BitCoinServiceDataGenerator.GetValidBitCoinInfo());

            _sut = new CreateEventDTOValidator(_EventValidation.Object,
                _BitCoinService.Object, _Logger.Object);
        }

        [Fact]
        public void Fail_If_Cost_Is_LessThan_2000()
        {
            // Arrange
            CreateEventDTO CreateEventDTO = EventDTOGenerator.GetCreateEventDTO();
            CreateEventDTO.Cost = 500;

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEventDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(CreateEventDTO.Cost));
        }

        [Fact]
        public void Fail_If_Name_LengthIsLess_Than_5()
        {
            // Arrange
            CreateEventDTO CreateEventDTO = EventDTOGenerator.GetCreateEventDTO();
            CreateEventDTO.Name = "Hi";

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEventDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(CreateEventDTO.Name));
        }

        [Fact]
        public void Pass_If_CreateEventCommand_IsValid()
        {
            // Arrange
            CreateEventDTO CreateEventDTO = EventDTOGenerator.GetCreateEventDTO();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEventDTO);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Fail_If_EventTypeId_DoesNotExist()
        {
            // Arrange
            CreateEventDTO InvalidCreateEventDTO = EventDTOGenerator.GetCreateEventDTOWithInValidEventTypeId();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(InvalidCreateEventDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(InvalidCreateEventDTO.EventTypeId));
        }

        [Fact]
        public void Fail_If_No_BitCoinData_Found()
        {
            // Arrange
            _BitCoinService.Setup(s => s.GetInfo())
              .ReturnsAsync(BitCoinServiceDataGenerator.GetInValidBitCoinInfo());

            CreateEventDTO CreateEventDTO = EventDTOGenerator.GetCreateEventDTO();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEventDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(CreateEventDTO.Cost));
        }
    }
}
