using Application.DTO.Email;
using Application.Validation.Email;
using Common.Tests.Generators.DTO;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Validation.Email
{
    [Trait("Category", "Validators")]
    [Trait("Category", "Email")]
    public class CreateEmailDTOValidatorShould
    {
        private readonly CreateEmailDTOValidator _sut;

        public CreateEmailDTOValidatorShould()
        {
            _sut = new CreateEmailDTOValidator();
        }

        [Fact]
        public void Fail_If_InValid_Email_Address()
        {
            // Arrange
            SendEmailDTO CreateEmailDTO = EmailDTOGenerator.GetInValidEmailEventDTO();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEmailDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(CreateEmailDTO.Recipient));
        }

        [Fact]
        public void Pass_If_InValid_Email_Address()
        {
            // Arrange
            SendEmailDTO CreateEmailDTO = EmailDTOGenerator.GetValidSendEmailDTO();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEmailDTO);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Pass_If_Subject_Length_Less_Than_60()
        {
            // Arrange
            SendEmailDTO CreateEmailDTO = EmailDTOGenerator.GetValidSendEmailDTO();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEmailDTO);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Fail_If_Subject_Greater_Than_60()
        {
            // Arrange
            SendEmailDTO CreateEmailDTO = EmailDTOGenerator.GetdEmailEventDTOWithInvalidSubjectLength();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEmailDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(CreateEmailDTO.Subject));
        }

        [Fact]
        public void Fail_If_BodyHTML_Greater_Than_1000()
        {
            // Arrange
            SendEmailDTO CreateEmailDTO = EmailDTOGenerator.GetdEmailEventDTOWithInvalidBodyHTMLLength();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEmailDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(CreateEmailDTO.BodyHTML));
        }

        [Fact]
        public void Fail_If_BodyPlainText_Greater_Than_500()
        {
            // Arrange
            SendEmailDTO CreateEmailDTO = EmailDTOGenerator.GetdEmailEventDTOWithInvalidBodyPlainTextLength();

            // Act
            FluentValidation.Results.ValidationResult result = _sut.Validate(CreateEmailDTO);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(o => o.PropertyName == nameof(CreateEmailDTO.BodyPlainText));
        }
    }
}
