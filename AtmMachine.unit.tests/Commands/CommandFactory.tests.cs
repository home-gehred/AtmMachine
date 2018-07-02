using System;
using Xunit;
using ATMMachine.Commands;

namespace AtmMachine.unit.tests.Commands
{
    public class CommandFactoryFixture : IDisposable
    {
        public CommandFactoryFixture()
        {
            Factory = new CommandFactory();
        }

        public void Dispose()
        {
            Factory = null;
        }

        public CommandFactory Factory { get; private set; }
    }


    public class CommandFactoryTests : IClassFixture<CommandFactoryFixture>
    {
        private CommandFactoryFixture _fixture;

        public CommandFactoryTests(CommandFactoryFixture commandFactoryFixture)
        {
            _fixture = commandFactoryFixture;
        }

        [Theory]
        [InlineData("@", typeof(InvalidCommand))]
        [InlineData("r", typeof(InvalidCommand))]
        [InlineData(" R", typeof(InvalidCommand))]
        [InlineData("R", typeof(RestockCommand))]
        [InlineData("w", typeof(InvalidCommand))]
        [InlineData("W", typeof(InvalidCommand))]
        [InlineData("W$", typeof(InvalidCommand))]
        [InlineData("W$-80", typeof(InvalidCommand))]
        [InlineData("W $-80", typeof(InvalidCommand))]
        [InlineData("W $80 ", typeof(WithdrawCommand))]
        [InlineData("W $0 ", typeof(WithdrawCommand))]
        [InlineData("W $$80 ", typeof(InvalidCommand))]
        [InlineData(" W $80 ", typeof(InvalidCommand))]
        [InlineData("W $80 $120", typeof(InvalidCommand))]
        [InlineData("i", typeof(InvalidCommand))]
        [InlineData("I", typeof(InvalidCommand))]
        [InlineData("I$", typeof(InvalidCommand))]
        [InlineData("I $-1", typeof(InvalidCommand))]
        [InlineData("I $-5", typeof(InvalidCommand))]
        [InlineData("I $0", typeof(InvalidCommand))]
        [InlineData("I $1000", typeof(InvalidCommand))]
        [InlineData("I$1$5$100", typeof(InvalidCommand))]
        [InlineData("I $1$$5$100", typeof(InvalidCommand))]
        [InlineData("I $1$5$0", typeof(InvalidCommand))]
        [InlineData("I $1 $$5 ", typeof(InvalidCommand))]
        [InlineData("I $1$5$100", typeof(InventoryCommand))]
        [InlineData("I $1 $5 $10 $20 $50 $100", typeof(InventoryCommand))]
        public void GivenUserInputWhenTransformedToCommandThenWorksAsDesired(string userInput, Type expectedType)
        {
            // Arrange
            // Act
            var actual = _fixture.Factory.CreateCommand(userInput);

            // Assert
            Assert.IsType(expectedType, actual);
        }
    }
}
